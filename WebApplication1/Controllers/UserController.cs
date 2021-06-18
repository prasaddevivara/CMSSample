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
using System.Net.Http.Headers;

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
                if (!String.IsNullOrEmpty(HttpContext.Request.Cookies.Get("TokenNumber").Value.ToString()))
                {
                    IEnumerable<UserDisplayViewModel> user = null;
                    using (var client = new HttpClient())
                    {
                        CommonHttpProps(client);
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
                            logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!");
                            return RedirectToAction("UnAuthorized", "Error");
                        }
                    }
                    return View(user);
                }
                else
                {
                    return RedirectToAction("UnAuthorized", "Error");
                }

            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }            
        }

        private void CommonHttpProps(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(WebAPIURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: HttpContext.Request.Cookies.Get("TokenNumber").Value.ToString());
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                UserEditViewModel usrs = null;
                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseTask = client.GetAsync("User/Edit");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<UserEditViewModel>();
                        readJob.Wait();
                        usrs = readJob.Result;                        
                        return View(usrs);
                    }
                    else
                    {                        
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
                    CommonHttpProps(client);
                    var responseTask = client.PostAsJsonAsync<UserEditViewModel>("User", usereditviewmodel);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
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
                        UserEditViewModel usr = new UserEditViewModel();
                        CommonHttpProps(client);
                        HttpResponseMessage response = client.GetAsync("User/" + id).Result;
                        usr = response.Content.ReadAsAsync<UserEditViewModel>().Result;
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
                    CommonHttpProps(client);
                    HttpResponseMessage response = client.PutAsJsonAsync("User", usr).Result;            
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
                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseDel = client.DeleteAsync("User/" + Id + "/UserRemove");
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