using SU.Frontend.Helper;
using SU.Frontend.Helper.User;
using System;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.UserControls
{
    public class TaskbarViewModel
    {

        // Commands
        public ICommand ShutDownCommand { get; }

        // Konstruktor
        public TaskbarViewModel()
        {

            // Initiera kommandon
            ShutDownCommand = new RelayCommand(ExecuteShutDown);

        }

        // Stäng ner applikationen
        private void ExecuteShutDown()
        {
            Application.Current.Shutdown();
        }

        
    }
}
