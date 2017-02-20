using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Helpers
{
    public class Status : ViewModelBase
    {
        private string _process;
        public string process
        {
            get { return _process; }
            set { Set(ref _process, value); }
        }

        private int _percent;

        public int percent
        {
            get { return _percent; }
            set { Set( ref _percent, value); }
        }

        private bool _isBusy;

        public bool isBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }
    }
}
