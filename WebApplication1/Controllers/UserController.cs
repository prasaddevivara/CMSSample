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
using CMSSample.DomainModel.ViewModels;

namespace WebApplication1.Controllers
{
   
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string WebAPIURL = "http://localhost/CMSWebAPI/api/";
       
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<UserDisplayViewModel> user = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebAPIURL);
                    client.DefaultRequestHeaders.Clear();
                    var responseTask = client.GetAsync("User");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<IList<UserDisplayViewModel>>();
                        readJob.Wait();
                        user = readJob.Result;                        
                    }
                    else
                    {
                        user = Enumerable.Empty<UserDisplayViewModel>();
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
                //IEnumerable<DZ> dz = null;
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(WebAPIURL);
                //    client.DefaultRequestHeaders.Clear();
                //    var responseTask = client.GetAsync("DZ/Edit");
                //    responseTask.Wait();

                //    var result = responseTask.Result;
                //    if (result.IsSuccessStatusCode)
                //    {
                //        var readJob = result.Content.ReadAsAsync<IList<DZ>>();
                //        readJob.Wait();
                //        dz = readJob.Result;
                //        ViewBag.DZ = new SelectList(dz.ToList(), "DZId", "DZName");
                //    }
                //    else
                //    {
                //        dz = Enumerable.Empty<DZ>();
                //        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                //        logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!");
                //    }
                //}

                //return View();
                UserEditViewModel usrs = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebAPIURL);
                    client.DefaultRequestHeaders.Clear();
                    var responseTask = client.GetAsync("User/Edit");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<UserEditViewModel>();
                        readJob.Wait();
                        usrs = readJob.Result;
                        // ViewBag.DZ = new SelectList(dz.ToList(), "DZId", "DZName");
                        return View(usrs);
                    }
                    else
                    {
                        // odzc = ODZCaseEditViewModel.;
                        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                    }
                }

                return View(usrs);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(UserEditViewModel usereditviewmodel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost/CMSWebAPI/api/");
                    var responseTask = client.PostAsJsonAsync<UserEditViewModel>("User", usereditviewmodel);
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
                        UserEditViewModel usr = new UserEditViewModel();

                        client.BaseAddress = new Uri("http://localhost/CMSWebAPI/api/");
                        HttpResponseMessage response = client.GetAsync("User/" + id).Result;
                        usr = response.Content.ReadAsAsync<UserEditViewModel>().Result;
                        //HttpResponseMessage response1 = client.GetAsync("DZ/Edit").Result;
                        //dzs = response1.Content.ReadAsAsync<IList<DZ>>().Result;
                        //ViewBag.DZ = new SelectList(dzs.ToList(), "DZId", "DZName");

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
        public ActionResult Edit(UserEditViewModel usr)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost/CMSWebAPI/api/");
                    HttpResponseMessage response = client.PutAsJsonAsync("User", usr).Result;
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
                    client1.BaseAddress = new Uri("http://localhost/CMSWebAPI/api/");
                    var responseDel = client1.DeleteAsync("User/" + Id + "/UserRemove");
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