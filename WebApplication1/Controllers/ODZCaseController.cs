using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net.Http;
using CMSSample.DomainModel;
using System.Threading.Tasks;
using CMSSample.DomainModel.ViewModels;

namespace WebApplication1.Controllers
{
    public class ODZCaseController : Controller
    {
        private static string WebAPIURL = "https://localhost:44353/api/";

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<ODZCaseDisplayViewModel> odzcase = null;           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Clear();
                var responseTask = client.GetAsync("ODZCase");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<ODZCaseDisplayViewModel>>();
                    readJob.Wait();
                    odzcase = readJob.Result;                   

                    return View(odzcase);
                }
                else
                {
                    odzcase = Enumerable.Empty<ODZCaseDisplayViewModel>();
                    ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                }
            }

            return View(odzcase);
        }
    }
}