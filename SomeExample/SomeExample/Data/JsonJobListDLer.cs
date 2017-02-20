using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Data
{
    class JsonJobListDLer : IInputDLer
    {
        private string _apiURL;
        private int _chID, _limit, _offset;
   
        #region ctor
        public JsonJobListDLer(string APIurl, int chID = 10, int limit = 100, int offset = 100)
        {
            _apiURL = APIurl;
            _chID = chID;
            _limit = limit;
            _offset = offset;
        }
        #endregion //ctor

        public async Task<string> DownloadPostingsAsync()
        {
            StringBuilder result = new StringBuilder();
            int tmpOffset = 0;

            string tmpJson;
            do
            {
                string selection = @"?filters={""Channel.ID"":[{""eq"":" + _chID + @"}]}&options={""limit"":[{""eq"":" + _limit + @"}], ""offset"":[{""eq"":" + tmpOffset + @"}]}&" +
                    @"fields=[""Posting.CreatingDate"",""Posting.ModifyingDate"",""Posting.ID"",""Posting.Title"",""Posting.DescriptionFormatted"",""Posting.HoursPerWeek"",""Posting.Radius"",""Posting.OrganisationUnit.ID"",""Posting.OrganisationUnit.CityName"",""Posting.Locations.ZipCode"",""Posting.Locations.CityName"",""Posting.Locations.AddressLine"",""Posting.Locations.District"",""Posting.Locations.Region.RegionName"",""Posting.Locations.Country.CountryName"",""Posting.Locations.Country.ISOCode"",""Posting.PostingProfessions.Profession.ID""]";
                Uri api = new Uri(_apiURL);
                Uri completeURL = new Uri(api, selection);

                WebClient wc = new WebClient();
                tmpJson = await wc.DownloadStringTaskAsync(completeURL.ToString()).ConfigureAwait(false);
                if (tmpJson.Length > 2) result.Append(tmpJson);
                tmpOffset += _offset;
            } while (tmpJson.Length > 2 && tmpJson.Contains("{\"Posting\":{\"ID\":"));

            result = result.Replace("][", ",");

            if (!result.ToString().Contains("{\"Posting\":{\"ID\":"))
            {
                return string.Empty;
            }
            return result.ToString();

        }
    }
}
