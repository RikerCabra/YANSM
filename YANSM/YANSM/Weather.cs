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
using System.Data.Objects;
using System.Windows;
using System.Xml;

namespace YANSM
{
    class Weather
    {
        private static string apiKey, cityID;
        private const string baseURL= "http://api.openweathermap.org/data/2.5/";

        /// <summary>
        /// Builds URL for OWM with apiKey from App.config
        /// </summary>
        /// <returns>API Call URL</returns>
        private static string BuildUrl(string weatherType)
        {
            string completeUrl = baseURL + weatherType;
            apiKey = ConfigurationManager.AppSettings["apiKey"];
            cityID = ConfigurationManager.AppSettings["cityID"];
            System.Diagnostics.Debug.Write(apiKey + " || " + cityID);
            completeUrl += String.Format("?id={0}&APPID={1}&mode=xml&units=imperial", cityID, apiKey);

            return completeUrl;
        }
        
        public static Conditions GetCurrentConditions()
        {
            Conditions current = new Conditions();

            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load(BuildUrl("weather"));

            if(xmlConditions.SelectSingleNode("/current/city") == null)
            {
                current.City = "City Not Found";
            }
            else
            {
                current.City = xmlConditions.SelectSingleNode("/current/city").Attributes["name"].Value;
                current.Condition = xmlConditions.SelectSingleNode("/current/weather").Attributes["value"].Value;
                current.Temp = xmlConditions.SelectSingleNode("/current/temperature").Attributes["value"].Value;
                
            }

            return current;
        }

        public static List<Conditions> GetForecastConditions()
        {
            List<Conditions> forecast = new List<Conditions>();
            

            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load(BuildUrl("forecast/city"));

            if (xmlConditions.SelectSingleNode("/weatherdata/location/name") == null)
            {
                Conditions noCity = new Conditions();
                noCity.City = "City Not Found";
                forecast.Add(noCity);
            }
            else
            {
                XmlNodeList nodeList = xmlConditions.GetElementsByTagName("time");
                foreach(XmlNode node in nodeList)
                {
                    Conditions condition = new Conditions();
                    condition.City = xmlConditions.SelectSingleNode("/weatherdata/location/name").InnerText;

                    condition.Time = node.SelectSingleNode("/weatherdata/forecast/time").Attributes["from"].Value;
                    //condition.Condition = node.SelectSingleNode("/weatherdata/forecast/time").Attributes["value"].Value;
                    condition.Temp = node.SelectSingleNode("/weatherdata/forecast/time/temperature").Attributes["value"].Value;

                    forecast.Add(condition);
                }
            }

            return forecast;
        }

    }
}
