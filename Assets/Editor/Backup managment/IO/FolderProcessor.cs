namespace UnityBackupManagment
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    internal static class FolderProcessor
    {
        internal static IgnoreCollection IgnoreCollection { get; set; }
        private static FolderData folderData;
        private static CopyProgress copyProgress;

        internal static void OpenFolderInFileExplorer(string folderPath)
        {
            UnityEditor.EditorUtility.RevealInFinder(folderPath);
        }
        internal static void DeleteFolder(string path)
        {
            Directory.Delete(path, true);
        }
        internal static async Task GetFolderDataAsync(string folderPath, MultiThreadingHandler<FolderData> multiThreadingHandler)
        {
            folderData = FolderData.EmptyData;
            await GetFolderDataAsync(new DirectoryInfo(folderPath), multiThreadingHandler);
        }
        internal static async Task CopyFolderAsync(string sourcePath, string destPath, MultiThreadingHandler<CopyProgress> multiThreadingHandler)
        {
            copyProgress = CopyProgress.EmptyProgress;
            await CopyFolderAsync(new DirectoryInfo(sourcePath), destPath, multiThreadingHandler);
        }

        private static async Task GetFolderDataAsync(DirectoryInfo directory, MultiThreadingHandler<FolderData> multiThreadingHandler)
        {
            foreach (FileInfo file in directory.EnumerateFiles())
            {
                if (IgnoreCollection.IgnoreFile(file.FullName))
                {
                    continue;
                }
                folderData.NumberOfFiles++;
                long size = await Task.Run(() => file.Length);
                folderData.TotalSize += size;
                multiThreadingHandler.Progress.Report(folderData);
                multiThreadingHandler.CancellationToken.ThrowIfCancellationRequested();
            }
            System.Collections.Generic.List<Task> a = new System.Collections.Generic.List<Task>();
            foreach (DirectoryInfo subFolder in directory.EnumerateDirectories())
            {
                if (IgnoreCollection.IgnoreFolder(subFolder.FullName))
                {
                    continue;
                }
                folderData.NumberOfFolders++;
                multiThreadingHandler.Progress.Report(folderData);
                multiThreadingHandler.CancellationToken.ThrowIfCancellationRequested();
                a.Add(GetFolderDataAsync(subFolder, multiThreadingHandler));
            }
            await Task.WhenAll(a);
        }
        internal static async void DeleteEmptySubDirectoriesAsync(string path)
        {
            await DeleteEmptySubDirectoriesAsync(new DirectoryInfo(path));
        }

        private static async Task CopyFolderAsync(DirectoryInfo sourceDir, string destDirPath, MultiThreadingHandler<CopyProgress> multiThreadingHandler)
        {
            copyProgress.CurrentFolder = sourceDir.FullName;
            Directory.CreateDirectory(destDirPath);

            foreach (FileInfo file in sourceDir.EnumerateFiles())
            {
                if (IgnoreCollection.IgnoreFile(file.FullName))
                {
                    continue;
                }
                multiThreadingHandler.CancellationToken.ThrowIfCancellationRequested();
                string filePath = file.FullName;
                copyProgress.CopiedFilesStringBuilder.Append($"Copying {filePath}\n");
                await CopyFilAsync(sourceDir, destDirPath, multiThreadingHandler, filePath);
            }

            foreach (DirectoryInfo subFolder in sourceDir.EnumerateDirectories())
            {
                if (IgnoreCollection.IgnoreFolder(subFolder.FullName))
                {
                    continue;
                }
                multiThreadingHandler.CancellationToken.ThrowIfCancellationRequested();
                await CopyFolderAsync(subFolder, Path.Combine(destDirPath, subFolder.Name), multiThreadingHandler);
            }
            copyProgress.NumberOfFoldersCopied++;
            multiThreadingHandler.CancellationToken.ThrowIfCancellationRequested();
            multiThreadingHandler.Progress.Report(copyProgress);
        }
        private static async Task CopyFilAsync(DirectoryInfo sourceDir, string destDirPath, MultiThreadingHandler<CopyProgress> multiThreadingHandler, string filePath)
        {
            try
            {
                using (FileStream sourceStream = File.Open(filePath, FileMode.Open))
                {
                    using (FileStream destinationStream = File.Create(filePath.Replace(sourceDir.FullName, destDirPath)))
                    {
                        await sourceStream.CopyToAsync(destinationStream, 81920, multiThreadingHandler.CancellationToken);
                        copyProgress.CopiedFilesStringBuilder.Append("\t<color=green>Copied successfully :-)</color>\n\n");
                        copyProgress.NumberOfFilesCopied++;
                        multiThreadingHandler.Progress.Report(copyProgress);
                    }
                }
            }
            catch (System.Exception e)
            {
                copyProgress.CopiedFilesStringBuilder.Append("\t<color=red>File couldn't be copied… :-(</color>\n\n");
                UnityEngine.Debug.LogWarning($"Error:\t{filePath}\t{e.Message}");
            }
        }
        private static async Task DeleteEmptySubDirectoriesAsync(DirectoryInfo folder)
        {
            foreach (DirectoryInfo subFolder in folder.EnumerateDirectories())
            {
                await DeleteEmptySubDirectoriesAsync(subFolder);
                if (!subFolder.EnumerateFileSystemInfos().Any())
                {
                    subFolder.Delete();
                }
            }
        }
    }
}