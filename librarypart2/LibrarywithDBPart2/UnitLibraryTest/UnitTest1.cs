using Microsoft.VisualStudio.TestTools.UnitTesting;
using LbAssgLibrary;
using System.Collections.Generic;
using System;
using System.IO;

namespace UnitLibraryTest
{
    [TestClass]
    public class UnitTestLibrary
    {

        public DTORole _role;
        public DTOUser _user;
        public UnitTestLibrary()
        {
            _role= new DTORole();
            _user= new DTOUser();
        }
        [TestMethod]
        public void roleAdd()
        {//Arrange
            List<DTORole> _rolelist = new List<DTORole>();
            //Act
            _role.AddRole(_rolelist, "Default");
            //Assert
           // Console.WriteLine(_rolelist.Count);
            Assert.AreEqual(4, _rolelist.Count);
                     
        }
        [TestMethod]
        public void loginsuccesful()
        {
            List<DTOUser> _userlist = new List<DTOUser>();
            _userlist.Add(new DTOUser
            {
                firstname = "Misha",
                lastname = "Nishanth",
                emailid = "mmisha.nishanth@gmail.com",
                username = "mishamm",
                password = "Misha123##",
                role = "Patron",
                phoneno = "6176394898"
            });
            // Call
            bool login_status = false;
            login_status = _user.authenticate(_userlist, "mishamm", "Misha123##");
            //Assert
            Assert.AreEqual(true,login_status);
          
        }
        [TestMethod]
        public void loginUnsuccessful()
        {
            List<DTOUser> _userlist = new List<DTOUser>();
            _userlist.Add(new DTOUser
            {
                firstname = "Misha",
                lastname = "Nishanth",
                emailid = "mmisha.nishanth@gmail.com",
                username = "mishamm",
                password = "Misha123##",
                role = "Patron",
                phoneno = "617639486"
            });
            // Call
            bool login_status = true;
            login_status = _user.authenticate(_userlist, "mishamm", "Misha123");
            //Assert
            Assert.AreEqual(false, login_status);

        }
        [TestMethod]
        public void PrintUserName()
        {
            List<DTOUser> _userlist = new List<DTOUser>();
            _userlist.Add(new DTOUser
            {
                firstname = "Misha",
                lastname = "Nishanth",
                emailid = "mmisha.nishanth@gmail.com",
                username = "mishamm",
                password = "Misha123##",
                role = "Patron",
                phoneno = "23492342"
            });
            string username;
            username = _user.PrintUser(_userlist, "PUT");
            Assert.AreEqual("mishamm", username);

        }
        [TestMethod]
        public void PrintUserProfile()
        {
            List<DTOUser> _userlist = new List<DTOUser>();
            _userlist.Add(new DTOUser
            {
                firstname = "Misha",
                lastname = "Nishanth",
                emailid = "mmisha.nishanth@gmail.com",
                username = "mishamm",
                password = "Misha123##",
                role = "Patron",
                phoneno = "623432"
            });
            string userprof;
            userprof = _user.PrintUser(_userlist, "PPT");
            Assert.AreEqual("mishammMishaNishanthmmisha.nishanth@gmail.com3214073443Patron", userprof);

        }
        [TestMethod]
        public void _logout()
        {
            List<DTOUser> _userlist = new List<DTOUser>();
            _userlist.Add(new DTOUser
            {
                firstname = "Misha",
                lastname = "Nishanth",
                emailid = "mmisha.nishanth@gmail.com",
                username = "mishamm",
                password = "Misha123##",
                role = "Patron",
                phoneno = "39432342"
             });
            char logout = _user.logout(_userlist, "mishamm");
            Assert.AreEqual('O', logout);
        }
        
    }
}
