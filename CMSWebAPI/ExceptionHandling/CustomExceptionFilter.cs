using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace CMSWebAPI.ExceptionHandling
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {        
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else if (((System.Data.SqlClient.SqlException)actionExecutedContext.Exception.InnerException.InnerException).Number == 2601)
            {
                exceptionMessage = "UserName already exist! duplicates are not allowed.";
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }
            //We can log this exception message to the file or database.
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service."),
                ReasonPhrase = exceptionMessage
            };
            actionExecutedContext.Response = response;
        }
    }
}