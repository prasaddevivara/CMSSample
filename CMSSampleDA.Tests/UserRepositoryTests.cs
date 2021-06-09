using System;
using System.Collections.Generic;
using CMSSample.DA;
using CMSSample.DA.Repository;
using CMSSample.DomainModel;
using CMSSample.DomainModel.ViewModels;
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
            List<UserDisplayViewModel> usrs = new List<UserDisplayViewModel>() 
            { 
                new UserDisplayViewModel{UserId=1, UserName="DVedanth1", Password="Test123$", FirstName="Devi", LastName="Prasad", Email="DVedanth1@gmail.com", Mobile="1234567890",DZName="India", RoleName="Admin" },
                new UserDisplayViewModel{UserId=2, UserName="GRam", Password="mock123$", FirstName="Ram", LastName="Grand", Email="GRam@gmail.com", Mobile="1234567890", DZName="France", RoleName="User" }
            };
            var mock = new Mock<IUserRepository>();
            mock.Setup(slf => slf.GetUsers()).Returns(usrs);

            Assert.IsNotNull(usrs);
        }

        [TestMethod]
        public void Test_GetMockUserByID()
        {           
            var mock = new Mock<IUserRepository>();
            UserEditViewModel usr = new UserEditViewModel()
            { UserId = 1, UserName = "DVedanth1", Password = "Test123$", FirstName = "Devi", LastName = "Prasad", Email = "DVedanth1@gmail.com", Mobile = "1234567890", DZId = 1, RoleID = 1 };
            
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
            mock.Setup(slf => slf.Delete(4));           
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
            mock.Setup(slf => slf.UpdateUser(user));
        }

        [TestMethod]
        public void Test_GetAllUsers()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            IEnumerable<UserDisplayViewModel> lstUsers;

            lstUsers = userrepository.GetUsers();

            Assert.IsNotNull(lstUsers);
        }

        [TestMethod]
        public void Test_GetUserByID()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            int userID = 2;
            UserEditViewModel user = new UserEditViewModel();

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

            //User user = new User()
            //{
            //    UserId=4,
            //    UserName = "VRamana",
            //    Password = "mypassword",
            //    FirstName = "Ramana",
            //    LastName = "Vedantham",
            //    Email = "vramana@gmail.com",
            //    Mobile = "1234567890"
            //};
            userrepository.Delete(4);
        }

        [TestMethod]
        public void Test_UpdateUser()
        {
            CMSSampleDAContext cmssampledacontext = new CMSSampleDAContext();
            IUserRepository userrepository = new UserRepository(cmssampledacontext);

            User user = new User()
            {
                UserId=5,
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
