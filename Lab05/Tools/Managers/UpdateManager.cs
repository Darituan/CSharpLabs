using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Lab05.ViewModels;

namespace Lab05.Tools.Managers
{
    internal static class UpdateManager
    {
        internal static Thread CollectionUpdater;
        internal static Thread MetaUpdater;
        internal static bool Stop = false;
        internal static bool UpdatingCollection;
        internal static bool UpdatingMeta;
        internal static readonly object Locker = new object();
        private static void UpdateCollection()
        {
            while (!Stop)
            {
                Thread.Sleep(2000);
                UpdatingCollection = true;
                lock (Locker)
                {
                    var allProcArr = Process.GetProcesses();
                    var allProc = new List<ProcessViewModel>();
                    foreach (var process in allProcArr)
                    {
                        try
                        {
                            var processViewModel = new ProcessViewModel(process);
                            allProc.Add(processViewModel);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    
                    var toBeDeleted = new List<ProcessViewModel>();
                    var check = "";
                    foreach (var process in ProcessesManager.ProcessesInfo.Processes)
                    {
                        try
                        {
                            check = process.Process.ProcessName;
                        }
                        catch (Exception e)
                        {
                            toBeDeleted.Add(process);
                        }
                    }
                    
                    var toBeAdded = new List<ProcessViewModel>();
                    foreach (var process in allProc)
                    {
                        var add = true;
                        foreach (var oldProcess in ProcessesManager.ProcessesInfo.Processes)
                        {
                            if (process.Id == oldProcess.Id)
                                add = false;
                        }
                        if (add) toBeAdded.Add(process);
                    }

                    try
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            
                            foreach (var process in toBeDeleted)
                            {
                                ProcessesManager.ProcessesInfo.Processes.Remove(process);
                            }
                            
                            foreach (var process in toBeAdded)
                            {
                                ProcessesManager.ProcessesInfo.Processes.Add(process);
                            }
                            
                        });
                    }
                    catch (Exception e)
                    {
                    }
                }
                UpdatingCollection = false;
            }
        }

        private static void UpdateMeta()
        {
            //ProcessesManager.ProcessesInfo.Processes.CollectionChanged +=
            //    (sender, args) => Console.WriteLine(args.Action);
            while (!Stop)
            {
                Thread.Sleep(500);
                UpdatingMeta = true;
                lock (Locker)
                {
                    var exited = new List<ProcessViewModel>();
                    foreach (var process in ProcessesManager.ProcessesInfo.Processes)
                    {
                        try
                        {
                            process.RefreshAndNotify();
                        }
                        catch (Exception e)
                        {
                            exited.Add(process);
                        }
                    }

                    try
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                ProcessesManager.ProcessesInfo.UpdateCurrent();
                            }
                            catch (Exception e)
                            {
                                ProcessesManager.ProcessesInfo.CurrentProcess = null;
                            }

                            foreach (var process in exited)
                            {
                                ProcessesManager.ProcessesInfo.Processes.Remove(process);
                            }
                        
                        });
                    }
                    catch (Exception e)
                    {
                    }
                }
                UpdatingMeta = false;
            }
        }

        internal static void Initialize()
        {
            CollectionUpdater = new Thread(UpdateCollection);
            MetaUpdater = new Thread(UpdateMeta);
            CollectionUpdater.Start();
            MetaUpdater.Start();
        }
    }
}