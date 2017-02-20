using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeExample.Model;
using Newtonsoft.Json;
using System.Net;
using SomeExample.Helpers;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;

namespace SomeExample.Data
{
    public class JsonJobListParser : IJobListParser
    {
        private string _jsonToParse;

        public async Task<List<Job>> ParseJobListAsync(string inputToParse)
        {
            _jsonToParse = inputToParse;

            Status status = new Status();
            List<importJob> jobs = new List<importJob>();

            jobs = JsonConvert.DeserializeObject<List<importJob>>(_jsonToParse);
            List<Job> result = new List<Job>();

            if (jobs != null)
            {
                int i = 0;
                foreach (var item in jobs)
                {
                    var tmpJob = await ParseJobAsync(item).ConfigureAwait(false);
                    result.Add(tmpJob);

                    i++;
                    status.percent = (int)((double)i / jobs.Count() * 100);
                    status.process = "Downloading files (with an artificial delay of 5ms)";
                    status.isBusy = true;
                    DispatcherHelper.CheckBeginInvokeOnUI(() => Messenger.Default.Send<Status>(status));
                    Thread.Sleep(5);
                }
                status.percent = 0;
                status.process = "Ready";
                status.isBusy = false;
                DispatcherHelper.CheckBeginInvokeOnUI(() => Messenger.Default.Send<Status>(status)); 
            }
            else
            {
                jobs = new List<importJob>();
                status.percent = 0;
                status.process = "Error reading input";
                status.isBusy = false;
                DispatcherHelper.CheckBeginInvokeOnUI(() => Messenger.Default.Send<Status>(status));
            }

            return result;
        }

        private Task<Job> ParseJobAsync(importJob oneJob)
        {
            return Task.Run(() =>
            {
                oneJob.job.link = oneJob.DetailsURL;
                oneJob.job.status = true;
                //if (string.IsNullOrEmpty(oneJob.job.refNum)) oneJob.job.refNum = GetRefNumFromHTML(oneJob.job.link);
                return oneJob.job;
            });
        }

        private string GetRefNumFromHTML(string url)
        {
            string refNum = string.Empty;
            using (WebClient wc = new WebClient())
            {
                refNum = wc.DownloadString(url);
                if (refNum.IndexOf("Referenznummer:</h5>") != -1)
                {
                    refNum = refNum.Substring(refNum.IndexOf("Referenznummer:</h5>"));
                    refNum = refNum.Substring(0, refNum.IndexOf("</p>"));
                    refNum = refNum.Substring(refNum.IndexOf("<p>") + 3);
                    refNum = refNum.Trim();
                }
            }
            return refNum;
        }
    }

    public class importJob
    {
        [JsonProperty(PropertyName = "Posting")]
        public Job job = new Job();
        public string DetailsURL { get; set; }
    }
}
