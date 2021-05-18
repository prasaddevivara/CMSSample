using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel usr)
        {
            IEnumerable<UserViewModel> user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                var responseTask = client.GetAsync("User?UserName=" + usr.UserName);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readJob.Wait();
                    user = readJob.Result;
                }
            }
            //IEnumerable<UserViewModel> user = null;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44353/api/");
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

            //return View(user);

            return RedirectToAction("Index", "User", new { area = "" });

        }
    }
}