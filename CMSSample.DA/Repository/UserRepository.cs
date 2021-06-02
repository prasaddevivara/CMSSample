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

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _context.User.ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public User GetUserByID(int UserId)
        {
            return _context.User.Find(UserId);
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
