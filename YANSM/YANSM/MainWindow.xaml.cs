using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YANSM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextInfo textInfo = new CultureInfo(CultureInfo.CurrentCulture.ToString()).TextInfo;
        public MainWindow()
        {
            InitializeComponent();
            GetCurrentWeather();
            GetForecast();
        }

        private void GetCurrentWeather()
        {
            Conditions currentWeather = Weather.GetCurrentConditions();
            lblCurrentCity.Content = textInfo.ToTitleCase(currentWeather.City);
            lblCurrentConditions.Content = textInfo.ToTitleCase(currentWeather.Condition);
            lblCurrentTemp.Content = String.Format("{0} °F", currentWeather.Temp);
        }

        private void GetForecast()
        {
            lblForecastTime.Content = "";
            lblForecastTemp.Content = "";
            List<Conditions> forecastWeather = Weather.GetForecastConditions();
            foreach (Conditions con in forecastWeather)
            {
                lblForecastTime.Content += DateTime.Parse(con.Time).ToString(CultureInfo.CurrentCulture)  + Environment.NewLine;
                lblForecastTemp.Content += String.Format("{0} °F", con.Temp) + Environment.NewLine;
            }
        }
    }
}
