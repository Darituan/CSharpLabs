using System;
using System.ComponentModel;
using System.Windows;
using Lab04.Models;
using Lab04.Tools;
using Lab04.Tools.Managers;
using Lab04.Tools.Navigation;

namespace Lab04.ViewModels
{
    public class AddEditViewModel: BaseViewModel
    {
        #region Fields
        private DateTime? _userEnteredBirthDate ;
        private string _userEnteredName;
        private string _userEnteredSurname;
        private string _userEnteredEMail;
        private readonly bool _add;
        private readonly string _modeName;
        private static readonly string AddModeName = "Add";
        private static readonly string EditModeName = "Edit";
        

        #region Commands
        private RelayCommand<object> _addEditCommand;
        private RelayCommand<object> _returnCommand;
        #endregion
        #endregion

        #region Properties

        public DateTime? UserEnteredBirthDate
        {
            get => _userEnteredBirthDate;
            set
            {
                _userEnteredBirthDate = value;
                OnPropertyChanged();
            }
        }
        
        public string UserEnteredName
        {
            get => _userEnteredName;
            set
            {
                _userEnteredName = value;
                OnPropertyChanged();
            }
        }

        public string UserEnteredSurname
        {
            get => _userEnteredSurname;
            set
            {
                _userEnteredSurname = value;
                OnPropertyChanged();
            }
        }

        public string UserEnteredEMail
        {
            get => _userEnteredEMail;
            set
            {
                _userEnteredEMail = value;
                OnPropertyChanged();
            }
        }

        public string ModeName => _modeName;

        #region Commands
        
        public RelayCommand<object> AddEditCommand
        {
            get
            {
                return _addEditCommand ??= new RelayCommand<object>(
                    AddEdit, o => CanAddOrEdit());
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

        #endregion
        #endregion

        private bool CanAddOrEdit()
        {
            return _userEnteredBirthDate != null && _userEnteredName != null && 
                   _userEnteredSurname != null && _userEnteredEMail != null;
        }

        private bool CanReturn() => true;
        

        private void AddEdit(object obj)
        {
            try
            {
                if (_add)
                {
                    AddUser();
                }
                else
                {
                    EditUser();
                }
                Return(null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void AddUser()
        {
            var newUser = new Person(_userEnteredName, _userEnteredSurname,
                _userEnteredEMail, _userEnteredBirthDate);
            StationManager.DataStorage.Users.Add(newUser);
        }

        private void EditUser()
        {
            StationManager.DataStorage.CurrentUser.Name = _userEnteredName;
            StationManager.DataStorage.CurrentUser.Surname = _userEnteredSurname;
            StationManager.DataStorage.CurrentUser.EMail = _userEnteredEMail;
            StationManager.DataStorage.CurrentUser.BirthDate = _userEnteredBirthDate;
        }

        private void Return(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
            StationManager.DataStorage.CurrentUser = null;
        }

        private void OnCurrentUserChanged(object obj, PropertyChangedEventArgs e)
        {
            if (_add)
            {
                UserEnteredName = null;
                UserEnteredSurname = null;
                UserEnteredEMail = null;
                UserEnteredBirthDate = null;
            }
            else if (StationManager.DataStorage.CurrentUser != null)
            {
                UserEnteredName = StationManager.DataStorage.CurrentUser.Name;
                UserEnteredSurname = StationManager.DataStorage.CurrentUser.Surname;
                UserEnteredEMail = StationManager.DataStorage.CurrentUser.EMail;
                UserEnteredBirthDate = StationManager.DataStorage.CurrentUser.BirthDate;
            }
        }

        public AddEditViewModel(bool add)
        {
            StationManager.DataStorage.PropertyChanged += OnCurrentUserChanged;
            _add = add;
            if (_add)
            {
                _modeName = AddModeName;
                return;
            }
            _modeName = EditModeName;
            UserEnteredName = StationManager.DataStorage.CurrentUser.Name;
            UserEnteredSurname = StationManager.DataStorage.CurrentUser.Surname;
            UserEnteredEMail = StationManager.DataStorage.CurrentUser.EMail;
            UserEnteredBirthDate = StationManager.DataStorage.CurrentUser.BirthDate;
        }
        
    }
}