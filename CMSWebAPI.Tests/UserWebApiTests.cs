using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using Moq;
using System.Collections.Generic;
using CMSWebAPI.Models;
using System.Net.Http;
using CMSWebAPI.Controllers;

namespace CMSWebAPI.Tests
{
    [TestClass]
    public class UserWebApiTests
    {
       
        [TestMethod]
        public void Test_WebAPIGetAllUsers()
        {
            //IEnumerable<User> usr = new List<User>();
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository usrrep = new UserRepository(cmssampledacontext);
            UserController usrcntrlr = new UserController(usrrep);            
            var usr = usrcntrlr.GetUsers();           
            Assert.IsNotNull(usr);
        }

        //[TestMethod]
        //public void Test_WebAPIInsertUser()
        //{
        //    UserViewModel userviewmodel = new UserViewModel()
        //    {
        //        UserName="KarthikV",
        //        Password= "Test1234",
        //        FirstName= "Karthik",
        //        LastName="Vutukuri",
        //        Email="karthikv@gmail.com",
        //        Mobile="5468792130",
        //        DZId=1                
        //    };

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(WebAPIURL);
        //        var responseTask = client.PostAsJsonAsync<UserViewModel>("User", userviewmodel);
        //        responseTask.Wait();

        //        var result = responseTask.Result;
                
        //        Assert.IsTrue(result.IsSuccessStatusCode);
                
        //    }

            
       // }

    }
}
