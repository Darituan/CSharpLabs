﻿using System.Threading.Tasks;
using System.Windows;
using Lab05.Tools;
using Lab05.Tools.Managers;
using Lab05.Tools.Navigation;

namespace Lab05.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel, ILoaderOwner, IContentOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        private INavigatable _content;
        private RelayCommand<object> _closeCommand;
        #endregion

        #region Properties
        public INavigatable Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        } 
        
        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(
                    Close, o => CanClose());
            }
        }
        #endregion

        private bool CanClose() => true;

        private void Close(object obj)
        {
            UpdateManager.Stop = true;
        }


        internal MainWindowViewModel()
        {
            ProcessesManager.Initialize();
            UpdateManager.Initialize();
            LoaderManager.Instance.Initialize(this);
            NavigationManager.Instance.Initialize(new ZodiacDeterminantNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
    }
}