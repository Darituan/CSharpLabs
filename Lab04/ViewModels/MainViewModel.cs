using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Lab04.Models;
using Lab04.Tools;
using Lab04.Tools.Managers;
using Lab04.Tools.Navigation;
using Lab04.Tools.SortAndFilter;

namespace Lab04.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Person> _users;
        
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;

        public ObservableCollection<Person> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public Person CurrentUser
        {
            get => StationManager.CurrentUser;
            set => StationManager.CurrentUser = value;
        }
        
        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _deletePersonCommand ??= new RelayCommand<object>(
                    DeletePerson, o => CanDeletePerson());
            }
        }
        
        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPersonCommand ??= new RelayCommand<object>(
                    AddPerson, o => CanAddPerson());
            }
        }
        
        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _editPersonCommand ??= new RelayCommand<object>(
                    EditPerson, o => CanEditPerson());
            }
        }


        private bool CanAddPerson()
        {
            return true; 
        }
        
        private bool CanEditPerson()
        {
            return CurrentUser != null; 
        }
        
        private bool CanDeletePerson()
        {
            return CurrentUser != null; 
        }
        

        private void DeletePerson(object obj)
        {
            StationManager.DataStorage.Users.Remove(CurrentUser);
            Users.Remove(CurrentUser);
            CurrentUser = null;
        }

        private void AddPerson(object obj)
        {
            StationManager.CurrentUser = null;
            NavigationManager.Instance.Navigate(ViewType.Add);
        }
        
        private void EditPerson(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Edit);
        }

        public MainViewModel()
        {
            _users = StationManager.DataStorage.Users;
        }
        
    }
}