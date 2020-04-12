using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using Lab05.ViewModels;

namespace Lab05.Tools.Managers
{
    internal static class UpdateManager
    {
        internal static readonly object Locker = new object();
        private static void UpdateCollection()
        {
            while (true)
            {
                Thread.Sleep(4000);
                lock (Locker)
                {
                    var allProcArr = Process.GetProcesses();
                    var allProc = from process in allProcArr 
                        select new ProcessViewModel(process);
                    var processViewModels = allProc as ProcessViewModel[] ?? allProc.ToArray();
                    var toBeDeleted = ProcessManager.ProcessesInfo.Processes.Except(processViewModels);
                    var toBeAdded = processViewModels.Except(ProcessManager.ProcessesInfo.Processes);
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        foreach (var process in toBeDeleted)
                        {
                            ProcessManager.ProcessesInfo.Processes.Remove(process);
                        }
                        foreach (var process in toBeAdded)
                        {
                            ProcessManager.ProcessesInfo.Processes.Add(process);
                        }
                    });
                }
            }
        }

        private static void UpdateMeta()
        {
            while (true)
            {
                Thread.Sleep(1500);
                lock (Locker)
                {
                    foreach (var process in ProcessManager.ProcessesInfo.Processes)
                    {
                        process.Refresh();
                    }

                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        ProcessManager.ProcessesInfo.Processes[0].RefreshAndNotify();
                        ProcessManager.ProcessesInfo.UpdateCurrent();
                    });
                }
            }
        }

        internal static void Initialize()
        {
            new Thread(UpdateCollection).Start();
            new Thread(UpdateMeta).Start();
        }
    }
}