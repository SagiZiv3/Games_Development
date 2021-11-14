namespace UnityBackupManagment
{
    internal class FileIgnoreTabManager
    {
        internal FileIgnoreTabUI UI { get; }
        internal FileIgnoreTabController Controller { get; }
        internal FileIgnoreTabManager(MainWindow mainWindow)
        {
            Controller = new FileIgnoreTabController();
            UI = new FileIgnoreTabUI(mainWindow, Controller);
        }
        internal void OnDisable()
        {
            UI.OnDisable();
        }
    }
}