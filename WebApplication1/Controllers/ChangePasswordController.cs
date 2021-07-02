using CMSSample.DomainModel.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;

namespace WebApplication1.Controllers
{
    public class ChangePasswordController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string WebAPIURL = "http://localhost/CMSWebAPI/api/";

        // GET: ChangePassword
        public ActionResult Index()
        {
            ChangePasswordViewModel changepwdvm = new ChangePasswordViewModel()
            {
                UserName = Session["UserName"].ToString()               
            };
            return View(changepwdvm);
        }

        [HttpPost]
        public ActionResult Index(ChangePasswordViewModel changepasswordvm)
        {
            string userName="";
            userName = Session["UserName"].ToString();
            ChangePasswordViewModel changepwdvm = new ChangePasswordViewModel()
            {
                UserName = userName,
                Password = string.IsNullOrEmpty(changepasswordvm.Password)? "" : PwdEncrAndDecr.Encrypt(changepasswordvm.Password)
            };            
            try
            {
                if (ModelState.IsValid)
                {


                    using (var client = new HttpClient())
                    {
                        CommonHttpProps(client);
                        var responseTask = client.PutAsJsonAsync("User/" + userName + "/ChangePassword", changepwdvm);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.ChangePwdSuccess = "Password Changed Successfully!";
                        }
                    }
                   // ModelState.AddModelError(String.Empty, "Server error occured.  Please contact admin for help");
                }
                
                return View(changepwdvm);
            }
            catch (Exception ex)
            {
                logger.Error(DateTime.Now + ": " + ex.Message);
                logger.Error(DateTime.Now + ": " + ex.StackTrace);
                ViewBag.ChangePwdSuccess = "Failed to change password !";
                return View(changepwdvm);
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