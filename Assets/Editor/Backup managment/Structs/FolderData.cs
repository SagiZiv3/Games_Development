namespace UnityBackupManagment
{
    internal struct FolderData
    {
        internal static FolderData EmptyData => new FolderData { NumberOfFiles = 0, NumberOfFolders = 0, TotalSize = 0 };
        internal uint NumberOfFolders { get; set; }
        internal uint NumberOfFiles { get; set; }
        internal long TotalSize { get; set; }

        public override string ToString()
        {
            string numberOfFilesString = NumberOfFiles.GetFormattedNumber();
            string numberOfFoldersString = NumberOfFolders.GetFormattedNumber();
            string totalFolderSizeString = IOManager.BytesToString(TotalSize);
            return $"Number of files:\t{numberOfFilesString}\nNumber of folders:\t{numberOfFoldersString}\nTotal folder size:\t{totalFolderSizeString}";
        }
    }
}