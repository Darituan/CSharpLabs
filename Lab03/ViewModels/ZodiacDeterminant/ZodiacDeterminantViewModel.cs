using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab03.Tools;
using Lab03.Tools.Managers;
using Lab03.Models;

namespace Lab03.ViewModels.ZodiacDeterminant
{
    internal class ZodiacDeterminantViewModel: BaseViewModel
    {
        #region Fields
        private DateTime? _userEnteredBirthDate ;
        private string _userEnteredName;
        private string _userEnteredSurname;
        private string _userEnteredEMail;

        private string _userName;
        private string _userSurname;
        private string _userEMail;
        private string _userBirthDate;
        private string _userIsAdult;
        private string _userIsBirthday;
        private string _userWesternZodiac;
        private string _userChineseZodiac;

        #region Commands
        private RelayCommand<object> _showDateInfoCommand;
        private RelayCommand<object> _closeCommand;
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
        
        
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string UserSurname
        {
            get => _userSurname;
            set
            {
                _userSurname = value;
                OnPropertyChanged();
            }
        }

        public string UserEMail
        {
            get => _userEMail;
            set
            {
                _userEMail = value;
                OnPropertyChanged();
            }
        }

        public string UserBirthDate
        {
            get => _userBirthDate;
            set
            {
                _userBirthDate = value;
                OnPropertyChanged();
            }
        }
        
        public string UserIsAdult
        {
            get => _userIsAdult;
            set
            {
                _userIsAdult = value;
                OnPropertyChanged();
            }
        }
        
        public string UserWesternZodiac
        {
            get => _userWesternZodiac;
            set
            {
                _userWesternZodiac = value;
                OnPropertyChanged();
            }
        }
        
        public string UserChineseZodiac
        {
            get => _userChineseZodiac;
            set
            {
                _userChineseZodiac = value;
                OnPropertyChanged();
            }
        }
        
        public string UserIsBirthday
        {
            get => _userIsBirthday;
            set
            {
                _userIsBirthday = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand<object> ShowDateInfoCommand
        {
            get
            {
                return _showDateInfoCommand ??= new RelayCommand<object>(
                    ShowDateInfoImplementation, o => CanExecuteCommand());
            }
        }

        public RelayCommand<object> CloseCommand => 
            _closeCommand ??= new RelayCommand<object>(o => Environment.Exit(0));

        #endregion
        #endregion

        private bool CanExecuteCommand()
        {
            return _userEnteredBirthDate != null && _userEnteredName != null && 
                   _userEnteredSurname != null && _userEnteredEMail != null;
        }

        private void ShowDateInfo()
        {
            Thread.Sleep(2000);
            try
            {
                var user = new Person(UserEnteredName, UserEnteredSurname, 
                    UserEnteredEMail, UserEnteredBirthDate);
                if (user.IsAdult.HasValue)
                    UserIsAdult = "Is Adult: " + user.IsAdult.Value;
                else UserIsAdult = "Is Adult: unknown";
                UserWesternZodiac = user.SunSign.HasValue ? 
                    Person.GetDescription(user.SunSign.Value) : "Western Zodiac Sign: unknown";
                UserChineseZodiac = user.ChineseSign.HasValue ? 
                    Person.GetDescription(user.ChineseSign.Value) : "Chinese Zodiac Sign: unknown";
                if (user.IsBirthday.HasValue)
                {
                    if (user.IsBirthday.Value)
                    {
                        UserIsBirthday = "Is Birthday: true";
                        MessageBox.Show("Happy Birthday! May all your dreams come true!");
                    }
                    else UserIsBirthday = "Is Birthday: false";
                }
                else UserIsBirthday = "Is Birthday: unknown";
                UserName = $"Name: {user.Name}";
                UserSurname = $"Surname: {user.Surname}";
                UserEMail = $"e-mail: {user.EMail}";
                UserBirthDate = user.BirthDate.HasValue ? 
                    $"Birth Date: {user.BirthDate.Value.Day}.{user.BirthDate.Value.Month}.{user.BirthDate.Value.Year}": 
                    "Birth Date: unknown";
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void ShowDateInfoImplementation(object obj)
        {
            UserName = string.Empty;
            UserSurname = string.Empty;
            UserEMail = string.Empty;
            UserBirthDate = string.Empty;
            UserIsBirthday = string.Empty;
            UserIsAdult = string.Empty;
            UserWesternZodiac = string.Empty;
            UserChineseZodiac = string.Empty;
            LoaderManager.Instance.ShowLoader();
            await Task.Run(ShowDateInfo);
            LoaderManager.Instance.HideLoader();
        }
    }
}