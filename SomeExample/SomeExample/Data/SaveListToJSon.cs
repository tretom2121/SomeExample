using Microsoft.Win32;
using Newtonsoft.Json;
using SomeExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Data
{
    public class SaveListToJSon : IListSaver
    {
        private List<Job> _listToSave;
        public SaveListToJSon(List<Job> listToSave)
        {
            _listToSave = listToSave;
        }

        public void SaveList()
        {
            SaveFileDialog sd = new SaveFileDialog();

            sd.Filter = "Json files|*.json|All files|*.*";

            if (sd.ShowDialog() == true)
            {
                string jsonStr = JsonConvert.SerializeObject(_listToSave.ToArray());
                System.IO.File.WriteAllText(sd.FileName, jsonStr, Encoding.UTF8);
            }
        }
    }
}
