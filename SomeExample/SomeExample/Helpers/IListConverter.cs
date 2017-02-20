using SomeExample.Model;
using System.Collections.Generic;

namespace SomeExample.Helpers
{
    public interface IListConverter
    {
        string ConvertList(List<Job> jobList);
    }
}