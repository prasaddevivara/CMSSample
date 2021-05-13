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

        public IEnumerable<User> GetUsers()
        {
            return _repository.GetUsers();
        }

        [HttpPost]
        public void PostUsers(User user)
        {
            _repository.InsertUser(user);
        }
    }
}
