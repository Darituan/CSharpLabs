using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Lab05.Tools;
using Lab05.Tools.Managers;
using Lab05.Tools.Navigation;
using Lab05.Tools.Sorting;

namespace Lab05.ViewModels
{
    internal class MainViewModel: BaseViewModel
    {
        private readonly ObservableCollection<PropertyGetter> _getters =
            PropertyGetterHelper.GetPropertyGetters(typeof(ProcessViewModel));

        private PropertyGetter _getter;
        
        private RelayCommand<object> _killCommand;
        private RelayCommand<object> _showCommand;
        private RelayCommand<object> _threadsCommand;
        private RelayCommand<object> _modulesCommand;
        private RelayCommand<object> _sortCommand;
        
        public RelayCommand<object> KillCommand
        {
            get
            {
                return _killCommand ??= new RelayCommand<object>(
                    Kill, o => CanKill());
            }
        }
        
        public RelayCommand<object> ShowCommand
        {
            get
            {
                return _showCommand ??= new RelayCommand<object>(
                    Show, o => CanShow());
            }
        }
        
        public RelayCommand<object> ModulesCommand
        {
            get
            {
                return _modulesCommand ??= new RelayCommand<object>(
                    ShowModules, o => CanShowModules());
            }
        }
        
        public RelayCommand<object> ThreadsCommand
        {
            get
            {
                return _threadsCommand ??= new RelayCommand<object>(
                    ShowThreads, o => CanShowThreads());
            }
        }
        
        public RelayCommand<object> SortCommand
        {
            get
            {
                return _sortCommand ??= new RelayCommand<object>(
                    Sort, o => CanSort());
            }
        }

        public ObservableCollection<PropertyGetter> Getters => _getters;

        public PropertyGetter Getter
        {
            get => _getter;
            set
            {
                _getter = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<ProcessViewModel> Processes
        {
            get => ProcessesManager.ProcessesInfo.Processes;
            set
            {
                ProcessesManager.ProcessesInfo.Processes = value;
                OnPropertyChanged();
            }
        }

        public ProcessViewModel CurrentProcess
        {
            get => ProcessesManager.ProcessesInfo.CurrentProcess;
            set
            {
                ProcessesManager.ProcessesInfo.CurrentProcess = value;
                OnPropertyChanged();
            }
        }

        private bool CanKill()
        {
            return CurrentProcess != null;
        }
        
        private bool CanShow()
        {
            return CurrentProcess != null;
        }
        
        private bool CanShowModules()
        {
            return CurrentProcess != null;
        }
        
        private bool CanShowThreads()
        {
            return CurrentProcess != null;
        }
        
        private bool CanSort()
        {
            return Getter != null;
        }

        private async void Sort(object o)
        {
            await Task.Run((() =>
            {
                Processes = Sorter.SortProcesses(Processes, Getter.Getter);
            }));
        }

        private void Kill(object o)
        {
            CurrentProcess.Process.Kill();
            CurrentProcess = null;
        }

        private void Show(object o)
        {
            var startInfo = new ProcessStartInfo()
            {
                Arguments = CurrentProcess.StartFileName,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        private void ShowModules(object o)
        {
            ProcessesManager.ProcessesInfo.UpdateCurrent();
            NavigationManager.Instance.Navigate(ViewType.Modules);
        }
        
        private void ShowThreads(object o)
        {
            ProcessesManager.ProcessesInfo.UpdateCurrent();
            NavigationManager.Instance.Navigate(ViewType.Threads);
        }

        public MainViewModel()
        {
            ProcessesManager.ProcessesInfo.PropertyChanged += (sender, args) => OnPropertyChanged();
        }
    }
}