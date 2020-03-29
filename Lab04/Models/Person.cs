using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Lab04.Exceptions;
using Lab04.Enums;
using System.Text.RegularExpressions;

namespace Lab04.Models
{
    [Serializable]
    internal class Person: INotifyPropertyChanged
    {
        
        #region Fields
        // the first year all dates of which work correctly in ChineseLunisolarCalendar
        private static readonly int AnchorYear = 1902;
        private static readonly ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();
        private static readonly Regex EmailRegex = 
            new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        private static readonly Regex NameRegex = new Regex(@"^\w+([-' ]\w+)*$");
        private string _name;
        private string _surname;
        private string _eMail;
        private bool? _isAdult;
        private bool? _hasBirthdayToday;
        private DateTime? _birthDate;
        private SunSigns? _sunSign;
        private ChineseSigns? _chineseSign;

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                CheckNameString(value);
                _name = value;
                OnPropertyChanged();
            }
        }
        
        public string Surname
        {
            get => _surname;
            set
            {
                CheckSurnameString(value);
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string EMail
        {
            get => _eMail;
            set
            {
                CheckEMail(value);
                _eMail = value;
                OnPropertyChanged();
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                CheckBirthDate(value);
                _birthDate = value;
                ChangeDependent();
                OnPropertyChanged();
            }
        }

        public SunSigns? SunSign => _sunSign;

        public ChineseSigns? ChineseSign => _chineseSign;

        public bool? IsAdult => _isAdult;

        public bool? HasBirthdayToday => _hasBirthdayToday;

        #endregion

        // each month has two western zodiac signs in it
        // this method returns the last day of the first zodiac sign of month passed to it
        private int GetDividingDay(int monthNumber)
        {
            return monthNumber switch
            {
                0 => 20,
                1 => 18,
                2 => 20,
                3 => 20,
                4 => 20,
                5 => 21,
                6 => 22,
                7 => 23,
                8 => 23,
                9 => 23,
                10 => 22,
                11 => 21,
                _ => throw new ArgumentOutOfRangeException(nameof(monthNumber),
                    "Month number must be in range [0, 12)")
            };
        }

        private void CheckBirthDate(DateTime? value)
        {
            if (value.HasValue)
                CheckAge(value.Value);
        }

        private void CheckEMail(string value)
        {
            if (value != null)
            {
                if (! EmailRegex.IsMatch(value))
                    throw new InvalidEmailException();
            }
        }
        
        private bool? CheckIfAdult()
        {
            if (_birthDate.HasValue)
                return CountAge(_birthDate.Value) >= 18;
            return null;
        }
        
        private bool? CheckIfBirthday()
        {
            if (_birthDate.HasValue)
                return _birthDate.Value.Month == DateTime.Today.Month &&
                       _birthDate.Value.Day == DateTime.Today.Day;
            return null;
        }
        
        private static int CountAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var years = today.Year - birthDate.Year;
            if (birthDate.Month == today.Month &&
                today.Day < birthDate.Day
                || today.Month < birthDate.Month)
            {
                years--;
            }
            return years;
        }

        private SunSigns? DetermineWesternZodiac()
        {
            if (!_birthDate.HasValue) return null;
            var index = _birthDate.Value.Month - 1;
            if (_birthDate.Value.Day > GetDividingDay(index))
                index = (index + 1) % 12;
            return (SunSigns)Enum.GetValues(typeof(SunSigns)).GetValue(index);
        }

        private ChineseSigns? DetermineChineseZodiac()
        {
            if (!_birthDate.HasValue) return null;
            var year = _birthDate.Value.Year < AnchorYear ? 
                _birthDate.Value.Year : ChineseCalendar.GetYear(_birthDate.Value);
            var index = (year - AnchorYear) % 12;
            if (index < 0) index += 12;
            return (ChineseSigns)Enum.GetValues(typeof(ChineseSigns)).GetValue(index);
        }

        private void ChangeDependent()
        {
            _sunSign = DetermineWesternZodiac();
            _chineseSign = DetermineChineseZodiac();
            UpdateAgeProperties();
        }

        internal void UpdateAgeProperties()
        {
            _isAdult = CheckIfAdult();
            _hasBirthdayToday = CheckIfBirthday();
        }

        private static void CheckAge(DateTime birthDate)
        {
            var age = CountAge(birthDate);
            if (age > 135) throw new BornInDistantPastException();
            if (age < 0) throw new BornInFutureException();
        }

        private static void CheckNameString(string value)
        {
            if (value != null)
            {
                if (! NameRegex.IsMatch(value))
                    throw new InvalidNameException();
            }
        }
        
        private static void CheckSurnameString(string value)
        {
            if (value != null)
            {
                if (! NameRegex.IsMatch(value))
                    throw new InvalidSurnameException();
            }
        }

        [field:NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal Person(string name, string surname, string email, DateTime? birthDate)
        {
            Name = name;
            Surname = surname;
            EMail = email;
            BirthDate = birthDate;
        }
        
        internal Person(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            EMail = email;
            BirthDate = null;
        }
        
        internal Person(string name, string surname, DateTime? birthDate)
        {
            Name = name;
            Surname = surname;
            EMail = null;
            BirthDate = birthDate;
        }
    }
}