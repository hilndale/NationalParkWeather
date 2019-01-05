using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Capstone.Web.Extensions;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParkDAL _parkDAL;
        private readonly IWeatherDAL _weatherDAL;

        public HomeController(IParkDAL parkDAL, IWeatherDAL weatherDAL)
        {
            _parkDAL = parkDAL;
            _weatherDAL = weatherDAL;
        }

        public IActionResult Index()
        {
            IndexViewModel allParks = new IndexViewModel();

            allParks.Parks = _parkDAL.GetAllParks();

            return View(allParks);
        }

        [HttpGet]
        public IActionResult Detail(string code)
        {
            DetailViewModel park = new DetailViewModel();

            park.Park = _parkDAL.GetPark(code);
            park.Weather = _weatherDAL.GetWeather(code);

            park.ForecastRecommendation = _weatherDAL.CreateForecastRecommendation(park.Weather[0].Forecast);
            park.TemperatureRecommendation = _weatherDAL.CreateTemperatureRecommendation(park.Weather[0].LowTemp, park.Weather[0].HighTemp);

            if(HttpContext.Session.Get<string>("UnitOfTemp") == "celsius")
            {
                foreach(Weather weather in park.Weather)
                {
                    weather.ConvertTemp();
                }
            }

            return View(park);
        }

        public IActionResult SaveSession(string unitOfTemp, Park park)
        {
            HttpContext.Session.Set("UnitOfTemp", unitOfTemp);
            return RedirectToAction(nameof(Detail), "Home", new { code = park.Code });
        }

    }
}

