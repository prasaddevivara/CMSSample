using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
            return _context.Users.ToList();
        }

        public User GetUserByID(int UserId)
        {
            return _context.Users.Find(UserId);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
            Save();
        }

        public void DeleteUser(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
           // _context.Users.Remove(user);
            Save();
        }

        public void UpdateUser(User user)
        {            
            _context.Entry(user).State = EntityState.Modified;
            Save();
        }
    }


}
