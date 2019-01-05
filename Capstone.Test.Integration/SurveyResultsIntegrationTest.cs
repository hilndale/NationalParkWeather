﻿using System;
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
    public class SurveyResultsIntegrationTest : DatabaseTest
    {
        //getsurveys, savesurvey, test that table is empty
        private ISurveyResultsDAL _surveyResultsDAL;

        [TestInitialize]
        public void Initialize()
        {
            _surveyResultsDAL = new SurveyResultsDAL(NpGeekDbConnectionString);
        }


        [TestMethod]
        public void whenSaveSurveyRunThenListOfSurveysEqualsOne()
        {

            using (var connection = new SqlConnection(NpGeekDbConnectionString))
            {
                const string sql =
                    @"INSERT park VALUES ('CVNP', 'Cuyahoga Valley National Park', 'Ohio', 32832, 696, 125, 0, 'Woodland', 2000, 2189849, 'Of all the paths you take in life, make sure a few of them are dirt.', 'John Muir', 'Though a short distance from the urban areas of Cleveland and Akron, Cuyahoga Valley National Park seems worlds away. The park is a refuge for native plants and wildlife, and provides routes of discovery for visitors. The winding Cuyahoga River gives way to deep forests, rolling hills, and open farmlands. Walk or ride the Towpath Trail to follow the historic route of the Ohio & Erie Canal', 0, 390);
                    INSERT park VALUES ('ENP', 'Everglades National Park', 'Florida', 1508538, 0, 35, 0, 'Tropical', 1934, 1110901, 'There are no other Everglades in the world. They are, they have always been, one of the unique regions of the earth; remote, never wholly known. Nothing anywhere else is like them.', 'Marjory Stoneman Douglas', 'The Florida Everglades, located in southern Florida, is one of the largest wetlands in the world. Several hundred years ago, this wetlands was a major part of a 5,184,000 acre watershed that covered almost a third of the entire state of Florida. The Everglades consist of a shallow sheet of fresh water that rolls slowly over the lowlands and through billions of blades of sawgrass. As water moves through the Everglades, it causes the sawgrass to ripple like green waves; this is why the Everglades received the nickname ""River of Grass.""', 8, 760);
                    INSERT park VALUES('GCNP', 'Grand Canyon National Park', 'Arizona', 1217262, 8000, 115, 120, 'Desert', 1919, 4756771, 'It is the one great wonders. . . every American should see.', 'Theodore Roosevelt', 'If there is any place on Earth that puts into perspective the grandiosity of Mother Nature, it is the Grand Canyon. The natural wonder, located in northern Arizona, is a window into the regio''s geological and Native American past. As one of the country''s first national parks, the Grand Canyon has long been considered a U.S. treasure, and continues to inspire scientific study and puzzlement.', 8, 450);";
                var command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();
                command.ExecuteNonQuery();
            }
            IList<FavoriteParks> surveys = new List<FavoriteParks>();
            Survey survey = new Survey { Id = 1, ParkCode = "CVNP", Email = "visitor@gmail.com", ActivityLevel = "sedentary", State = "Ohio" };

            _surveyResultsDAL.SaveSurvey(survey);
            surveys = _surveyResultsDAL.GetSurveyResults();

            Assert.AreEqual(1, surveys.Count);

        }

    }
}
