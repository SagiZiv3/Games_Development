namespace UnityBackupManagment
{
    using System.Text;
    internal struct CopyProgress
    {
        internal static CopyProgress EmptyProgress => new CopyProgress { CopiedFilesStringBuilder = new StringBuilder(), NumberOfFilesCopied = 0, NumberOfFoldersCopied = 0, CurrentFolder = string.Empty };

        internal StringBuilder CopiedFilesStringBuilder { get; private set; }
        internal string CopiedFilesString => CopiedFilesStringBuilder.ToString();
        internal uint NumberOfFoldersCopied { get; set; }
        internal uint NumberOfFilesCopied { get; set; }
        internal string CurrentFolder { get; set; }

        public override string ToString()
        {
            string numberOfFilesString = NumberOfFilesCopied.GetFormattedNumber();
            string numberOfFoldersString = NumberOfFoldersCopied.GetFormattedNumber();
            return $"Progress:\nFiles copied: {numberOfFilesString},\tFolders copied: {numberOfFoldersString}\nCurrent folder: {CurrentFolder}";
        }
    }
}