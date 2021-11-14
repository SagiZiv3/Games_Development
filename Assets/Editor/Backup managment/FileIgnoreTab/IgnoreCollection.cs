namespace UnityBackupManagment
{
    using System;
    using System.Collections.Generic;
    internal class IgnoreCollection
    {
        private readonly HashSet<string> toIgnore;
        internal IgnoreCollection()
        {
            toIgnore = new HashSet<string>();
        }
        internal void CastStringToHash(string list)
        {
            toIgnore.Clear();
            if (string.IsNullOrWhiteSpace(list))
            {
                return;
            }
            //list = list.ToLower();
            string[] lines = list.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string line;
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Trim();
                if (!toIgnore.Contains(line))
                {
                    toIgnore.Add(line);
                }
            }
        }
        internal bool IgnoreFile(string filePath)
        {
            IOManager.GetFileNameAndExtension(filePath, out string name, out string extension);
            return toIgnore.Contains(name) || !string.IsNullOrEmpty(extension) && toIgnore.Contains($"*{extension}");
        }
        internal bool IgnoreFolder(string folderPath)
        {
            return toIgnore.Contains($"{IOManager.GetFolderName(folderPath).ToLower()}/");
        }
    }
}