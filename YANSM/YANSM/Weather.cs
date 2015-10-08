using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace YANSM
{
    class Weather
    {
        //Duluth city ID is 5024719
        
        public string apiKey { get; set; }
        [System.ComponentModel.DefaultValue("5024719")] //Duluth, MN
        public string cityID { get; set; }
        
        private const string URL = "http://api.openweathermap.org/data/2.5/forecast/";
        
        /// <summary>
        /// Builds URL for OWM with apiKey from App.config
        /// </summary>
        /// <returns>String</returns>
        private string BuildUrl()
        {
            string completeUrl = URL;
            this.apiKey = ConfigurationManager.AppSettings["apiKey"];

            completeUrl += String.Format("city?id={0}&APPID={1}", cityID, this.apiKey);

            return completeUrl;
        }

        public void GetWeather()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BuildUrl());
        }


    }
}
