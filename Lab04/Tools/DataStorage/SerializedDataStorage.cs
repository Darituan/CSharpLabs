﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Lab04.Models;
using Lab04.Tools.Managers;

namespace Lab04.Tools.DataStorage
{
    internal class SerializedDataStorage: INotifyPropertyChanged
    {
        private static readonly string[] Names = {"Dan", "Alex", "Kate", "Mike", "Julia", "Sophie", "John", "Ann"};
        private static readonly string[] Surnames = {"Darituan", "Surname", "Sad", "Star", 
            "Roberts", "Xeno", "Wick", "Hinkul"};
        private static readonly string[] EMails = {"dar", "aaa", "aba", "star", "fff", "hex", "wick", "ah"};
        private static readonly Random Rand = new Random();
        private readonly ObservableCollection<Person> _users;
        private Person _currentUser;

        internal ObservableCollection<Person> Users => _users;

        public Person CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        internal SerializedDataStorage()
        {
            try
            {
                _users = 
                    SerializationManager.Deserialize<ObservableCollection<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = CreateDefaultUsers();
            }
        }
        
        
        private static ObservableCollection<Person> CreateDefaultUsers()
        {
            var users = new ObservableCollection<Person>();
            var currentYear = DateTime.Now.Year;
            var minYear = currentYear - 135;
            for (var i = 0; i < 50; ++i)
            {
                var name = Names[Rand.Next(Names.Length)];
                var surname = Surnames[Rand.Next(Surnames.Length)];
                var eMail = $"{EMails[Rand.Next(EMails.Length)]}@gmail.com";
                var birthDate = new DateTime(Rand.Next(minYear, currentYear), 
                    Rand.Next(1, 13), Rand.Next(1, 29));
                users.Add(new Person(name, surname, eMail, birthDate));
            }
            SerializationManager.Serialize(users, FileFolderHelper.StorageFilePath);
            return users;
        }
        
        internal void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}