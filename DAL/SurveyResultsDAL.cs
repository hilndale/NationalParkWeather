using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class SurveyResultsDAL : ISurveyResultsDAL
    {
        private readonly string connectionString;

        public SurveyResultsDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<FavoriteParks> GetSurveyResults()
        {
            IList<FavoriteParks> parks = new List<FavoriteParks>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select Count(*) as count, sr.parkCode, parkName FROM survey_result sr JOIN park p on p.parkCode = sr.parkCode GROUP by sr.parkCode, parkName ORDER BY count desc, parkName;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        FavoriteParks park = new FavoriteParks();

                        park.Name = Convert.ToString(reader["parkName"]);
                        park.Code = Convert.ToString(reader["parkCode"]);
                        park.Count = Convert.ToInt32(reader["count"]);

                        parks.Add(park);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return parks;
        }

        public void SaveSurvey(Survey survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@parkCode, @emailAddress, @state, @activityLevel);", conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.Email);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
