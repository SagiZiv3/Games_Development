namespace UnityBackupManagment
{
    internal class BackupTabManager
    {
        public BackupTabUI UI { get; }
        private readonly BackupTabController controller;
        private readonly FileCounter fileCounter;
        internal BackupTabManager(MainWindow mainWindow)
        {
            controller = new BackupTabController();
            fileCounter = new FileCounter();
            UI = new BackupTabUI(mainWindow, controller, fileCounter);

            controller.InitializeListeners(UI.UpdateBackupProgress);
            fileCounter.InitializeListeners(UI.UpdateFolderData);

            fileCounter.StartCount();
        }
        internal void OnDisable()
        {
            controller.OnDisable();
            fileCounter.OnDisable();
        }
    }
}
