using System;
using System.Collections.ObjectModel;
using Lab04.Models;
using Lab04.Tools;
using Lab04.Tools.Managers;

namespace Lab04.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Person> _users;
        
        
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

        public MainViewModel()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.Users);
        }
        
    }
}