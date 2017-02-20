using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeExample.Model;
using Microsoft.Win32;
using SomeExample.Helpers;

namespace SomeExample.Data
{
    public class SaveListToTxt : IListSaver
    {
        private IListConverter _converter;
        private List<Job> _jobList;

        public SaveListToTxt(IListConverter converter, List<Job> jobList)
        {
            _converter = converter;
            _jobList = jobList;
        }

        public void SaveList()
        {
            SaveFileDialog sd = new SaveFileDialog();

            string strToSave = _converter.ConvertList(_jobList);

            sd.Filter = "Txt files|*.txt|All files|*.*";

            if (sd.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(sd.FileName, strToSave, Encoding.UTF8);
            }
        }
    }
}
