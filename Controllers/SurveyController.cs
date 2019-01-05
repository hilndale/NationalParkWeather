using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {

        ISurveyResultsDAL _surveyResultsDAL;

        public SurveyController(ISurveyResultsDAL surveyResultsDAL)
        {
            _surveyResultsDAL = surveyResultsDAL;
        }

        [HttpGet]
        public IActionResult Survey()
        {
            ViewBag.parkCodeList = parkCodes;
            ViewBag.activityLevelList = activityLevels;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Survey(Survey survey)
        {
            _surveyResultsDAL.SaveSurvey(survey);
            return RedirectToAction(nameof(Results));
        }
        
        public IActionResult Results()
        {
            SurveyViewModel parks = new SurveyViewModel();
            parks.Parks = _surveyResultsDAL.GetSurveyResults();

            return View(parks);
        }

         public static List<SelectListItem> parkCodes = new List<SelectListItem>()
         {
            new SelectListItem() { Text = "Cuyahoga Valley National Park", Value = "CVNP"},
            new SelectListItem() { Text = "Everglades National Park" , Value = "ENP"},
            new SelectListItem() { Text = "Grand Canyon National Park", Value = "GCNP" },
            new SelectListItem() { Text = "Glacier National Park", Value = "GNP" },
            new SelectListItem() { Text = "Great Smoky Mountains National Park", Value = "GSMNP" },
            new SelectListItem() { Text = "Grand Teton National Park", Value = "GTNP" },
            new SelectListItem() { Text = "Mount Rainier National Park", Value = "MRNP" },
            new SelectListItem() { Text = "Rocky Mountain National Park", Value = "RMNP" },
            new SelectListItem() { Text = "Yellowstone National Park", Value = "YNP" },
            new SelectListItem() { Text = "Yosemite National Park", Value = "YNP2" }
         };

        public static List<SelectListItem> activityLevels = new List<SelectListItem>()
        {
            new SelectListItem() { Text = "Inactive", Value = "Inactive"},
            new SelectListItem() { Text = "Sedentary" , Value = "Sedentary"},
            new SelectListItem() { Text = "Active", Value = "Active" },
            new SelectListItem() { Text = "Extremely Active", Value = "Extremely Active" },
        };


    }
}