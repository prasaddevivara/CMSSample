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
using NLog;

namespace WebApplication1.Controllers
{
    public class ODZCaseController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string WebAPIURL = "https://localhost:44353/api/";

        [HttpGet]
        public ActionResult Index(string assistedperson, int? casereference)
        {
            try
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
                        if (casereference == null && string.IsNullOrEmpty(assistedperson))
                        {
                            return View(odzcase);
                        }
                        else if (casereference == null && !string.IsNullOrEmpty(assistedperson))
                        {
                            return View(odzcase.Where(x => x.AssistedPerson.ToLower().Contains(assistedperson.ToLower())).ToList());
                            //.Where(x => x.AssistedPerson == assistedperson).ToList();
                            //return View(odzcase);
                        }
                        else if (casereference != null && string.IsNullOrEmpty(assistedperson))
                        {
                            return View(odzcase.Where(x => x.ODZCaseReference == casereference).ToList());
                            //return View(odzcase);
                        }
                        else if (casereference != null && !string.IsNullOrEmpty(assistedperson))
                        {
                            return View(odzcase.Where(x => x.ODZCaseReference == casereference)
                                .Where(x => x.AssistedPerson.ToLower().Contains(assistedperson.ToLower())).ToList());
                        }
                        else
                        {
                            return View(odzcase);
                        }
                    }
                    else
                    {
                        odzcase = Enumerable.Empty<ODZCaseDisplayViewModel>();
                        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                    }
                }

                return View(odzcase);
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
                ODZCaseEditViewModel odzc = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebAPIURL);
                    client.DefaultRequestHeaders.Clear();
                    var responseTask = client.GetAsync("ODZCase/Edit");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<ODZCaseEditViewModel>();
                        readJob.Wait();
                        odzc = readJob.Result;
                        // ViewBag.DZ = new SelectList(dz.ToList(), "DZId", "DZName");
                        return View(odzc);
                    }
                    else
                    {
                        // odzc = ODZCaseEditViewModel.;
                        ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                    }
                }

                return View(odzc);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View();
            }            
        }

        [HttpPost]
        public ActionResult Create(ODZCaseEditViewModel odzcaseEditViewModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44353/api/");
                    var responseTask = client.PostAsJsonAsync<ODZCaseEditViewModel>("ODZCase", odzcaseEditViewModel);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");

                return View(odzcaseEditViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return View(odzcaseEditViewModel);
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
                        //IEnumerable<DZ> dzs = null;
                        ODZCaseEditViewModel odzc = new ODZCaseEditViewModel();

                        client.BaseAddress = new Uri("https://localhost:44353/api/");
                        HttpResponseMessage response = client.GetAsync("ODZCase/" + id).Result;
                        odzc = response.Content.ReadAsAsync<ODZCaseEditViewModel>().Result;
                        return View(odzc);
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
        public ActionResult Edit(ODZCaseEditViewModel odzc)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44353/api/");
                    HttpResponseMessage response = client.PutAsJsonAsync("ODZCase", odzc).Result;
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
                    var responseDel = client1.DeleteAsync("ODZCase/" + Id + "/CaseRemove");
                    responseDel.Wait(30000);

                    var result = responseDel.Result;

                    if (result.IsSuccessStatusCode)
                        return Json(new { status = "Success", message = "Deleted Succesfully!" });
                }

                return RedirectToAction("Index", "ODZCase");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return RedirectToAction("Index", "ODZCase");
            }
        }
    }
}