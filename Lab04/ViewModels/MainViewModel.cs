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
        
        private bool _nameFilterEnabled;
        private string _nameFilterString;
        
        private bool _surnameFilterEnabled;
        private string _surnameFilterString;
        
        private bool _emailFilterEnabled;
        private string _emailFilterString;
        
        private bool _birthDateFilterFromEnabled;
        private bool _birthDateFilterToEnabled;
        private DateTime? _birthDateLowerBound;
        private DateTime? _birthDateHigherBound;
        
        private bool _chineseSignsFilterFromEnabled;
        private bool _chineseSignsFilterToEnabled;
        private ChineseSigns? _chineseSignsLowerBound;
        private ChineseSigns? _chineseSignsHigherBound;
        
        private bool _sunSignsFilterFromEnabled;
        private bool _sunSignsFilterToEnabled;
        private SunSigns? _sunSignsLowerBound;
        private SunSigns? _sunSignsHigherBound;
        
        private bool _adultFilterEnabled;
        private bool? _adultFilterBool;
        
        private bool _birthdayFilterEnabled;
        private bool? _birthdayFilterBool;
        
        private bool _sortingAndFiltersApplied;

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        
        private RelayCommand<object> _applySortingAndFiltersCommand;
        private RelayCommand<object> _clearSortingAndFiltersCommand;
        
        private RelayCommand<object> _clearSortIfNeededCommand;
        private RelayCommand<object> _clearNameIfNeededCommand;
        private RelayCommand<object> _clearSurnameIfNeededCommand;
        private RelayCommand<object> _clearEMailIfNeededCommand;
        private RelayCommand<object> _clearBirthDateLowerIfNeededCommand;
        private RelayCommand<object> _clearBirthDateHigherIfNeededCommand;
        private RelayCommand<object> _clearSunSignsLowerIfNeededCommand;
        private RelayCommand<object> _clearSunSignsHigherIfNeededCommand;
        private RelayCommand<object> _clearChineseSignsLowerIfNeededCommand;
        private RelayCommand<object> _clearChineseSignsHigherIfNeededCommand;
        private RelayCommand<object> _clearAdultIfNeededCommand;
        private RelayCommand<object> _clearBirthdayIfNeededCommand;
        

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
        
        public DateTime? BirthDateLowerBound
        {
            get => _birthDateLowerBound;
            set
            {
                _birthDateLowerBound = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime? BirthDateHigherBound
        {
            get => _birthDateHigherBound;
            set
            {
                _birthDateHigherBound = value;
                OnPropertyChanged();
            }
        }
        
        public SunSigns? SunSignsLowerBound
        {
            get => _sunSignsLowerBound;
            set
            {
                _sunSignsLowerBound = value;
                OnPropertyChanged();
            }
        }

        public SunSigns? SunSignsHigherBound
        {
            get => _sunSignsHigherBound;
            set
            {
                _sunSignsHigherBound = value;
                OnPropertyChanged();
            }
        }
        
        public ChineseSigns? ChineseSignsHigherBound
        {
            get => _chineseSignsHigherBound;
            set
            {
                _chineseSignsHigherBound = value;
                OnPropertyChanged();
            }
        }
        
        public ChineseSigns? ChineseSignsLowerBound
        {
            get => _chineseSignsLowerBound;
            set
            {
                _chineseSignsLowerBound = value;
                OnPropertyChanged();
            }
        }
        
        public bool? AdultFilterBool
        {
            get => _adultFilterBool;
            set
            {
                _adultFilterBool = value;
                OnPropertyChanged();
            }
        }
        
        public bool? BirthdayFilterBool
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
                    ClearSortingAndFilters, o => CanClearSortingAndFilters());
            }
        }
        
        public RelayCommand<object> ClearSortIfNeededCommand
        {
            get
            {
                return _clearSortIfNeededCommand ??= new RelayCommand<object>(
                    ClearSortIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearNameIfNeededCommand
        {
            get
            {
                return _clearNameIfNeededCommand ??= new RelayCommand<object>(
                    ClearNameIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearSurnameIfNeededCommand
        {
            get
            {
                return _clearSurnameIfNeededCommand ??= new RelayCommand<object>(
                    ClearSurnameIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearEMailIfNeededCommand
        {
            get
            {
                return _clearEMailIfNeededCommand ??= new RelayCommand<object>(
                    ClearEMailIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearBirthDateLowerIfNeededCommand
        {
            get
            {
                return _clearBirthDateLowerIfNeededCommand ??= new RelayCommand<object>(
                    ClearBirthDateLowerIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearBirthDateHigherIfNeededCommand
        {
            get
            {
                return _clearBirthDateHigherIfNeededCommand ??= new RelayCommand<object>(
                    ClearBirthDateHigherIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearSunSignsLowerIfNeededCommand
        {
            get
            {
                return _clearSunSignsLowerIfNeededCommand ??= new RelayCommand<object>(
                    ClearSunSignsLowerIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearSunSignsHigherIfNeededCommand
        {
            get
            {
                return _clearSunSignsHigherIfNeededCommand ??= new RelayCommand<object>(
                    ClearSunSignsHigherIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearChineseSignsLowerIfNeededCommand
        {
            get
            {
                return _clearChineseSignsLowerIfNeededCommand ??= new RelayCommand<object>(
                    ClearChineseSignsLowerIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearChineseSignsHigherIfNeededCommand
        {
            get
            {
                return _clearChineseSignsHigherIfNeededCommand ??= new RelayCommand<object>(
                    ClearChineseSignsHigherIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearBirthdayIfNeededCommand
        {
            get
            {
                return _clearBirthdayIfNeededCommand ??= new RelayCommand<object>(
                    ClearBirthdayIfNeeded, o => CanClearIfNeeded());
            }
        }
        
        public RelayCommand<object> ClearAdultIfNeededCommand
        {
            get
            {
                return _clearAdultIfNeededCommand ??= new RelayCommand<object>(
                    ClearAdultIfNeeded, o => CanClearIfNeeded());
            }
        }


        private bool CanClearIfNeeded()
        {
            return true;
        }

        private bool CanApplySortingAndFilters()
        {
            return HasFiltersOrSorting();
        }
        
        private bool CanClearSortingAndFilters()
        {
            return HasFiltersOrSorting();
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

        private void ClearSortIfNeeded(object obj)
        {
            Getter = null;
        }

        private void ClearNameIfNeeded(object obj)
        {
            NameFilterString = null;
        }
        
        private void ClearSurnameIfNeeded(object obj)
        {
            SurnameFilterString = null;
        }
        
        private void ClearEMailIfNeeded(object obj)
        {
            EMailFilterString = null;
        }

        private void ClearBirthDateLowerIfNeeded(object obj)
        {
            BirthDateLowerBound = null;
        }
        
        private void ClearBirthDateHigherIfNeeded(object obj)
        {
            BirthDateHigherBound = null;
        }
        
        private void ClearSunSignsLowerIfNeeded(object obj)
        {
            SunSignsLowerBound = null;
        }
        
        private void ClearSunSignsHigherIfNeeded(object obj)
        {
            SunSignsHigherBound = null;
        }
        
        private void ClearChineseSignsLowerIfNeeded(object obj)
        {
            ChineseSignsLowerBound = null;
        }
        
        private void ClearChineseSignsHigherIfNeeded(object obj)
        {
            ChineseSignsHigherBound = null;
        }

        private void ClearAdultIfNeeded(object obj)
        {
            AdultFilterBool = null;
        }
        
        private void ClearBirthdayIfNeeded(object obj)
        {
            BirthdayFilterBool = null;
        }

        private void ApplyChanges(ObservableCollection<Person> users)
        {
            var boundGetters = new List<MethodInfo>();
            var stringGetters = new List<MethodInfo>();
            var boolGetters = new List<MethodInfo>();
            foreach (var getter in _getters)
            {
                if (getter.Getter.ReturnType == typeof(string))
                    stringGetters.Add(getter.Getter);
                else if (getter.Getter.ReturnType == typeof(bool?))
                    boolGetters.Add(getter.Getter);
                else boundGetters.Add(getter.Getter);
            }
            var lowerBounds = new List<IComparable>();
            lowerBounds.Add(BirthDateLowerBound);
            lowerBounds.Add(SunSignsLowerBound);
            lowerBounds.Add(ChineseSignsLowerBound);
            var higherBounds = new List<IComparable>();
            higherBounds.Add(BirthDateHigherBound);
            higherBounds.Add(SunSignsHigherBound);
            higherBounds.Add(ChineseSignsHigherBound);
            var stringKeys = new List<string>();
            stringKeys.Add(NameFilterString);
            stringKeys.Add(SurnameFilterString);
            stringKeys.Add(EMailFilterString);
            var boolKeys = new List<bool?>();
            boolKeys.Add(AdultFilterBool);
            boolKeys.Add(BirthdayFilterBool);
            if (SortingEnabled)
                Users = SortAndFilter.SortAndFilterUsers(users, boundGetters,
                    stringGetters, boolGetters, lowerBounds, higherBounds,
                    stringKeys, boolKeys, Getter.Getter);
            else Users = SortAndFilter.SortAndFilterUsers(users, boundGetters,
                stringGetters, boolGetters, lowerBounds, higherBounds,
                stringKeys, boolKeys);
            _sortingAndFiltersApplied = true;
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
                ClearSortIfNeeded(null);
                ClearNameIfNeeded(null);
                ClearSurnameIfNeeded(null);
                ClearEMailIfNeeded(null);
                ClearBirthDateLowerIfNeeded(null);
                ClearBirthDateHigherIfNeeded(null);
                ClearSunSignsLowerIfNeeded(null);
                ClearSunSignsHigherIfNeeded(null);
                ClearChineseSignsLowerIfNeeded(null);
                ClearChineseSignsHigherIfNeeded(null);
                ClearAdultIfNeeded(null);
                ClearBirthdayIfNeeded(null);
                _sortingAndFiltersApplied = false;
            });
            LoaderManager.Instance.HideLoader();
        }

        private bool HasFiltersOrSorting()
        {
            return SortingEnabled || NameFilterEnabled || SurnameFilterEnabled || EMailFilterEnabled ||
                   BirthDateFilterFromEnabled || BirthDateFilterToEnabled || SunSignsFilterFromEnabled ||
                   SunSignsFilterToEnabled || ChineseSignsFilterFromEnabled || ChineseSignsFilterToEnabled ||
                   AdultFilterEnabled || BirthdayFilterEnabled;
        }

        internal void UpdateIfNeeded()
        {
            if (_sortingAndFiltersApplied && CanApplySortingAndFilters())
                ApplySortingAndFilters(null);
        }

        private void OnUsersChanged(object obj, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    
                    var newUser = e.NewItems[0] as Person;
                    Users.Add(newUser);
                    if (HasFiltersOrSorting())
                    {
                        ApplyNotifiedChanges();
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var oldUser = e.OldItems[0] as Person;
                    Users.Remove(oldUser);
                    break;
                default:
                    if (HasFiltersOrSorting())
                    {
                        ApplyNotifiedChanges();
                    }
                    break;
            }
        }

        public MainViewModel()
        {
            _users = new ObservableCollection<Person>(StationManager.DataStorage.Users);
            StationManager.DataStorage.Users.CollectionChanged += OnUsersChanged;
        }
        
    }
}