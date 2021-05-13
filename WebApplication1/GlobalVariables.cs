using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApplication1
{
    public static class GlobalVariables
    {
        public static HttpClient WebapiClient = new HttpClient();


        static GlobalVariables()
        {
            WebapiClient.BaseAddress = new Uri("https://localhost:44391/api/");
            WebapiClient.DefaultRequestHeaders.Clear();
            //WebapiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}