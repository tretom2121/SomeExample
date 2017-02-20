using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeExample.Model;
using System.Xml.Linq;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace SomeExample.Data
{
    public class SaveListToXml : IListSaver
    {
        private List<Job> _listToSave;
        public SaveListToXml(List<Job> listToSave)
        {
            _listToSave = listToSave;
        }

        public void SaveList()
        {
            XDocument xDoc = new XDocument();
            using (var writer = xDoc.CreateWriter())
            {
                var ser = new DataContractSerializer(_listToSave.GetType());
                ser.WriteObject(writer, _listToSave);
            }

            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "Xml files|*.xml|All files|*.*";
            if (sd.ShowDialog() == true)
            {
                xDoc.Save(sd.FileName);
            }

        }
    }
}
