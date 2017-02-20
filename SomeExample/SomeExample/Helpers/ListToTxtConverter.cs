using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeExample.Model;

namespace SomeExample.Helpers
{
    public class ListToTxtConverter : IListConverter
    {
        public string ConvertList(List<Job> jobList)
        {
            StringBuilder result = new StringBuilder();

            if (jobList != null)
            {
                foreach (var item in jobList)
                {
                    if (item.title == null || item.title == "")
                    {
                        item.title = "Not specified";
                    }
                    result.Append(Environment.NewLine + item.title + Environment.NewLine);
                } 
            }
            return result.ToString();
        }
    }
}
