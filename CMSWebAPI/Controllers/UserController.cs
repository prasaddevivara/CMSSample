using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMSWebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //[CustomAuthenticationFilter]
        public IEnumerable<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        [HttpGet]
        //[CustomAuthenticationFilter]
        public IEnumerable<User> GetUserByUserName(string UserName)
        {
            return _repository.GetUserByUserName(UserName);
        }

        [HttpPost]
        public void PostUsers(User user)
        {
            _repository.InsertUser(user);
        }
    }
}
