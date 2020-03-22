using System;
using System.Windows;
using Lab04.Models;
using Lab04.Tools.DataStorage;

namespace Lab04.Tools.Managers
{
    internal static class StationManager
    {
        private static SerializedDataStorage _dataStorage;

        internal static Person CurrentUser { get; set; }

        internal static SerializedDataStorage DataStorage => _dataStorage;

        internal static void Initialize(SerializedDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }
    }
}