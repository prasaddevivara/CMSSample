using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CMSSample.DomainModel.ViewModels;

namespace CMSSample.DA.Repository
{
    public class UserRepository : IUserRepository
    {
        private CMSSampleDAContext _context;

        public UserRepository(CMSSampleDAContext cmssampledacontext)
        {
            this._context = cmssampledacontext;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<UserDisplayViewModel> GetUsers()
        {
            using (_context)
            {
                List<User> users = new List<User>();
                users = _context.User.AsNoTracking()
                    .Include(x => x.UserRoles)
                    .Include(x => x.DZ)
                    .ToList();

                if (users != null)
                {
                    List<UserDisplayViewModel> usersDisplay = new List<UserDisplayViewModel>();
                    foreach (var x in users)
                    {
                        var userDisplay = new UserDisplayViewModel()
                        {
                            UserId = x.UserId,
                            UserName = x.UserName,
                            Password = x.Password,
                            RoleName = x.UserRoles.RoleName,
                            DZName = x.DZ.DZName,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Mobile = x.Mobile
                        };
                        usersDisplay.Add(userDisplay);
                    }
                    return usersDisplay;
                }
                return null;
            }
        }

        public UserEditViewModel CreateUser()
        {
            var usrroleRepo = new UserRolesRepository(_context);
            var dzRepo = new DZRepository(_context);
            var usrs = new UserEditViewModel()
            {
                UserRoles = usrroleRepo.GetAllUserRoles(),
                UserDZs = dzRepo.GetDZs()
            };

            return usrs;
        }

        public UserEditViewModel GetUserByID(int userid)
        {
            //return _context.User.Find(UserId);

            var userroleRepo = new UserRolesRepository(_context);
            var dzRepo = new DZRepository(_context);
            var usr = _context.User.Where(x => x.UserId == userid).FirstOrDefault();
            var useredt = new UserEditViewModel()
            {
                UserId = usr.UserId,
                UserName = usr.UserName,
                Password = usr.Password,
                UserDZs = dzRepo.GetDZs(),
                DZId = usr.DZId,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Email = usr.Email,
                Mobile = usr.Mobile,
                RoleID = usr.RoleID,
                UserRoles = userroleRepo.GetAllUserRoles()
                //ODZCaseReference = odzc.ODZCaseReference,
                //IncidentTypeID = odzc.IncidentTypeID.ToString(),
                //IncidentTypes = incRepo.GetIncidentTypes(),
                //SelectedCountryofIncidentID = odzc.CountryofIncidentID,
                //DZS = dzRepo.GetDZs(),
                //CaseCoverageAmount = odzc.CaseCoverageAmount,
                //AssistedPerson = odzc.AssistedPerson,
                //CaseDescription = odzc.CaseDescription
            };
            //return _context.ODZCase.Where(x => x.ODZCaseID == odzcID).FirstOrDefault();
            return useredt;
        }

        public UserDisplayViewModel GetUserByUserName(string UserName)
        {            
            using (_context)
            {
                User usr = new User();
                usr = _context.User.AsNoTracking()
                    .Include(x => x.DZ)
                    .Where(y => y.UserName == UserName)
                    .FirstOrDefault();
                var usrdisp = new UserDisplayViewModel
                {
                    UserName = usr.UserName,
                    DZName = usr.DZ.DZName,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName
                };

                return usrdisp;
            }            
        }
        

        public void InsertUser(User user)
        {
            _context.User.Add(user);
            Save();
        }

        public void Delete(Object id)
        {
            User usr = new User();
            usr = _context.User.Find(id);
            _context.User.Remove(usr);
            Save();
        }

        public void UpdateUser(User user)
        {            
            _context.Entry(user).State = EntityState.Modified;
            Save();
        }
    }


}
