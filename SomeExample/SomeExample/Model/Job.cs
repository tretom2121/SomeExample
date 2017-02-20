using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExample.Model
{
    public class Job
    {
        public int _id { get; set; }
        [JsonProperty(PropertyName = "ID")]
        public double persy_id { get; set; }
        public string refNum { get; set; }
        public string title { get; set; }
        public bool status { get; set; }
        [JsonProperty(PropertyName = "OrganisationUnit")]
        public OrganisationUnit office { get; set; }
        public List<Location> locations { get; set; }
        public string link { get; set; }
        [JsonProperty(PropertyName = "DescriptionFormatted")]
        public string details { get; set; }
        public int? Radius { get; set; }
        public decimal? payment { get; set; }
        public DateTime creatingDate { get; set; }
        public DateTime modifyingDate { get; set; }
        public int? hoursPerWeek { get; set; }
        [JsonProperty(PropertyName = "PostingProfessions")]
        public List<PostingProfessions> HR_BA_ID { get; set; }

        #region ctor
        public Job()
        {
            details = string.Empty;
            HR_BA_ID = new List<PostingProfessions>();
        }
        #endregion //ctor
    }

    public class OrganisationUnit
    {
        public int ID { get; set; }
        public string cityName { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "ZipCode")]
        public int zip { get; set; }
        [JsonProperty(PropertyName = "CityName")]
        public string city { get; set; }
        [JsonProperty(PropertyName = "AddressLine")]
        public string address { get; set; }
        [JsonProperty(PropertyName = "District")]
        public string district { get; set; }
        public Region region { get; set; }
        public Country country { get; set; }
    }

    public class Region
    {
        public string regionName { get; set; }
    }

    public class Country
    {
        public string countryName { get; set; }
        public string ISOCode { get; set; }
    }

    public class PostingProfessions
    {
        public Profession profession { get; set; }
    }

    public class Profession
    {
        public int ID { get; set; }
    }

}
