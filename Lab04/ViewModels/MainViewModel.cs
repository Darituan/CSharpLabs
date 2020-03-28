using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Lab04.Models;
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
        private bool _sortingAndFiltersApplied;

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

        private bool CanApplySortingAndFilters()
        {
            return SortingEnabled && Getter != null;
        }
        
        private bool CanClearSortingAndFilters()
        {
            return _sortingAndFiltersApplied;
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
            StationManager.CurrentUser = null;
            NavigationManager.Instance.Navigate(ViewType.Add);
        }
        
        private void EditPerson(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Edit);
        }

        private async void ApplySortingAndFilters(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Users = SortAndFilter.SortUsers(Users, Getter.Getter);
                _sortingAndFiltersApplied = true;
            });
            LoaderManager.Instance.HideLoader();
        }
        
        private async void ClearSortingAndFilters(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Users = StationManager.DataStorage.Users;
                _sortingAndFiltersApplied = false;
            });
            LoaderManager.Instance.HideLoader();
        }

        public MainViewModel()
        {
            _users = StationManager.DataStorage.Users;
        }
        
    }
}