using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Model
{
    public class InputFileLoader
    {
        IInputFile _inputfile;
        string _path;

        #region ctor
        public InputFileLoader(IInputFile inputFile)
        {
            _inputfile = inputFile;
        }
        #endregion //ctor

        public string LoadInputFile()
        {
            //TODO: implement File open and give back inputfile path
            return _inputfile.ReadContext(_path);
        }
    }
}
