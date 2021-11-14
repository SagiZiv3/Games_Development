using System;
using System.IO;

internal static class IOManager
{
    internal static string TempFolderPath => Path.GetTempPath();
    internal static string ProjectPath { get; }
    internal static string ProjectName { get; }

    static IOManager()
    {
        DirectoryInfo temp = new DirectoryInfo(UnityEngine.Application.dataPath).Parent;
        ProjectPath = temp.FullName;
        ProjectName = temp.Name;
    }
    internal static bool IsFolderExist(string folderPath)
    {
        return Directory.Exists(folderPath);
    }
    internal static string BytesToString(long byteCount)
    {
        byteCount = Math.Abs(byteCount);
        string[] suf = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" }; //Longs run out around EB
        if (byteCount == 0)
        {
            return "0" + suf[0];
        }
        int place = Convert.ToInt32(Math.Floor(Math.Log(byteCount, 1024)));
        double num = Math.Round(byteCount / Math.Pow(1024, place), 2);
        return string.Format("{0:#,0.00}", Math.Sign(byteCount) * num) + suf[place];
    }

    internal static string OpenFolderPicker()
    {
        return UnityEditor.EditorUtility.OpenFolderPanel("Select a folder", "", "");
    }

    internal static string ReadFile()
    {
        string path = GetFilePath();
        return File.ReadAllText(path);
    }

    private static string GetFilePath()
    {
        return UnityEditor.EditorUtility.OpenFilePanel("Select ignore file", string.Empty, "txt");
    }

    internal static string GenerateBackupPath(string folderPath)
    {
        if (folderPath.EndsWith("\\"))
        {
            return $"{folderPath}{ProjectName}_Backup";
        }
        return $"{folderPath}\\{ProjectName}_Backup";
    }
    internal static void GetFileNameAndExtension(string filePath, out string name, out string extension)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        name = fileInfo.Name.ToLower();
        extension = fileInfo.Extension.ToLower();
    }
    internal static string GetFolderName(string folderPath)
    {
        return new DirectoryInfo(folderPath).Name;
    }
}