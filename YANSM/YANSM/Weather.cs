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
                XmlNodeList nodeList = xmlConditions.GetElementsByTagName("city");
                for (int i = 0; i < nodeList.Count; i++)
                    current.City = nodeList[i].Attributes["name"].Value;

                nodeList = xmlConditions.GetElementsByTagName("weather");
                for (int i = 0; i < nodeList.Count; i++)
                    current.Condition = nodeList[i].Attributes["value"].Value;

                nodeList = xmlConditions.GetElementsByTagName("temperature");
                for (int i = 0; i < nodeList.Count; i++)
                    current.Temp = nodeList[i].Attributes["value"].Value;
                
            }

            return current;
        }

        public static Conditions GetForecastConditions()
        {
            Conditions forecast = new Conditions();

            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load(BuildUrl("forecast/city"));

            if (xmlConditions.SelectSingleNode("/weatherdata/location/name") == null)
            {
                forecast.City = "NOOOOOOOOOOOOOOOOOOOOOOB";
            }

            return forecast;
        }

    }
}
