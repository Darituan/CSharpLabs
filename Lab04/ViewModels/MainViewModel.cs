using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;
using Lab04.Models;
using Lab04.Enums;
using Lab04.Tools;
using Lab04.Tools.Managers;
using Lab04.Tools.Navigation;
using Lab04.Tools.SortAndFilter;

namespace Lab04.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Person> _users;

        private readonly ObservableCollection<PropertyGetter> _getters =
            PropertyGetterHelper.GetPropertyGetters(typeof(Person));

        private PropertyGetter _getter;
        
        private bool _sortingEnabled;
        private bool _filtersOrSortingApplied;
        
        private bool _nameFilterEnabled;
        private string _nameFilterString;
        
        private bool _surnameFilterEnabled;
        private string _surnameFilterString;
        
        private bool _emailFilterEnabled;
        private string _emailFilterString;
        
        private bool _birthDateFilterFromEnabled;
        private bool _birthDateFilterToEnabled;
        private DateTime _birthDateLowerBound;
        private DateTime _birthDateHigherBound;
        
        private bool _chineseSignsFilterFromEnabled;
        private bool _chineseSignsFilterToEnabled;
        private ChineseSigns _chineseSignsLowerBound;
        private ChineseSigns _chineseSignsHigherBound;
        
        private bool _sunSignsFilterFromEnabled;
        private bool _sunSignsFilterToEnabled;
        private SunSigns _sunSignsLowerBound;
        private SunSigns _sunSignsHigherBound;
        
        private bool _adultFilterEnabled;
        private bool _adultFilterBool;
        
        private bool _birthdayFilterEnabled;
        private bool _birthdayFilterBool;

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        
        private RelayCommand<object> _applySortingAndFiltersCommand;
        private RelayCommand<object> _clearSortingAndFiltersCommand;


        public ObservableCollection<Person> Users
        {
            get => _users;
            private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PropertyGetter> Getters => _getters;

        public PropertyGetter Getter
        {
            get => _getter;
            set
            {
                _getter = value;
                OnPropertyChanged();
            }
        }

        public bool SortingEnabled
        {
            get => _sortingEnabled;
            set
            {
                _sortingEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool NameFilterEnabled
        {
            get => _nameFilterEnabled;
            set
            {
                _nameFilterEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool SurnameFilterEnabled
        {
            get => _surnameFilterEnabled;
            set
            {
                _surnameFilterEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool EMailFilterEnabled
        {
            get => _emailFilterEnabled;
            set
            {
                _emailFilterEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool BirthDateFilterFromEnabled
        {
            get => _birthDateFilterFromEnabled;
            set
            {
                _birthDateFilterFromEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool BirthDateFilterToEnabled
        {
            get => _birthDateFilterToEnabled;
            set
            {
                _birthDateFilterToEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool SunSignsFilterFromEnabled
        {
            get => _sunSignsFilterFromEnabled;
            set
            {
                _sunSignsFilterFromEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool SunSignsFilterToEnabled
        {
            get => _sunSignsFilterToEnabled;
            set
            {
                _sunSignsFilterToEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool ChineseSignsFilterFromEnabled
        {
            get => _chineseSignsFilterFromEnabled;
            set
            {
                _chineseSignsFilterFromEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool ChineseSignsFilterToEnabled
        {
            get => _chineseSignsFilterToEnabled;
            set
            {
                _chineseSignsFilterToEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool AdultFilterEnabled
        {
            get => _adultFilterEnabled;
            set
            {
                _adultFilterEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public bool BirthdayFilterEnabled
        {
            get => _birthdayFilterEnabled;
            set
            {
                _birthdayFilterEnabled = value;
                OnPropertyChanged();
            }
        }
        
        public string NameFilterString
        {
            get => _nameFilterString;
            set
            {
                _nameFilterString = value;
                OnPropertyChanged();
            }
        }
        
        public string SurnameFilterString
        {
            get => _surnameFilterString;
            set
            {
                _surnameFilterString = value;
                OnPropertyChanged();
            }
        }
        
        public string EMailFilterString
        {
            get => _emailFilterString;
            set
            {
                _emailFilterString = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime BirthDateLowerBound
        {
            get => _birthDateLowerBound;
            set
            {
                _birthDateLowerBound = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime BirthDateHigherBound
        {
            get => _birthDateHigherBound;
            set
            {
                _birthDateHigherBound = value;
                OnPropertyChanged();
            }
        }
        
        public SunSigns SunSignsLowerBound
        {
            get => _sunSignsLowerBound;
            set
            {
                _sunSignsLowerBound = value;
                OnPropertyChanged();
            }
        }

        public SunSigns SunSignsHigherBound
        {
            get => _sunSignsHigherBound;
            set
            {
                _sunSignsHigherBound = value;
                OnPropertyChanged();
            }
        }
        
        public ChineseSigns ChineseSignsHigherBound
        {
            get => _chineseSignsHigherBound;
            set
            {
                _chineseSignsHigherBound = value;
                OnPropertyChanged();
            }
        }
        
        public ChineseSigns ChineseSignsLowerBound
        {
            get => _chineseSignsLowerBound;
            set
            {
                _chineseSignsLowerBound = value;
                OnPropertyChanged();
            }
        }
        
        public bool AdultFilterBool
        {
            get => _adultFilterBool;
            set
            {
                _adultFilterBool = value;
                OnPropertyChanged();
            }
        }
        
        public bool BirthdayFilterBool
        {
            get => _birthdayFilterBool;
            set
            {
                _birthdayFilterBool = value;
                OnPropertyChanged();
            }
        }

        public Person CurrentUser
        {
            get => StationManager.DataStorage.CurrentUser;
            set => StationManager.DataStorage.CurrentUser = value;
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
        
        public RelayCommand<object> ApplySortingAndFiltersCommand
        {
            get
            {
                return _applySortingAndFiltersCommand ??= new RelayCommand<object>(
                    ApplySortingAndFilters, o => CanApplySortingAndFilters());
            }
        }
        
        public RelayCommand<object> ClearSortingAndFiltersCommand
        {
            get
            {
                return _clearSortingAndFiltersCommand ??= new RelayCommand<object>(
                    ClearSortingAndFilters, o => CanClearTextFilters());
            }
        }

        private bool CanApplySortingAndFilters()
        {
            return EnabledFiltersEntered() && HasFiltersOrSortingEnabled();
        }
        
        private bool CanClearTextFilters()
        {
            return _filtersOrSortingApplied;
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
            StationManager.DataStorage.CurrentUser = null;
            NavigationManager.Instance.Navigate(ViewType.Add);
        }
        
        private void EditPerson(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Edit);
        }

        private void ClearName()
        {
            NameFilterString = null;
        }
        
        private void ClearSurname()
        {
            SurnameFilterString = null;
        }
        
        private void ClearEMail()
        {
            EMailFilterString = null;
        }

        private void ApplyChanges(ObservableCollection<Person> users)
        {
            var lowerBounds = new Dictionary<MethodInfo, IComparable>();
            var higherBounds = new Dictionary<MethodInfo, IComparable>();
            var containedStrings = new Dictionary<MethodInfo, string>();
            var predicates = new Dictionary<MethodInfo, bool>();
            if (NameFilterEnabled)
                containedStrings[Getters[0].Getter] = NameFilterString;
            if (SurnameFilterEnabled)
                containedStrings[Getters[1].Getter] = SurnameFilterString;
            if (EMailFilterEnabled)
                containedStrings[Getters[2].Getter] = EMailFilterString;
            if (BirthDateFilterFromEnabled)
                lowerBounds[Getters[3].Getter] = BirthDateLowerBound;
            if (BirthDateFilterToEnabled)
                higherBounds[Getters[3].Getter] = BirthDateHigherBound;
            if (SunSignsFilterFromEnabled)
                lowerBounds[Getters[4].Getter] = SunSignsLowerBound;
            if (SunSignsFilterToEnabled)
                higherBounds[Getters[4].Getter] = SunSignsHigherBound;
            if (ChineseSignsFilterFromEnabled)
                lowerBounds[Getters[5].Getter] = ChineseSignsLowerBound;
            if (ChineseSignsFilterToEnabled)
                higherBounds[Getters[5].Getter] = ChineseSignsHigherBound;
            if (AdultFilterEnabled)
                predicates[Getters[6].Getter] = AdultFilterBool;
            if (BirthdayFilterEnabled)
                predicates[Getters[7].Getter] = BirthdayFilterBool;
            if (SortingEnabled)
                Users = SortAndFilter.SortAndFilterUsers(users, lowerBounds, higherBounds,
                    containedStrings, predicates, Getter.Getter);
            else
                Users = SortAndFilter.SortAndFilterUsers(users, lowerBounds, higherBounds,
                    containedStrings, predicates);
            _filtersOrSortingApplied = true;
        }

        private async void ApplyNotifiedChanges()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                ApplyChanges(Users);
            });
            LoaderManager.Instance.HideLoader();
        }

        private async void ApplySortingAndFilters(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                ApplyChanges(StationManager.DataStorage.Users);
            });
            LoaderManager.Instance.HideLoader();
        }
        
        private async void ClearSortingAndFilters(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Users = new ObservableCollection<Person>(StationManager.DataStorage.Users);
                ClearName();
                ClearSurname();
                ClearEMail();
                Getter = Getters[0];
                BirthDateLowerBound = DateTime.Now;
                BirthDateHigherBound = DateTime.Now;
                SunSignsLowerBound = SunSigns.Capricorn;
                SunSignsHigherBound = SunSigns.Capricorn;
                ChineseSignsLowerBound = ChineseSigns.Tiger;
                ChineseSignsHigherBound = ChineseSigns.Tiger;
                AdultFilterBool = false;
                BirthdayFilterBool = false;

                SortingEnabled = false;
                NameFilterEnabled = false;
                SurnameFilterEnabled = false;
                EMailFilterEnabled = false;
                BirthDateFilterFromEnabled = false;
                BirthDateFilterToEnabled = false;
                SunSignsFilterFromEnabled = false;
                SunSignsFilterToEnabled = false;
                ChineseSignsFilterFromEnabled = false;
                ChineseSignsFilterToEnabled = false;
                AdultFilterEnabled = false;
                BirthdayFilterEnabled = false;
                
                _filtersOrSortingApplied = false;
            });
            LoaderManager.Instance.HideLoader();
        }

        private bool EnabledFiltersEntered()
        {
            return (SortingEnabled && Getter != null || !SortingEnabled) && 
                   (NameFilterEnabled && NameFilterString != null || !NameFilterEnabled) && 
                   (SurnameFilterEnabled && SurnameFilterString != null || !SurnameFilterEnabled) &&
                   (EMailFilterEnabled && EMailFilterString != null || !EMailFilterEnabled);
        }

        private bool HasFiltersOrSortingEnabled()
        {
            return SortingEnabled || NameFilterEnabled || SurnameFilterEnabled || EMailFilterEnabled ||
                   BirthDateFilterFromEnabled || BirthDateFilterToEnabled || SunSignsFilterFromEnabled ||
                   SunSignsFilterToEnabled || ChineseSignsFilterFromEnabled || ChineseSignsFilterToEnabled ||
                   AdultFilterEnabled || BirthdayFilterEnabled;
        }

        private void OnUsersChanged(object obj, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    
                    var newUser = e.NewItems[0] as Person;
                    Users.Add(newUser);
                    if (EnabledFiltersEntered())
                    {
                        ApplyNotifiedChanges();
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var oldUser = e.OldItems[0] as Person;
                    Users.Remove(oldUser);
                    break;
                default:
                    if (EnabledFiltersEntered())
                    {
                        ApplyNotifiedChanges();
                    }
                    break;
            }
        }

        public MainViewModel()
        {
            Getter = Getters[0];
            BirthDateLowerBound = DateTime.Now;
            BirthDateHigherBound = DateTime.Now;
            _users = new ObservableCollection<Person>(StationManager.DataStorage.Users);
            StationManager.DataStorage.Users.CollectionChanged += OnUsersChanged;
        }
        
    }
}