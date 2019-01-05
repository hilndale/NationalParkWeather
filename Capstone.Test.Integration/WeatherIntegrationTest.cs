using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Linq;

namespace Capstone.Test.Integration
{
    [TestClass]
    public class WeatherIntegrationTest : DatabaseTest
    {
        private IWeatherDAL _weatherDAL;
        private string parkCode;

        [TestInitialize]
        public void Initialize()
        {
            _weatherDAL = new WeatherDAL(NpGeekDbConnectionString);

            parkCode = "CVNP";
        }

        [TestMethod]
        public void noWeatherExistsInDB()
        {
            var weather = _weatherDAL.GetWeather(parkCode);
            Assert.IsFalse(weather.Any());
        }

        [TestMethod]
        public void whenGetWeatherRunsItReturnsAListOfFiveWeatherObjects()
        {
            //Arrange
            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                INSERT INTO weather VALUES('CVNP',1,38,62,'rain');
                INSERT INTO weather VALUES('CVNP',2,38,56,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',3,51,66,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',4,55,65,'rain');
                INSERT INTO weather VALUES('CVNP',5,53,69,'thunderstorms');";

                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }

            //Act
            IList<Weather> weather = _weatherDAL.GetWeather(parkCode);

            //Assert
            Assert.AreEqual(5, weather.Count);
        }

        [TestMethod]
        public void whenGetWeatherRunsItReturnsCVNPFirstParkCode()
        {
            //Arrange
            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                INSERT INTO weather VALUES('CVNP',1,38,62,'rain');
                INSERT INTO weather VALUES('CVNP',2,38,56,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',3,51,66,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',4,55,65,'rain');
                INSERT INTO weather VALUES('CVNP',5,53,69,'thunderstorms');";

                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }

            //Act
            IList<Weather> weather = _weatherDAL.GetWeather(parkCode);

            //Assert
            Assert.AreEqual("CVNP", weather[0].Code);
        }

        [TestMethod]
        public void whenCreateForecastRecommendationRunsReturnsRainRecommendation()
        {
            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                INSERT INTO weather VALUES('CVNP',1,38,62,'rain');
                INSERT INTO weather VALUES('CVNP',2,38,56,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',3,51,66,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',4,55,65,'rain');
                INSERT INTO weather VALUES('CVNP',5,53,69,'thunderstorms');";

                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }

            IList<Weather> weather = _weatherDAL.GetWeather(parkCode);
            string recommendation = _weatherDAL.CreateForecastRecommendation(weather[0].Forecast);

            Assert.AreEqual("It's going to rain! Pack rain gear and wear waterproof shoes.", recommendation);

        }

        [TestMethod]
        public void whenCreateTemperatureRecommendationRunsReturnsLayerRecommendation()
        {
            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                INSERT INTO weather VALUES('CVNP',1,38,62,'rain');
                INSERT INTO weather VALUES('CVNP',2,38,56,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',3,51,66,'partly cloudy');
                INSERT INTO weather VALUES('CVNP',4,55,65,'rain');
                INSERT INTO weather VALUES('CVNP',5,53,69,'thunderstorms');";

                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }

            IList<Weather> weather = _weatherDAL.GetWeather(parkCode);
            string recommendation = _weatherDAL.CreateTemperatureRecommendation(weather[0].LowTemp, weather[0].HighTemp);

            Assert.AreEqual("Wear breathable layers.", recommendation);
        }
    }
}
