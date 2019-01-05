using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IWeatherDAL
    {


        IList<Weather> GetWeather(string parkCode);
        string CreateForecastRecommendation(string forecast);
        string CreateTemperatureRecommendation(int low, int high);
    }
}
