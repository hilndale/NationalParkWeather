using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string Code { get; set; }

        public int Day { get; set; }

        public int LowTemp { get; set; }

        public int HighTemp { get; set; }

        public string Forecast { get; set; }

        public void ConvertTemp()
        {
           LowTemp = (int)(LowTemp - 32) * 5/9;
           HighTemp = (int)(HighTemp - 32) * 5/9;
        }
    }
}


