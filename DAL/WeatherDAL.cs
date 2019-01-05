using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace Capstone.Web.DAL
{
    public class WeatherDAL : IWeatherDAL
    {
        private readonly string connectionString;

        public WeatherDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Weather> GetWeather(string parkCode)
        {
            IList<Weather> weather = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM weather WHERE parkCode = @parkCode;", conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();  

                    while (reader.Read())
                    {
                        Weather weatherObject = new Weather();

                        weatherObject.Code = parkCode;
                        weatherObject.Day = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weatherObject.LowTemp = Convert.ToInt32(reader["low"]);
                        weatherObject.HighTemp = Convert.ToInt32(reader["high"]);
                        weatherObject.Forecast = Convert.ToString(reader["forecast"]);

                        weather.Add(weatherObject);
                    }

                }
            }
            catch(Exception ex)
            {

            }

            return weather;
        }

        public string CreateForecastRecommendation(string forecast)
        {
            string recommendation = "";

            switch (forecast)
            {
                case "rain":
                    recommendation = "It's going to rain! Pack rain gear and wear waterproof shoes.";
                    break;
                case "snow":
                    recommendation = "It's going to snow! Pack snowshoes and a warm coat.";
                    break;
                case "thunderstorms":
                    recommendation = "During storms, seek shelter and avoid hiking on exposed ridges.";
                    break;
                case "sunny":
                    recommendation = "It's going to be sunny! Pack sunblock.";
                    break;
                case "partly cloudy":
                    recommendation = "It's going to be partly cloudy! Nice day for hiking!";
                    break;
            }

            return recommendation;
        }

        public string CreateTemperatureRecommendation(int low, int high)
        {
            string recommendation = "";

            if (high > 75)
            {
                recommendation = "Bring an extra gallon of water.";
            }
            else if(low < 20)
            {
                recommendation = "Avoid prolonged exposure to frigid temperatures.";
            }
            else if(high - low > 20)
            {
                recommendation = "Wear breathable layers.";
            }

            return recommendation;
        }
    }
}
