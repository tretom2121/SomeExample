using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Nito.AsyncEx;
using SomeExample.Data;
using SomeExample.Helpers;
using SomeExample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SomeExample.ViewModel
{
 
    public class MainViewModel : ViewModelBase
    {
        private IInputDLer inputDLer;
        private IListSaver listSaver;
        private IJobListParser jobListParser;
        private List<Job> jl;

        private INotifyTaskCompletion<List<Job>> _fileDLDone;
        public INotifyTaskCompletion<List<Job>> fileDLDone
        {
            get { return _fileDLDone; }
            set { Set( ref _fileDLDone, value); }
        }

        #region LoadRadioButtonsProps
        private bool _txtChk;
        public bool txtChk
        {
            get { return _txtChk; }
            set { Set (ref _txtChk, value); LoadListCommand.RaiseCanExecuteChanged(); }
        }

        private bool _xmlChk;
        public bool xmlChk
        {
            get { return _xmlChk; }
            set { Set(ref _xmlChk, value); LoadListCommand.RaiseCanExecuteChanged(); }
        }

        private bool _jsonChk;
        public bool jsonChk
        {
            get { return _jsonChk; }
            set { Set(ref _jsonChk, value); LoadListCommand.RaiseCanExecuteChanged(); }
        }
        #endregion //LoadRadioButtonsProps

        #region SaveRadioButtonsProps
        private bool _txtOutChk;
        public bool txtOutChk
        {
            get { return _txtOutChk; }
            set { Set(ref _txtOutChk, value); SaveListCommand.RaiseCanExecuteChanged(); }
        }

        private bool _xmlOutChk;
        public bool xmlOutChk
        {
            get { return _xmlOutChk; }
            set { Set(ref _xmlOutChk, value); SaveListCommand.RaiseCanExecuteChanged(); }
        }

        private bool _jsonOutChk;
        public bool jsonOutChk
        {
            get { return _jsonOutChk; }
            set { Set(ref _jsonOutChk, value); SaveListCommand.RaiseCanExecuteChanged(); }
        }
        #endregion //SaveRadioButtonsProps

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

        private bool _canLoad;
        public bool canLoad
        {
            get { return _canLoad; }
            set { Set(ref _canLoad, value); LoadListCommand.RaiseCanExecuteChanged(); }
        }

        private bool _canSave;
        public bool canSave
        {
            get { return _canSave; }
            set { Set(ref _canSave, value); }
        }

        private bool _isIndeterminate;

        public bool isIndeterminate
        {
            get { return _isIndeterminate; }
            set { Set( ref _isIndeterminate, value); }
        }


        private ObservableCollection<Job> _jobList;
        public ObservableCollection<Job> jobList
        {
            get { return _jobList; }
            set { Set( ref _jobList, value); }
        }

        private Status  _status;
        public Status status
        {
            get { return _status; }
            set { Set(ref _status, value); }
        }


        public RelayCommand LoadListCommand { get; private set; }
        public RelayCommand SaveListCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }

#region ctor
        public MainViewModel()
        {
            status = new Status() { process = "", percent = 0, isBusy = false };
            inputDLer = null;
            jobListParser = null;

            LoadListCommand = new RelayCommand(LoadList, CanLoad);
            SaveListCommand = new RelayCommand(SaveList, CanSave);
            ExitCommand = new RelayCommand(Exit, CanExit);

            canLoad = true;
            canSave = false;

            Messenger.Default.Register<Status>(this, SetStatus);
        }
#endregion //ctor

        private bool CanExit()
        {
            return canExitChecker;
        }

        private bool CanLoad()
        {
            return (txtChk || xmlChk || jsonChk) && canLoad;
        }

        private bool CanSave()
        {
            return canSave && (txtOutChk || xmlOutChk || jsonOutChk);
        }

        private void LoadList()
        {
            canLoad = false;
            LoadListCommand.RaiseCanExecuteChanged();
            if (txtChk)
            {
                Debug.WriteLine("a txt file must be loaded");
                canLoad = true;
                LoadListCommand.RaiseCanExecuteChanged();
            }
            else if (xmlChk)
            {
                jobList = new ObservableCollection<Job>()
                {
                    new Job()
                    {
                        title = "Sorry, this function is not implemented yet",
                    }
                };
                canLoad = true;
                LoadListCommand.RaiseCanExecuteChanged();
            }
            else
            {
                jl = new List<Job>();
                string jobsURL = @"";
                inputDLer = new JsonJobListDLer(jobsURL);
                jobListParser = new JsonJobListParser();

                status.percent = 0;
                status.process = "Process started";
                status.isBusy = true;
                isIndeterminate = true;

                GetJobList getJobs = new GetJobList();
                fileDLDone = NotifyTaskCompletion.Create(getJobs.GetJobListAsync(inputDLer, jobListParser));
                fileDLDone.PropertyChanged += FileDLDone_PropertyChanged;
            }
        }

        private void SaveList()
        {
            if (jobList == null || jobList.Count == 0)
            {
                status.process = "Your joblist is empty, nothing to save";
            }
            else
            {
                if (txtOutChk)
                {

                    listSaver = new SaveListToTxt(new ListToTxtConverter(), jobList.ToList());
                    
                }
                else if (xmlOutChk)
                { listSaver = new SaveListToXml(jobList.ToList()); }
                else
                { listSaver = new SaveListToJSon(jobList.ToList()); }

                if (listSaver != null)
                {
                    listSaver.SaveList();
                }
            }
        }

        private void SetStatus(Status actStatus)
        {
            isIndeterminate = false;
            status = actStatus;
        }

        private void FileDLDone_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (fileDLDone.IsSuccessfullyCompleted && e.PropertyName== "IsSuccessfullyCompleted")
            {
                var tmpList = fileDLDone.Result;
                tmpList = tmpList.OrderBy(p => p.title).ToList();
                jobList = new ObservableCollection<Job>(tmpList);
                


                canLoad = true;
                LoadListCommand.RaiseCanExecuteChanged();
                canSave = true;
                SaveListCommand.RaiseCanExecuteChanged();
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}