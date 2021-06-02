using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net.Http;
using CMSSample.DomainModel;
using System.Threading.Tasks;
using NLog;

namespace WebApplication1.Controllers
{
   
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string WebAPIURL = "https://localhost:44353/api/";
       
        [HttpGet]
        public ActionResult Index()
        {
            try
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
                    else
                    {
                        user = Enumerable.Empty<UserViewModel>();
                        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                        logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!") ;
                    }
                }
                return View(user);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }            
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                IEnumerable<DZ> dz = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebAPIURL);
                    client.DefaultRequestHeaders.Clear();
                    var responseTask = client.GetAsync("DZ/Edit");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<IList<DZ>>();
                        readJob.Wait();
                        dz = readJob.Result;
                        ViewBag.DZ = new SelectList(dz.ToList(), "DZId", "DZName");
                    }
                    else
                    {
                        dz = Enumerable.Empty<DZ>();
                        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                        logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userviewmodel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44353/api/");
                    var responseTask = client.PostAsJsonAsync<UserViewModel>("User", userviewmodel);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //return Json(new { status = "Success", message = "User Created Succesfully!" });

                        //ViewBag.Inserted = "User registered succesfully!";
                        return RedirectToAction("Index", "User");
                    }
                }
                ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!");

                return View();
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            try
            {

                if (id != 0)
                {
                    using (var client = new HttpClient())
                    {
                        IEnumerable<DZ> dzs = null;
                        UserViewModel usr = new UserViewModel();

                        client.BaseAddress = new Uri("https://localhost:44353/api/");
                        HttpResponseMessage response = client.GetAsync("User/GetUserByID?UserID=" + id).Result;
                        usr = response.Content.ReadAsAsync<UserViewModel>().Result;
                        HttpResponseMessage response1 = client.GetAsync("DZ/Edit").Result;
                        dzs = response1.Content.ReadAsAsync<IList<DZ>>().Result;
                        ViewBag.DZ = new SelectList(dzs.ToList(), "DZId", "DZName");

                        // usr.DZviewmodel = dzs;
                        return View(usr);
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(User usr)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44353/api/");
                    HttpResponseMessage response = client.PutAsJsonAsync("User/UpdateUser", usr).Result;
                    //return View(response.Content.ReadAsAsync<User>().Result);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                using (var client1 = new HttpClient())
                {
                    client1.DefaultRequestHeaders.Clear();
                    client1.BaseAddress = new Uri("https://localhost:44353/api/");
                    var responseDel = client1.DeleteAsync("User/Delete?id=" + Id);
                    responseDel.Wait(30000);

                    var result = responseDel.Result;

                    if (result.IsSuccessStatusCode)
                        return Json(new { status = "Success", message = "Deleted Succesfully!" });
                }

                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return RedirectToAction("Index", "User");
            }
        }
    }
}