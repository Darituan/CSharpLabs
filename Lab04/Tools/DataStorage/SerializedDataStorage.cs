using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Lab04.Models;
using Lab04.Tools.Managers;

namespace Lab04.Tools.DataStorage
{
    internal class SerializedDataStorage
    {
        private static readonly string[] Names = {"Dan", "Alex", "Kate", "Mike", "Julia", "Sophie", "John", "Ann"};
        private static readonly string[] Surnames = {"Darituan", "Surname", "Sad", "Star", 
            "Roberts", "Xeno", "Wick", "Hinkul"};
        private static readonly string[] EMails = {"dar", "aaa", "aba", "star", "fff", "hex", "wick", "ah"};
        private static readonly Random Rand = new Random();
        private readonly List<Person> _users;

        internal List<Person> Users => _users;

        internal SerializedDataStorage()
        {
            try
            {
                _users = 
                    SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = CreateDefaultUsers();
            }
        }

        internal void AddUser(Person user)
        {
            _users.Add(user);
            SaveChanges();
        }
        
        internal void EditUser(Person user)
        {
            _users.Remove(user);
            _users.Add(user);
            SaveChanges();
        }
        
        internal void DeleteUser(Person user)
        {
            _users.Remove(user);
            SaveChanges();
        }
        
        private static List<Person> CreateDefaultUsers()
        {
            var users = new List<Person>();
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
        
        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
    }
}