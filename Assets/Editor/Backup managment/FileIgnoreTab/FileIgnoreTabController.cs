namespace UnityBackupManagment
{
    internal class FileIgnoreTabController
    {
        private readonly IgnoreCollection ignoreCollection;
        public FileIgnoreTabController()
        {
            ignoreCollection = new IgnoreCollection();
            FolderProcessor.IgnoreCollection = ignoreCollection;
        }
        internal void UpdateIgnoreList(string filesToIgnore)
        {
            ignoreCollection.CastStringToHash(filesToIgnore);
        }
    }
}