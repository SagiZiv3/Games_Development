using System;
using System.Threading;
namespace UnityBackupManagment
{
    internal class MultiThreadingHandler<T>
    {
        internal CancellationToken CancellationToken => cancellationTokenSource.Token;
        internal IProgress<T> Progress => progress as IProgress<T>;

        private Progress<T> progress;
        private CancellationTokenSource cancellationTokenSource;

        internal MultiThreadingHandler()
        {
            progress = new Progress<T>();
            cancellationTokenSource = new CancellationTokenSource();
        }
        internal void AddEventToProgressReport(EventHandler<T> action)
        {
            progress.ProgressChanged += action;
        }
        internal void RemoveEventFromProgressReport(EventHandler<T> action)
        {
            progress.ProgressChanged -= action;
        }
        internal void Cancel()
        {
            cancellationTokenSource.Cancel();
        }
        internal void ResetCancellationToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }
    }
}