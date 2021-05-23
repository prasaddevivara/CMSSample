using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using Moq;
using System.Collections.Generic;
using CMSWebAPI.Models;
using System.Net.Http;

namespace CMSWebAPI.Tests
{
    [TestClass]
    public class UserWebApiTests
    {
        private static string WebAPIURL = "https://localhost:44353/api/";

        [TestMethod]
        public void Test_WebAPIGetAllUsers()
        {
            IEnumerable<UserViewModel> user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Clear();
                var responseTask = client.GetAsync("User");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readJob.Wait();
                    user = readJob.Result;
                }
            }

            Assert.IsNotNull(user);
            //IEnumerable<UserViewModel> user = null;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(WebAPIURL);
            //    client.DefaultRequestHeaders.Clear();
            //    var responseTask = client.GetAsync("User");
            //    responseTask.Wait();

                //    var result = responseTask.Result;
                //    if (result.IsSuccessStatusCode)
                //    {
                //        var readJob = result.Content.ReadAsAsync<IList<UserViewModel>>();
                //        readJob.Wait();
                //        user = readJob.Result;
                //    }
                //    else
                //    {
                //        user = Enumerable.Empty<UserViewModel>();
                //        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                //    }
                //}
        }
    }
}
