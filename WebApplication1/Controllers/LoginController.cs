using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(CMSLoginViewModel usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<UserViewModel> user = null;
                    var tokenBased = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.BaseAddress = new Uri("http://localhost/CMSWebAPI/api/");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                        var responseMessage = await client.GetAsync(requestUri: "Account/ValidLogin?UserName=" + usr.UserName + "&UserPassword=" + usr.Password);

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                            tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);
                            System.Web.HttpContext.Current.Response.Cookies.Add(new System.Web.HttpCookie("TokenNumber")
                            {
                                Value = tokenBased,
                                HttpOnly = true
                            });
                            //Session["TokenNumber"] = tokenBased;
                            Session["UserName"] = usr.UserName;
                            return RedirectToAction("Index", "HomeDisp", new { area = "" });
                        }
                        else
                        {
                            ViewBag.NotValidUser = "Invalid UserName or Passwrod!";
                            return View();
                        }
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
    }
}