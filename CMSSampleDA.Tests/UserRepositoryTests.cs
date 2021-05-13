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

        [TestMethod]
        public void Test_GetUserByID()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            int userID = 1;
            User user = new User();

            user = userrepository.GetUserByID(userID);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void Test_InsertUser()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            User user = new User()
            {
                UserName="VRamana",
                Password="mypassword",
                FirstName="Ramana",
                LastName="Vedantham",
                Email="vramana@gmail.com",
                Mobile=1234
            }; 
            userrepository.InsertUser(user);           
        }

        [TestMethod]
        public void Test_DeleteUser()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            User user = new User()
            {
                UserId=4,
                UserName = "VRamana",
                Password = "mypassword",
                FirstName = "Ramana",
                LastName = "Vedantham",
                Email = "vramana@gmail.com",
                Mobile = 1234
            };
            userrepository.DeleteUser(user);
        }

        [TestMethod]
        public void Test_UpdateUser()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            User user = new User()
            {
                UserId=1,
                UserName = "DVedanth1",
                Password = "Test123$",
                FirstName = "Devi",
                LastName = "Prasad",
                Email = "DVedanth1@gmail.com",
                Mobile = 4321
            };
            userrepository.UpdateUser(user);
        }
    }
}
