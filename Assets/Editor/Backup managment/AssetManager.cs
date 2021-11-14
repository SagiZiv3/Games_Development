using UnityEditor;
using UnityEngine;

namespace UnityBackupManagment
{
    internal class AssetManager
    {
        private static BackupTabManager backupTabManager;
        private static FileIgnoreTabManager ignoreTabManager;
        private static MainWindow mainWindow;

        [MenuItem("File/Create backup %&b")]
        private static void Main()
        {
            if (mainWindow == null)
            {
                mainWindow = EditorWindow.GetWindow<MainWindow>();
                mainWindow.titleContent = new GUIContent("Backup Unity project");
                InitializeTabManagers(mainWindow);
                mainWindow.Initialize(backupTabManager.UI, ignoreTabManager.UI);
                mainWindow.minSize = new Vector2(475, 700);
                mainWindow.OnDisableEvent += OnDisable;
            }
            mainWindow.Show();
        }

        internal static void OnDisable()
        {
            backupTabManager.OnDisable();
            ignoreTabManager.OnDisable();
            mainWindow.OnDisableEvent -= OnDisable;
            mainWindow = null;
        }

        private static void InitializeTabManagers(MainWindow mainWindow)
        {
            ignoreTabManager = new FileIgnoreTabManager(mainWindow);
            backupTabManager = new BackupTabManager(mainWindow);
        }
    }
}
