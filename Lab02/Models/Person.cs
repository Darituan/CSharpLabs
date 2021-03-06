﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Lab02.Models
{
    internal class Person
    {
        
        #region Fields
        // the first year all dates of which work correctly in ChineseLunisolarCalendar
        private static readonly int AnchorYear = 1902;
        private static readonly ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();
        private static readonly Regex EmailRegex = 
            new Regex("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

        private string _eMail;
        private DateTime? _birthDate;
        private SunSigns? _sunSign;
        private ChineseSigns? _chineseSign;

        #endregion

        #region Properties
        
        internal string Name { get; private set; }
        
        internal string Surname { get; private set; }

        internal string EMail
        {
            get => _eMail;
            private set
            {
                if (value != null)
                {
                    if (! EmailRegex.IsMatch(value))
                        throw new ArgumentException(
                            "Oops! It seems like there is a mistake in Your e-mail address");
                }
                _eMail = value;
            }
        }

        internal DateTime? BirthDate
        {
            get => _birthDate;
            private set
            {
                if (value.HasValue)
                    CheckAge(value.Value);
                _birthDate = value;
                ChangeDependent();
            }
        }

        internal SunSigns? SunSign => _sunSign;

        internal ChineseSigns? ChineseSign => _chineseSign;

        internal bool? IsAdult => CheckIfAdult();

        internal bool? IsBirthday => CheckIfBirthday();

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
        }

        private static void CheckAge(DateTime birthDate)
        {
            var age = CountAge(birthDate);
            if (age >= 0 && age <= 135) return;
            var message = string.Concat("It seems like You were either born more", 
                " than 135 years ago or in future.", " If this is not a mistake, we are sorry to tell", 
                " this app is probably not designed for You :(");
            Console.WriteLine(message);
            throw new ArgumentException(message);
        }
        
        internal static string GetDescription<T>(T genericEnum) where T: Enum
        {
            var memberInfo = typeof(T).GetMember(genericEnum.ToString());
            if (memberInfo.Length <= 0) return genericEnum.ToString();
            var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribs.Any() ? ((DescriptionAttribute)attribs.ElementAt(0)).Description : genericEnum.ToString();
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