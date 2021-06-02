using CMSSample.DA;
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
            try
            {
                return _repository.GetUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        //[CustomAuthenticationFilter]
        public User GetUserByID(int UserId)
        {
            return _repository.GetUserByID(UserId);
        }

        [HttpGet]
        //[CustomAuthenticationFilter]
        public UserDisplayViewModel GetUserByUserName(string UserName)
        {
            return _repository.GetUserByUserName(UserName);
            //return _repository.GetUsers().Where(x => x.UserName == UserName).ToList();
        }

        [HttpPut]
        //[CustomAuthenticationFilter]
        public void UpdateUser(User usr)
        {
            _repository.UpdateUser(usr);
        }

        
        //[CustomAuthenticationFilter]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        [HttpPost]
        public void PostUsers(User user)
        {
            _repository.InsertUser(user);
        }
    }
}
