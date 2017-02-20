using SomeExample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Model
{
    public class GetJobList
    {
        private IInputDLer _inputDLer;
        private IJobListParser _jobListParser;

        public async Task<List<Job>> GetJobListAsync(IInputDLer inputDLer, IJobListParser jobListParser)
        {
            _inputDLer = inputDLer;
            _jobListParser = jobListParser;

            string inpFileString = await _inputDLer.DownloadPostingsAsync().ConfigureAwait(false);

            List<Job> parsedJobList = new List<Job>();
            parsedJobList = await _jobListParser.ParseJobListAsync(inpFileString).ConfigureAwait(false);

            return parsedJobList;
        }
    }
}
