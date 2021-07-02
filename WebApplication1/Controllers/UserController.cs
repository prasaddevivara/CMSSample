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
using WebApplication1.Common;

namespace WebApplication1.Controllers
{
    [NoDirectAccess]
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
                        TempData["UserEditVM"] = usrs;
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
            UserEditViewModel usreditvm = null;
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        usereditviewmodel.Password = PwdEncrAndDecr.Encrypt(usereditviewmodel.Password);
                        CommonHttpProps(client);
                        var responseTask = client.PostAsJsonAsync<UserEditViewModel>("User", usereditviewmodel);
                        try
                        {
                            responseTask.Wait();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "User");
                        }

                        ModelState.AddModelError(string.Empty, Convert.ToString(result.ReasonPhrase));
                        logger.Error(DateTime.Now + ": " + Convert.ToString(result));
                    }
                }                

                if (TempData.ContainsKey("UserEditVM"))
                    usreditvm = (UserEditViewModel)TempData["UserEditVM"];

                TempData.Keep("UserEditVM");

                return View(usreditvm);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View(usreditvm);
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
                UserDeleteViewModel userdeletevm = new UserDeleteViewModel()
                {
                    UserId = Id,
                    DeletedAt = DateTime.Now,
                    IsDeleted=true
                };

                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseTask = client.PutAsJsonAsync("User/" + Id + "/UserRemove", userdeletevm);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json(new { status = "Success", message = "User Deleted Succesfully!" });
                    }
                }
                ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");

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