using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myTwitterProject.Controllers;
using myTwitterProject.Models;
using System.Web.Http;
using System.Net.Http;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;



namespace myTwitterUnitTest
{

    [TestClass]
    public class StatusControllerTest
    {
        
        public StatusController controller = new StatusController();


        [TestMethod]
        public void GetStatus_NullTest()
        {
            var NoOfStatus = 10;
            var response = controller.Getstatus(NoOfStatus);
            IQueryable<status> list = response;
            Assert.IsNotNull(response,"The controller returned an empty object");
           
        }

        [TestMethod]
        public void GetStatus_ReturnsRequiredNoofStatus()
        {
            var NoOfStatus = 10;
            var response = controller.Getstatus(NoOfStatus);
            IQueryable<status> list = response;
            Assert.IsTrue(list.Count() < NoOfStatus, "The Controller did not return the required number of status");

        }

        [TestMethod]
        public async Task PutStatus_NoIdProvided()
        {
            status check= new status();
            check.StatusId = 3;
            var response = await controller.Putstatus(2,check);
            Assert.IsInstanceOfType(response, typeof(BadRequestResult), "Id does not match but the controller returned a valid response");
            
        }


        [TestMethod]
        public async Task PutStatus_IdNotFound()
        {

            status check = new status();
            check.StatusId = 15;
            var response = await controller.Putstatus(15, check);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task PutStatus_SuccesfullUpdationShouldRetturnOK()
        {

            status check = new status();
            check.StatusId = 56;
            var response = await controller.Putstatus(56, check);
            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));

        }



        [TestMethod]
        public async Task PostStatus_StatusCreatedAndNotNull()
        {

            status check = new status();
            check.statuses = "Hello world";
            var response = await controller.Poststatus(check);
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<status>;
            Assert.IsNotNull(response);            
            
        }



        [TestMethod]
        public async Task PostStatus_ReturnsValidRoute()
        {

            status check = new status();
            check.statuses = "Hello world";
            var response = await controller.Poststatus(check);
            var createdResult = response as CreatedAtRouteNegotiatedContentResult<status>;            
            Assert.AreEqual("DefaultApi", createdResult.RouteName);

        }

        [TestMethod]
        public async Task DeleteStatus_WrongIdProvided()
        {

            var response = await controller.Deletestatus(12);            
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));

        }


        [TestMethod]
        public async Task DeleteStatus_ShouldReturnOk()
        {

            var response = await controller.Deletestatus(76);            
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }


        [TestMethod]
        public async Task PostLike_IdNotFound()
        {

            status check = new status();
            check.StatusId = 15;
            check.like = "2";
            var response = await controller.Postlike(15, check);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));

        }


        [TestMethod]
        public async Task PostLike_SuccesfullUpdationShouldReturnOK()
        {

            status check = new status();
            check.StatusId = 56;
            check.like = "3";
            var response = await controller.Postlike(56, check);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteAccount_InvalidUserShouldReturnBadRequest()
        {

            status check = new status();          
            var response = await controller.DeleteAcc(check);
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task DeleteAccount_ShouldReturnOK()
        {

            status check = new status();
            check.Name = "Sam";
            var response = await controller.DeleteAcc(check);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

    }
}