using UnityEditor;
using UnityEngine;

namespace UnityBackupManagment
{
    internal class FileIgnoreTabUI : WindowTabUI
    {
        internal override string TabName => "Ignore files";

        private readonly FileIgnoreTabController controller;
        private string filesToIgnore;
        internal FileIgnoreTabUI(MainWindow mainWindow, FileIgnoreTabController controller) : base(mainWindow)
        {
            this.controller = controller;
            filesToIgnore = EditorPrefs.GetString($"{IOManager.ProjectName}_Backup_FilesToIgnore", string.Empty);
            controller.UpdateIgnoreList(filesToIgnore);
        }

        internal override void OnGUI()
        {
            StartScrollView(mainWindow.position.width - 10f, 450);
            filesToIgnore = GUILayout.TextArea(filesToIgnore).ToLower();
            StopScrollView();
            if (GUILayout.Button("Save"))
            {
                controller.UpdateIgnoreList(filesToIgnore);
            }
            if (GUILayout.Button("Read from file"))
            {
                filesToIgnore = IOManager.ReadFile();
                controller.UpdateIgnoreList(filesToIgnore);
            }
        }
        internal void OnDisable()
        {
            EditorPrefs.SetString($"{IOManager.ProjectName}_Backup_FilesToIgnore", filesToIgnore);
        }
    }
}
