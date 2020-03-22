using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Lab04.Models;
using Lab04.Tools;
using Lab04.Tools.Managers;

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
            StationManager.DataStorage.DeleteUser(CurrentUser);
            Users.Remove(CurrentUser);
            CurrentUser = null;
        }

        public MainViewModel()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.Users);
        }
        
    }
}