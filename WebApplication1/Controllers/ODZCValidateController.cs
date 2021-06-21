using CMSSample.DomainModel.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;

namespace WebApplication1.Controllers
{
    [NoDirectAccess]
    public class ODZCValidateController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static string WebAPIURL = "http://localhost/CMSWebAPI/api/";

        [HttpGet]
        public ActionResult Index(int id)
        {
            TempData["ODZCaseID"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase UploadFile)
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {

                    int odzcaseid = 0;

                    if (TempData.ContainsKey("ODZCaseID"))
                        odzcaseid = Convert.ToInt32(TempData["ODZCaseID"]);

                    TempData.Keep("ODZCaseID");

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = odzcaseid + "_" + file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadedFiles/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

            //------------------------------------------------------------------------------------------------------
            //if (UploadFile.ContentLength > 0)
            //{
            //    string fileName = Path.GetFileName(UploadFile.FileName);
            //    string folderPath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

            //    UploadFile.SaveAs(folderPath);
            //}

            //ViewBag.Message = "File uploaded successfully!";
            //return View();
        }

        [HttpPost]
        public ActionResult ValidateCase(string validateDesc)
        {

            try
            {
                int odzcaseid = 0;

                if (TempData.ContainsKey("ODZCaseID"))
                    odzcaseid = Convert.ToInt32(TempData["ODZCaseID"]);

                TempData.Keep("ODZCaseID");

                string userName = Session["UserName"].ToString();

                ODZCaseValidateViewModel odzcasevalidatevm = new ODZCaseValidateViewModel()
                { 
                    ODZCaseID = odzcaseid,                   
                    ValidationDesc = validateDesc                   
                };                

                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseTask = client.PutAsJsonAsync("ODZCase/" + userName, odzcasevalidatevm);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json("Case Validated Successfully!");
                    }
                }
                ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");

                return Json("Case Validated Successfully!");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return Json("Case Validation Failed!");
            }
        }


        

        [HttpPost]
        public ActionResult CaseClose(string caseCloseDesc)
        {

            try
            {
                int odzcaseid = 0;

                if (TempData.ContainsKey("ODZCaseID"))
                    odzcaseid = Convert.ToInt32(TempData["ODZCaseID"]);

                TempData.Keep("ODZCaseID");

                string userName = Session["UserName"].ToString();

                ODZCaseCloseViewModel odzcaseclosevm = new ODZCaseCloseViewModel()
                {
                    ODZCaseID = odzcaseid,
                    ClosingDesc = caseCloseDesc
                };

                using (var client = new HttpClient())
                {
                    CommonHttpProps(client);
                    var responseTask = client.PutAsJsonAsync("ODZCase/" + userName + "/CaseClose", odzcaseclosevm);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json("Case Closed Successfully!");
                    }
                }
                ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");

                return Json("Case Closed Successfully!");
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                return Json("Failed to close case!");
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