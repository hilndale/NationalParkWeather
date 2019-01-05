using Capstone.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Test.Unit
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _subject;

        [TestInitialize]
        public void Initialize()
        {
            _subject = new HomeController();
        }

        [TestClass]
        public class Index : HomeControllerTest
        {
            [TestMethod]
            public void ReturnsAViewBasedOnActionName()
            {
                ViewResult result = _subject.Index() as ViewResult
            }
        }

    }
}
