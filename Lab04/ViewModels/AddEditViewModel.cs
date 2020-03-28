using System;
using System.Threading;
using System.Threading.Tasks;
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
        private string _modeName;
        

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
            StationManager.CurrentUser = newUser;
        }

        private void EditUser()
        {
            StationManager.CurrentUser.Name = _userEnteredName;
            StationManager.CurrentUser.Surname = _userEnteredSurname;
            StationManager.CurrentUser.EMail = _userEnteredEMail;
            StationManager.CurrentUser.BirthDate = _userEnteredBirthDate;
        }

        private void Return(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
            StationManager.CurrentUser = null;
        }

        public AddEditViewModel(bool add)
        {
            _add = add;
            if (_add)
            {
                _modeName = "Add";
                return;
            }
            _modeName = "Edit";
            UserEnteredName = StationManager.CurrentUser.Name;
            UserEnteredSurname = StationManager.CurrentUser.Surname;
            UserEnteredEMail = StationManager.CurrentUser.EMail;
            UserEnteredBirthDate = StationManager.CurrentUser.BirthDate;
        }
        
    }
}