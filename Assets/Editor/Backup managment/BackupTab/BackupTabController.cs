using System;
using UnityEditor.SceneManagement;

namespace UnityBackupManagment
{
    internal class BackupTabController
    {
        #region Properties
        internal string BackupPath { get; set; }
        internal bool OpenFolderAfterBackup { get; set; }
        internal bool DeleteOldBackup { get; set; }
        internal bool DeleteEmptyDirectoriesFromBackupFolder { get; set; }
        internal bool IsBackingUp { get; private set; }
        #endregion
        public event Action<bool> OnBackupEnded;
        private MultiThreadingHandler<CopyProgress> multiThreadingHandler;
        private EventHandler<CopyProgress>[] eventHandlers;
        private bool didBackupStopped;
        internal BackupTabController()
        {
            BackupPath = UnityEditor.EditorPrefs.GetString($"{IOManager.ProjectName}_BackupPath",
                IOManager.GenerateBackupPath(IOManager.TempFolderPath));
            OpenFolderAfterBackup = DeleteEmptyDirectoriesFromBackupFolder = true;
            DeleteOldBackup = false;
            IsBackingUp = didBackupStopped = false;
        }
        internal void InitializeListeners(params EventHandler<CopyProgress>[] eventHandlers)
        {
            this.eventHandlers = eventHandlers;
            multiThreadingHandler = new MultiThreadingHandler<CopyProgress>();
            RegisterListeners(eventHandlers);
        }
        internal void OnDisable()
        {
            DeRegisterListeners();
            StopBackup();
            UnityEditor.EditorPrefs.SetString($"{IOManager.ProjectName}_BackupPath", BackupPath);
        }
        internal async void StartBackup()
        {
            try
            {
                EditorSceneManager.SaveOpenScenes();
                if (DeleteOldBackup)
                {
                    FolderProcessor.DeleteFolder(BackupPath);
                }
                IsBackingUp = true;
                multiThreadingHandler.ResetCancellationToken();
                await FolderProcessor.CopyFolderAsync(IOManager.ProjectPath, BackupPath, multiThreadingHandler);
            }
            catch (OperationCanceledException)
            {
                UnityEngine.Debug.LogWarning("Backup stopped!");
            }
            StartPostBackupActions();
        }

        private void StartPostBackupActions()
        {
            OnBackupEnded?.Invoke(didBackupStopped);
            IsBackingUp = false;
            if (!didBackupStopped)
            {
                if (DeleteEmptyDirectoriesFromBackupFolder)
                {
                    FolderProcessor.DeleteEmptySubDirectoriesAsync(BackupPath);
                }
                if (OpenFolderAfterBackup)
                {
                    FolderProcessor.OpenFolderInFileExplorer(BackupPath);
                }
            }
            didBackupStopped = false;
        }

        internal void StopBackup()
        {
            multiThreadingHandler.Cancel();
            didBackupStopped = true;
        }

        private void RegisterListeners(EventHandler<CopyProgress>[] eventHandlers)
        {
            foreach (EventHandler<CopyProgress> eventHandler in eventHandlers)
            {
                multiThreadingHandler.AddEventToProgressReport(eventHandler);
            }
        }
        private void DeRegisterListeners()
        {
            foreach (EventHandler<CopyProgress> eventHandler in eventHandlers)
            {
                multiThreadingHandler.RemoveEventFromProgressReport(eventHandler);
            }
        }
    }
}
