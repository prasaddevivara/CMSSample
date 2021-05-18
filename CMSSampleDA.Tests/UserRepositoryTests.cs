using System;
using System.Collections.Generic;
using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CMSSampleDA.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {

        [TestMethod]
        public void Test_GetMockAllUsers()
        {
            List<User> usrs = new List<User>() 
            { 
                new User{UserId=1, UserName="DVedanth1", Password="Test123$", FirstName="Devi", LastName="Prasad", Email="DVedanth1@gmail.com", Mobile="1234567890" },
                new User{UserId=2, UserName="GRam", Password="mock123$", FirstName="Ram", LastName="Grand", Email="GRam@gmail.com", Mobile="1234567890" }
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(slf => slf.GetUsers()).Returns(usrs);

            Assert.IsNotNull(usrs);
        }

        [TestMethod]
        public void Test_GetMockUserByID()
        {           
            var mock = new Mock<IUserRepository>();
            User usr = new User()
            { UserId = 1, UserName = "DVedanth1", Password = "Test123$", FirstName = "Devi", LastName = "Prasad", Email = "DVedanth1@gmail.com", Mobile = "1234567890" };
            
            int userID = 1;
            mock.Setup(slf => slf.GetUserByID(userID)).Returns(usr);

            Assert.IsNotNull(usr);
        }

        [TestMethod]
        public void Test_MockInsertUser()
        {
            var mock = new Mock<IUserRepository>();
            User user = new User()
            {
                UserName = "VRamana",
                Password = "mypassword",
                FirstName = "Ramana",
                LastName = "Vedantham",
                Email = "vramana@gmail.com",
                Mobile = "1234567890"
            };
            mock.Setup(slf => slf.InsertUser(user));            
        }

        [TestMethod]
        public void Test_MockDeleteUser()
        {
            var mock = new Mock<IUserRepository>();
            User user = new User()
            {
                UserId = 4,
                UserName = "VRamana",
                Password = "mypassword",
                FirstName = "Ramana",
                LastName = "Vedantham",
                Email = "vramana@gmail.com",
                Mobile = "1234567890"
            };
            mock.Setup(slf => slf.DeleteUser(user));           
        }

        [TestMethod]
        public void Test_MockUpdateUser()
        {
            var mock = new Mock<IUserRepository>();
            User user = new User()
            {
                UserId = 1,
                UserName = "DVedanth1",
                Password = "Test123$",
                FirstName = "Devi",
                LastName = "Prasad",
                Email = "DVedanth1@gmail.com",
                Mobile = "1234567890"
            };
            mock.Setup(slf => slf.DeleteUser(user));
        }

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
                Mobile= "1234567890"
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
                Mobile = "1234567890"
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
                Mobile = "4321567890"
            };
            userrepository.UpdateUser(user);
        }
    }
}
