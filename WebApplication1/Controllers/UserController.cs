using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net.Http;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<UserViewModel> user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                var responseTask = client.GetAsync("User");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readJob.Wait();
                    user = readJob.Result;
                }
                else
                {
                    user = Enumerable.Empty<UserViewModel>();
                    ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                }
            }

            return View(user);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userviewmodel)
        {          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44353/api/");
                var responseTask = client.PostAsJsonAsync<UserViewModel>("User", userviewmodel); 
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");

            return View(userviewmodel);
        }
    }
}