namespace UnityBackupManagment
{
    using UnityEditor;
    using UnityEngine;
    internal class BackupTabUI : WindowTabUI
    {
        private static readonly GUIStyle BoldRichTextStyle = new GUIStyle { richText = true, fontStyle = FontStyle.Bold };
        private readonly BackupTabController controller;
        private readonly FileCounter fileCounter;

        internal override string TabName => "Backup";
        private string folderDataText, backupProgressText, filesCopied;

        internal BackupTabUI(MainWindow mainWindow, BackupTabController controller, FileCounter fileCounter) : base(mainWindow)
        {
            this.controller = controller;
            this.fileCounter = fileCounter;
            UpdateFolderData(this, FolderData.EmptyData);
            ResetVariables();
        }
        internal override void OnGUI()
        {
            ShowPathData();
            ShowProjectFolderData();
            ShowProgress();
            ShowBottomButtons();
            ShowToggles();
        }
        internal void UpdateFolderData(object sender, FolderData folderData)
        {
            folderDataText = folderData.ToString();
            mainWindow.Repaint();
        }
        internal void UpdateBackupProgress(object _, CopyProgress backupProgress)
        {
            backupProgressText = backupProgress.ToString();
            filesCopied = backupProgress.CopiedFilesString;
            mainWindow.Repaint();
            scrollPosition.y = Mathf.Infinity;
        }

        #region Private GUI functions
        private void ShowPathData()
        {
            GUILayout.Label("Current project path:\t" + IOManager.ProjectPath);
            GUILayout.BeginHorizontal("Box");
            controller.BackupPath = EditorGUILayout.TextField("Backup path: ", controller.BackupPath);
            bool doesFolderExist = IOManager.IsFolderExist(controller.BackupPath);
            ShowPathButtons(doesFolderExist);
            GUILayout.EndHorizontal();
            if (doesFolderExist)
            {
                EditorGUILayout.HelpBox("The folder you want to backup to already exists", MessageType.Info);
            }
        }
        private void ShowPathButtons(bool doesFolderExist)
        {
            if (GUILayout.Button("Browse", GUILayout.Width(70)))
            {
                string temp;
                if (!string.IsNullOrEmpty(temp = IOManager.OpenFolderPicker()))
                {
                    controller.BackupPath = IOManager.GenerateBackupPath(temp);
                }
            }
            if (doesFolderExist && GUILayout.Button("Open", GUILayout.Width(70)))
            {
                FolderProcessor.OpenFolderInFileExplorer(controller.BackupPath);
            }
        }
        private void ShowProjectFolderData()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            GUILayout.Label(folderDataText);
            GUILayout.EndHorizontal();
        }
        private void ShowProgress()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Label(backupProgressText);
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            ShowFilesCopiedScrollView();
        }
        private void ShowFilesCopiedScrollView()
        {
            StartScrollView(mainWindow.position.width - 20, 450f);
            GUILayout.Label(filesCopied, BoldRichTextStyle);
            StopScrollView();
        }
        private void ShowBottomButtons()
        {
            GUILayout.BeginHorizontal();
            ShowBackupButton();
            GUI.enabled = !controller.IsBackingUp;
            ShowClearConsoleButton();
            GUI.enabled = !fileCounter.IsCounting;
            if (GUILayout.Button("Refresh file & folder counter"))
            {
                fileCounter.StartCount();
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
        }
        private void ShowClearConsoleButton()
        {
            if (GUILayout.Button("Clear console"))
            {
                ResetVariables();
            }
        }
        private void ShowBackupButton()
        {
            if (!controller.IsBackingUp && GUILayout.Button("Create backup"))
            {
                controller.OnBackupEnded += Controller_OnBackupEnded;
                controller.StartBackup();
            }
            else if (controller.IsBackingUp && GUILayout.Button("Stop backup"))
            {
                controller.StopBackup();
            }
        }

        private void Controller_OnBackupEnded(bool didBackupStopped)
        {
            controller.OnBackupEnded -= Controller_OnBackupEnded;
            if (didBackupStopped)
            {
                EditorUtility.DisplayDialog("Backup unity project", "The backup has stopped", "O.K.");
                filesCopied += "\n\n<color=red>!BACKUP STOPPED!</color>";
            }
            else
            {
                EditorUtility.DisplayDialog("Backup unity project", "The backup ended successfully", "O.K.");
                filesCopied += "\n\n<color=green>!BACKUP SUCCEEDED!</color>";
            }

        }

        private void ShowToggles()
        {
            GUILayout.BeginHorizontal();
            controller.OpenFolderAfterBackup = GUILayout.Toggle(controller.OpenFolderAfterBackup, "Open folder after backup is completed");
            GUI.enabled = IOManager.IsFolderExist(controller.BackupPath);
            controller.DeleteOldBackup = GUILayout.Toggle(controller.DeleteOldBackup, "Delete the old backup when creating a new one");
            GUI.enabled = true;
            controller.DeleteEmptyDirectoriesFromBackupFolder = GUILayout.Toggle(controller.DeleteEmptyDirectoriesFromBackupFolder, "Delete empty directories from backup folder");
            GUILayout.EndHorizontal();
        }
        #endregion
        private void ResetVariables()
        {
            filesCopied = string.Empty;
            UpdateBackupProgress(this, CopyProgress.EmptyProgress);
        }
    }
}