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
        private readonly IUserRolesRepository _usrroleprepository;
        private readonly IDZRepository _dzrepository;

        public UserController(IUserRepository repository, IUserRolesRepository usrroleprepository, IDZRepository dzrepository)
        {
            _repository = repository;
            _usrroleprepository = usrroleprepository;
            _dzrepository = dzrepository;
        }

        public IEnumerable<UserDisplayViewModel> Get()
        {
            try
            {
                return _repository.GetUsers().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/User/Edit")]
        public UserEditViewModel GetUserByEdit()
        {
            var usrs = new UserEditViewModel()
            {
                UserRoles = _usrroleprepository.GetAllUserRoles(),
                UserDZs = _dzrepository.GetDZs()
            };

            return usrs;
        }

        [Route("api/User/{id}")]
        //[CustomAuthenticationFilter]
        public UserEditViewModel GetUserByID(int id)
        {
            return _repository.GetUserByID(id);
        }


        [Route("api/User/{UserName}/ByUserName")]
        public UserDisplayViewModel GetUserByUserName(string UserName)
        {
            return _repository.GetUserByUserName(UserName);
            //return _repository.GetUsers().Where(x => x.UserName == UserName).ToList();
        }

        [HttpPut]
        //[CustomAuthenticationFilter]
        public void UpdateUser(UserEditViewModel user)
        {
            var usr = new User()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                DZId = Convert.ToInt32(user.DZId),
                RoleID = Convert.ToInt32(user.RoleID)
            };
            _repository.UpdateUser(usr);
        }


        //[CustomAuthenticationFilter]
        [HttpDelete, Route("api/User/{id}/UserRemove")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        [HttpPost]
        public void PostUsers(UserEditViewModel user)
        {
            var usr = new User()
            {
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                DZId = Convert.ToInt32(user.DZId),
                RoleID = Convert.ToInt32(user.RoleID)
            };
                       
            _repository.InsertUser(usr);
        }
    }
}
