using System.Collections.ObjectModel;
using System.Diagnostics;
using Lab05.Tools;
using Lab05.Tools.Managers;
using Lab05.Tools.Navigation;

namespace Lab05.ViewModels
{
    public class ModulesViewModel: BaseViewModel
    {
        private RelayCommand<object> _returnCommand;
        
        public ObservableCollection<ProcessModule> Modules
        {
            get => ProcessesManager.ProcessesInfo.CurrentModules;
            set
            {
                ProcessesManager.ProcessesInfo.CurrentModules = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand<object> ReturnCommand
        {
            get
            {
                return _returnCommand ??= new RelayCommand<object>(
                    Return, o => CanReturn());
            }
        }

        private bool CanReturn()
        {
            return true;
        }

        private void Return(object o)
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
        }

        internal ModulesViewModel()
        {
            ProcessesManager.ProcessesInfo.PropertyChanged += (sender, args) => OnPropertyChanged();
        }
    }
}