using System;
using System.Collections.Generic;
using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMSSampleDA.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void Test_GetAllUsers()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            IEnumerable<User> lstUsers;

            lstUsers = userrepository.GetUsers();

            Assert.IsNotNull(lstUsers);
        }

        public void Test_GetUserByID()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            int userID = 1;
            User user = new User();

            user = userrepository.GetUserByID(userID);

            Assert.IsNotNull(user);
        }
    }
}
