using CMSSample.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSSample.DA.Repository
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int UserId);
        IEnumerable<User> GetUserByUserName(string UserName);
        void InsertUser(User user);
        void Delete(Object userID);
        void UpdateUser(User user);
        void Save();
    }
}
