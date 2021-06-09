using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMSWebAPI.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public HttpResponseMessage ValidLogin(string UserName, string UserPassword)
        {            
            List<UserDisplayViewModel> usr = new List<UserDisplayViewModel>();
            usr = _repository.GetUsers().Where(x => x.UserName == UserName && x.Password == UserPassword).ToList();

            if (usr.Count() > 0)
            {
                //if (usr == "admin" && userPassword == "Admin")
                //{
                    return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(UserName));
                //}
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "User name and password is invalid");
        }

        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetEmployee()
        {
            return Request.CreateResponse(HttpStatusCode.OK, value: "Successfully Validated!");
        }
    }
}
