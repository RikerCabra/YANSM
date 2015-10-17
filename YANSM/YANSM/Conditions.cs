using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANSM
{
    class Conditions
    {
        public string Time { get; set; }
        public string City {get; set; }
        public string Precipitation { get; set; } //NEW
        public string Condition { get; set; }
        public string ConditionIcon { get; set; } //NEW
        public string Wind { get; set; } //NEW
        public string WindDirection { get; set; } //NEW
        public string Clouds { get; set; } //NEW
        public string Temp { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
    }
}
