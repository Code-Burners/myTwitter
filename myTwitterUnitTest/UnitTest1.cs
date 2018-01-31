using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using myTwitterProject.Models;
using myTwitterProject.Controllers;

namespace myTwitterUnitTest
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var NoOfStatus = 10;
            var controller = new StatusController();
            var response = controller.Getstatus(NoOfStatus);
            Console.Write(response);
        }
    }
}
