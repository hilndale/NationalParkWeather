using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class DetailViewModel
    {
        public Park Park { get; set; }

        public IList<Weather> Weather { get; set; }

        public string ForecastRecommendation { get; set; }

        public string TemperatureRecommendation { get; set; }

        public string UnitOfTemp { get; set; }
    }
}
