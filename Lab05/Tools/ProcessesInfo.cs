using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Lab05.ViewModels;

namespace Lab05.Tools
{
    internal class ProcessesInfo: INotifyPropertyChanged
    {
        private ObservableCollection<ProcessViewModel> _processes;

        private ProcessViewModel _currentProcess;

        private ObservableCollection<ProcessThread> _currentThreads;
        
        private ObservableCollection<ProcessModule> _currentModules;


        public ObservableCollection<ProcessViewModel> Processes
        {
            get => _processes;
            set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }

        public ProcessViewModel CurrentProcess
        {
            get => _currentProcess;
            set
            {
                _currentProcess = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProcessThread> CurrentThreads
        {
            get => _currentThreads;
            set
            {
                _currentThreads = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<ProcessModule> CurrentModules
        {
            get => _currentModules;
            set
            {
                _currentModules = value;
                OnPropertyChanged();
            }
        }

        internal void UpdateCurrent()
        {
            if (CurrentProcess != null)
            {
                var threads = CurrentProcess.Process.Threads;
                CurrentThreads = new ObservableCollection<ProcessThread>(
                    from object? thread in threads select thread as ProcessThread);
                var modules = CurrentProcess.Process.Modules;
                CurrentModules = new ObservableCollection<ProcessModule>(
                    from object? module in modules select module as ProcessModule);
            }
            else
            {
                CurrentThreads = null;
                CurrentModules = null;
            }
        }

        internal ProcessesInfo()
        {
            var allProc = Process.GetProcesses();
            _processes = new ObservableCollection<ProcessViewModel>();
            foreach (var process in allProc)
            {
                try
                {
                    var processViewModel = new ProcessViewModel(process);
                    _processes.Add(processViewModel);
                }
                catch (Exception e)
                {
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}