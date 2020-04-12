using System.Collections.ObjectModel;
using Lab05.Tools;
using Lab05.Tools.Managers;
using Lab05.Tools.Sorting;

namespace Lab05.ViewModels
{
    internal class MainViewModel: BaseViewModel
    {
        private readonly ObservableCollection<PropertyGetter> _getters =
            PropertyGetterHelper.GetPropertyGetters(typeof(ProcessViewModel));

        private PropertyGetter _getter;
        
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

        public ProcessViewModel CurrentProcessViewModel
        {
            get => ProcessesManager.ProcessesInfo.CurrentProcess;
            set
            {
                ProcessesManager.ProcessesInfo.CurrentProcess = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ProcessesManager.ProcessesInfo.PropertyChanged += (sender, args) => OnPropertyChanged();
        }
    }
}