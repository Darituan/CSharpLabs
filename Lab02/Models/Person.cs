using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Lab02.Models
{
    internal class Person
    {
        // each month has two western zodiac signs in it
        // this array represents the last days of the first zodiac sign of each month
        private enum MonthDividingDays
        {
            Jan = 20,
            Feb = 18,
            Mar = 20,
            Apr = 20,
            May = 20,
            Jun = 21,
            Jul = 22,
            Aug = 23,
            Sep = 23,
            Oct = 23,
            Nov = 22,
            Dec = 21
        }

        internal enum SunSigns
        {
            [Description("Western Zodiac Sign: Capricorn")]
            Capricorn,
            [Description("Western Zodiac Sign: Aquarius")]
            Aquarius,
            [Description("Western Zodiac Sign: Pisces")]
            Pisces,
            [Description("Western Zodiac Sign: Aries")]
            Aries,
            [Description("Western Zodiac Sign: Taurus")]
            Taurus,
            [Description("Western Zodiac Sign: Gemini")]
            Gemini,
            [Description("Western Zodiac Sign: Cancer")]
            Cancer,
            [Description("Western Zodiac Sign: Leo")]
            Leo,
            [Description("Western Zodiac Sign: Virgo")]
            Virgo,
            [Description("Western Zodiac Sign: Libra")]
            Libra,
            [Description("Western Zodiac Sign: Scorpio")]
            Scorpio,
            [Description("Western Zodiac Sign: Sagittarius")]
            Sagittarius
        }
        
        internal enum ChineseSigns
        {
            [Description("Chinese Zodiac Sign: Tiger")]
            Tiger,
            [Description("Chinese Zodiac Sign: Rabbit")]
            Rabbit,
            [Description("Chinese Zodiac Sign: Dragon")]
            Dragon,
            [Description("Chinese Zodiac Sign: Snake")]
            Snake,
            [Description("Chinese Zodiac Sign: Horse")]
            Horse,
            [Description("Chinese Zodiac Sign: Goat")]
            Goat,
            [Description("Chinese Zodiac Sign: Monkey")]
            Monkey,
            [Description("Chinese Zodiac Sign: Rooster")]
            Rooster,
            [Description("Chinese Zodiac Sign: Dog")]
            Dog,
            [Description("Chinese Zodiac Sign: Pig")]
            Pig,
            [Description("Chinese Zodiac Sign: Rat")]
            Rat,
            [Description("Chinese Zodiac Sign: Ox")]
            Ox
        }
        
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
        
        internal string Name { get; set; }
        
        internal string Surname { get; set; }

        internal string EMail
        {
            get => _eMail;
            set
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
            set
            {
                if (value.HasValue)
                    CheckAge(value.Value);
                _birthDate = value;
                ChangeDependent();
            }
        }

        internal SunSigns? SunSign => _sunSign;

        internal ChineseSigns? ChineseSign => _chineseSign;

        internal bool? IsAdult
        {
            get
            {
                if (_birthDate.HasValue)
                    return AgeInYears(_birthDate.Value) >= 18;
                return null;
            }
        }

        internal bool? IsBirthday
        {
            get
            {
                if (_birthDate.HasValue)
                    return _birthDate.Value.Month == DateTime.Today.Month &&
                          _birthDate.Value.Day == DateTime.Today.Day;
                return null;
            }
        }

        #endregion

        private static int AgeInYears(DateTime birthDate)
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
            if (_birthDate.Value.Day > (int)Enum.GetValues(typeof(MonthDividingDays)).GetValue(index))
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

        private void CheckAge(DateTime birthDate)
        {
            var age = AgeInYears(birthDate);
            if (age >= 0 && age <= 135) return;
            var message = string.Concat("It seems like You were either born more", 
                " than 135 years ago or in future.", " If this is not a mistake, we are sorry to tell", 
                " this app is probably not designed for You :(");
            Console.WriteLine(message);
            throw new ArgumentException(message);
        }
        
        internal static string GetDescription<T>(T genericEnum) where T: Enum
        {
            MemberInfo[] memberInfo = typeof(T).GetMember(genericEnum.ToString());
            if (memberInfo.Length > 0)
            {
                var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribs.Any())
                {
                    return ((DescriptionAttribute)attribs.ElementAt(0)).Description;
                }
            }
            return genericEnum.ToString();
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