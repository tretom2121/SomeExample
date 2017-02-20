using SomeExample.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeExample.Data
{
    public interface IJobListParser
    {
        Task<List<Job>> ParseJobListAsync(string inputToParse);
    }
}
