using System.Collections.ObjectModel;
using System.Diagnostics;
using Lab05.Tools;
using Lab05.Tools.Managers;
using Lab05.Tools.Navigation;

namespace Lab05.ViewModels
{
    public class ThreadsViewModel: BaseViewModel
    {
        private RelayCommand<object> _returnCommand;
        
        public ObservableCollection<ProcessThread> Threads
        {
            get => ProcessesManager.ProcessesInfo.CurrentThreads;
            set
            {
                ProcessesManager.ProcessesInfo.CurrentThreads = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand<object> DeletePersonCommand
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
        
        internal ThreadsViewModel()
        {
            ProcessesManager.ProcessesInfo.PropertyChanged += (sender, args) => OnPropertyChanged();
        }
    }
}