using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeDispController : Controller
    {
        private static string WebAPIURL = "http://localhost/CMSWebAPI/api/";
        // GET: HomeDisp
        public ActionResult Index()
        {
            var usrName = Session["UserName"];

            UserDisplayViewModel user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Clear();
                var responseTask = client.GetAsync("User/" + usrName + "/ByUserName");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<UserDisplayViewModel>();
                    readJob.Wait();
                    user = readJob.Result;
                }
                else
                {
                    //user = Enumerable.Empty<UserViewModel>();
                    ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                }
            }

            return View(user);
        }

        
        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["TokenNumber"] = null;

            return RedirectToAction("Login", "Login");
        }
    }
}