using System;

namespace UnityBackupManagment
{
    internal class FileCounter
    {
        internal bool IsCounting { get; private set; } = false;

        private MultiThreadingHandler<FolderData> multiThreadingHandler;
        private EventHandler<FolderData>[] eventHandlers;

        internal void InitializeListeners(params EventHandler<FolderData>[] eventHandlers)
        {
            this.eventHandlers = eventHandlers;
            multiThreadingHandler = new MultiThreadingHandler<FolderData>();
            RegisterListeners(eventHandlers);
        }
        internal async void StartCount()
        {
            try
            {
                IsCounting = true;
                await FolderProcessor.GetFolderDataAsync(IOManager.ProjectPath, multiThreadingHandler);
            }
            catch (OperationCanceledException) { }
            IsCounting = false;
        }
        internal void OnDisable()
        {
            StopCount();
        }
        internal void StopCount()
        {
            multiThreadingHandler.Cancel();
        }

        private void RegisterListeners(EventHandler<FolderData>[] eventHandlers)
        {
            foreach (EventHandler<FolderData> eventHandler in eventHandlers)
            {
                multiThreadingHandler.AddEventToProgressReport(eventHandler);
            }
        }
        private void DeRegisterListeners()
        {
            foreach (EventHandler<FolderData> eventHandler in eventHandlers)
            {
                multiThreadingHandler.RemoveEventFromProgressReport(eventHandler);
            }
        }

    }
}