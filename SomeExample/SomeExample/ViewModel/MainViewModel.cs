using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SomeExample.Model;
using System;
using System.Diagnostics;
using System.Windows;

namespace SomeExample.ViewModel
{
 
    public class MainViewModel : ViewModelBase
    {
        private bool _txtChk;
        public bool txtChk
        {
            get { return _txtChk; }
            set { Set (ref _txtChk, value); }
        }

        private bool _xmlChk;
        public bool xmlChk
        {
            get { return _xmlChk; }
            set { Set(ref _xmlChk, value); }
        }

        private bool _jsonChk;
        public bool jsonChk
        {
            get { return _jsonChk; }
            set { Set(ref _jsonChk, value); }
        }

        private bool _canExitChecker;
        public bool canExitChecker
        {
            get { return _canExitChecker; }
            set
            {
                Set(ref _canExitChecker, value);
                ExitCommand.RaiseCanExecuteChanged();
            }
        }

        private string _fileContent;
        public string fileContent
        {
            get { return _fileContent; }
            set { Set(ref _fileContent, value); }
        }

        public RelayCommand LoadFileCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

#region ctor
        public MainViewModel()
        {
            LoadFileCommand = new RelayCommand(LoadFile);
            ExitCommand = new RelayCommand(Exit, ()=> { return canExitChecker; });
        }
#endregion //ctor

        private void LoadFile()
        {
            if (txtChk)
            {
            }
            Debug.WriteLine("LoadFileCommand called");
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}