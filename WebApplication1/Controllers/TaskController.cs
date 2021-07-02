using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSSample.DomainModel.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApplication1.Common;

namespace WebApplication1.Controllers
{
    [NoDirectAccess]
    public class TaskController : Controller
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
                    IEnumerable<TaskDisplayViewModel> task = null;
                    using (var client = new HttpClient())
                    {
                        CommonHttpProps(client);
                        var responseTask = client.GetAsync("Task");
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readJob = result.Content.ReadAsAsync<IList<TaskDisplayViewModel>>();
                            readJob.Wait();
                            task = readJob.Result;
                        }
                        else
                        {
                            task = Enumerable.Empty<TaskDisplayViewModel>();
                            ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                            logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!");
                            return RedirectToAction("UnAuthorized", "Error");
                        }
                    }
                    return View(task);
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

        [HttpGet]
        public ActionResult Create(int Id)
        {
            try
            {
                TaskEditViewModel tsks = null;
                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseTask = client.GetAsync("Task/Edit?odzCaseId=" + Id + "&username=" + Session["UserName"].ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<TaskEditViewModel>();
                        readJob.Wait();
                        tsks = readJob.Result;
                        TempData["tsksVM"] = tsks;
                        return View(tsks);
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                    }
                }

                return View(tsks);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult TaskClose(TaskEditViewModel taskeditviewmodel)
        {
            TaskEditViewModel tskeditvm = null;

            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        taskeditviewmodel.CompletedDate = DateTime.Now;
                        CommonHttpProps(client);
                        HttpResponseMessage response = client.PutAsJsonAsync("Task", taskeditviewmodel).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return Json(new { status = "Success", message = "Task Closed Succesfully!" });
                        }
                    }
                    ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                    logger.Error(DateTime.Now + ": Server error occured.Please contact admin for help!");
                }

                if (TempData.ContainsKey("tsksVM"))
                    tskeditvm = (TaskEditViewModel)TempData["tsksVM"];

                TempData.Keep("tsksVM");

                return View(tskeditvm);

                return RedirectToAction("Index","Task");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return RedirectToAction("Index", "Task");
            }
        }

        [HttpPost]
        public ActionResult Create(TaskEditViewModel taskeditviewmodel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseTask = client.PostAsJsonAsync<TaskEditViewModel>("Task", taskeditviewmodel);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Task");
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
                        TaskEditViewModel tsk = new TaskEditViewModel();
                        CommonHttpProps(client);
                        HttpResponseMessage response = client.GetAsync("Task/" + id).Result;
                        tsk = response.Content.ReadAsAsync<TaskEditViewModel>().Result;
                        return View(tsk);
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
        public ActionResult Edit(TaskEditViewModel tsk)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    HttpResponseMessage response = client.PutAsJsonAsync("Task", tsk).Result;
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
                    var responseDel = client.DeleteAsync("Task/" + Id + "/TaskRemove");
                    responseDel.Wait(30000);

                    var result = responseDel.Result;

                    if (result.IsSuccessStatusCode)
                        return Json(new { status = "Success", message = "Deleted Succesfully!" });
                }

                return RedirectToAction("Index", "Task");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return RedirectToAction("Index", "Task");
            }
        }

        private void CommonHttpProps(HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(WebAPIURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: HttpContext.Request.Cookies.Get("TokenNumber").Value.ToString());
        }
    }
}