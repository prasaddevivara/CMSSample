using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
using CMSWebAPI.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [CustomAuthenticationFilter(Roles = "Admin")]
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
        [CustomAuthenticationFilter(Roles = "Admin")]
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

        [CustomAuthenticationFilter(Roles = "Admin")]
        [Route("api/User/{id}")]       
        public UserEditViewModel GetUserByID(int id)
        {
            return _repository.GetUserByID(id);
        }

        [CustomAuthenticationFilter(Roles = "Admin, User")]
        [Route("api/User/{UserName}/ByUserName")]
        public UserDisplayViewModel GetUserByUserName(string UserName)
        {
            return _repository.GetUserByUserName(UserName);            
        }

        [CustomAuthenticationFilter(Roles = "Admin")]
        [HttpPut]        
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

        [CustomAuthenticationFilter(Roles = "Admin")]
        [HttpPut, Route("api/User/{id}/UserRemove")]
        public void PutUserRemove([FromUri] int id, [FromBody] UserDeleteViewModel userdeletevm)
        {
            var usr = new User()
            {
                UserId = id,
                IsDeleted = true,
                DeletedAt = DateTime.Now
            };

            _repository.UpdateSoftDelete(usr);
        }

        [HttpPut, Route("api/User/{userName}/ChangePassword")]
        public void PutChangePassword([FromUri] string userName, [FromBody] ChangePasswordViewModel changepasswordvm)
        {
            var usr = new UserDisplayViewModel();
            usr = _repository.GetUserByUserName(userName);

            var user = new User()
            {
                UserId = usr.UserId,
               // UserName = userName,
               Password = changepasswordvm.Password
            };

             _repository.UpdatePassword(user);
        }

        [CustomAuthenticationFilter(Roles = "Admin")]
        [CustomExceptionFilter]
        [HttpPost]
        public HttpResponseMessage PostUsers(UserEditViewModel user)
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
                    CreationDate= DateTime.Now,
                    RoleID = Convert.ToInt32(user.RoleID)
                };

                _repository.InsertUser(usr);

                return Request.CreateResponse<UserEditViewModel>(HttpStatusCode.OK, user);
        }

        HttpResponseMessage TraceErrorAndReturnResponse(string message, HttpStatusCode statusCode)
        {
            Trace.TraceError(Request.Headers.ToString());
            Trace.TraceError(message);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        }
    }
}
