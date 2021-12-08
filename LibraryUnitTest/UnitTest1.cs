using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibraryCommon;
using ConsoleUI;
using System.Collections.Generic;
using ClassLibraryDatabase;
using System.Configuration;

namespace LibraryUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UserUserName_Test() { 
            //arrange
            string username = "username";
            string firstname = "first";
            string lastname = "last";
            string password = "password";
            int roleid = 1;
            string expected = "username";
            //act
            User user = new User(firstname, lastname, username, password, roleid);
            string result = user.UserName;
            //assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void UserFirstname_Test()
        {
            //arrange
            string username = "username";
            string firstname = "first";
            string lastname = "last";
            string password = "password";
            int roleid = 1;
            string expected = "first";
            //act
            User user = new User(firstname, lastname, username, password, roleid);
            string result = user.FirstName;
            //assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void UserLastname_Test()
        {
            //arrange
            string username = "username";
            string firstname = "first";
            string lastname = "last";
            string password = "password";
            int roleid = 1;
            string expected = "last";
            //act
            User user = new User(firstname, lastname, username, password, roleid);
            string result = user.LastName;
            //assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void UserPassword_Test()
        {
            //arrange
            string username = "username";
            string firstname = "first";
            string lastname = "last";
            string password = "password";
            int roleid = 1;
            string expected = "password";
            //act
            User user = new User(firstname, lastname, username, password, roleid);
            string result = user.Password;
            //assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void UserRoleID_Test()
        {
            //arrange
            string username = "username";
            string firstname = "first";
            string lastname = "last";
            string password = "password";
            int roleid = 1;
            int expected = 1;
            //act
            User user = new User(firstname, lastname, username, password, roleid);
            int result = user.RoleID_FK;
            //assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void AddUser_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            //act
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            int expected_userid = user_test.UserID;
            ado.DeleteUserInDb(user_test);
            //assert
            Assert.AreEqual(result_userid, expected_userid);
        }
        [TestMethod]
        public void AddRole_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            Role role_test = new Role("test_role");
            //act
            int result_roleid = ado.CreateRoleToDb(role_test);
            role_test.RoleID = result_roleid;
            int expected_roleid = role_test.RoleID;
            ado.DeleteRoleInDb(role_test);
            //assert
            Assert.AreEqual(result_roleid, expected_roleid);
        }
        [TestMethod]
        public void DeleteUser_Db_Test()
        {
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            bool was_created = false;
            List<User> user_list = ado.GetUsersFromDb();
            if (user_list.Exists(user => user.UserID == user_test.UserID))
            {
                was_created = true;
            }
            //act
            ado.DeleteUserInDb(user_test);
            user_list = ado.GetUsersFromDb();
            bool was_deleted = false;
            if (!user_list.Exists(user => user.UserID == user_test.UserID) & was_created == true)
            {
                was_deleted = true;
            }
            //assert
            Assert.IsTrue(was_deleted);
        }
        [TestMethod]
        public void DeleteRole_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            Role role_test = new Role("test_role");
            int result_roleid = ado.CreateRoleToDb(role_test);
            role_test.RoleID = result_roleid;
            bool was_created = false;
            List<Role> role_list = ado.GetRolesFromDb();
            if (role_list.Exists(role => role.RoleID == role_test.RoleID))
            {
                was_created = true;
            }
            //act
            ado.DeleteRoleInDb(role_test);
            role_list = ado.GetRolesFromDb();
            bool was_deleted = false;
            if (!role_list.Exists(role => role.RoleID == role_test.RoleID) & was_created == true)
            {
                was_deleted = true;
            }
            //assert
            Assert.IsTrue(was_deleted);
        }
        [TestMethod]
        public void UpdateRoleName_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            Role role_test = new Role("test_role");
            int result_roleid = ado.CreateRoleToDb(role_test);
            role_test.RoleID = result_roleid;
            //act
            string expected = "new_role";
            ado.UpdateRoleNameInDb(role_test, expected);
            //assert
            List<Role> roles = ado.GetRolesFromDb();
            Role new_rolename = roles.Find(role => role.RoleID == role_test.RoleID);
            string result = new_rolename.RoleName;
            ado.DeleteRoleInDb(new_rolename);
            Assert.IsTrue(result == expected);
        }
        [TestMethod]
        public void UpdateUserName_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            //act
            string expected = "new_username";
            ado.UpdateUserNameInDb(user_test, expected);
            //assert
            List<User> users = ado.GetUsersFromDb();
            User new_username = users.Find(user => user.UserID == user_test.UserID);
            string result = new_username.UserName;
            ado.DeleteUserInDb(new_username);
            Assert.IsTrue(result == expected);
        }
        [TestMethod]
        public void UpdateFirstName_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            //act
            string expected = "new_firstname";
            ado.UpdateUserFirstNameInDb(user_test, expected);
            //assert
            List<User> users = ado.GetUsersFromDb();
            User new_firstname = users.Find(user => user.UserID == user_test.UserID);
            string result = new_firstname.FirstName;
            ado.DeleteUserInDb(new_firstname);
            Assert.IsTrue(result == expected);
        }
        [TestMethod]
        public void UpdateLastName_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            //act
            string expected = "new_lastname";
            ado.UpdateUserLastNameInDb(user_test, expected);
            //assert
            List<User> users = ado.GetUsersFromDb();
            User new_lastname = users.Find(user => user.UserID == user_test.UserID);
            string result = new_lastname.LastName;
            ado.DeleteUserInDb(new_lastname);
            Assert.IsTrue(result == expected);
        }
        [TestMethod]
        public void UpdatePassword_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            //act
            string expected = "new_password";
            ado.UpdateUserPasswordInDb(user_test, expected);
            //assert
            List<User> users = ado.GetUsersFromDb();
            User new_password = users.Find(user => user.UserID == user_test.UserID);
            string result = new_password.Password;
            ado.DeleteUserInDb(new_password);
            Assert.IsTrue(result == expected);
        }
        [TestMethod]
        public void GetRoleName_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            //get a roleID
            Role role_test = new Role("test_rolename");
            //act
            int result_roleid = ado.CreateRoleToDb(role_test);
            role_test.RoleID = result_roleid;
            //give that roleID to a user
            
            User user_test = new User("Mary", "Christmas", "marychristmas01", "password", role_test.RoleID);
            int result_userid = ado.CreateUserToDb(user_test);
            user_test.UserID = result_userid;
            //act
            string result = ado.GetRoleNameofUserFromDb(user_test);
            ado.DeleteUserInDb(user_test);
            //assert
            Assert.AreEqual(role_test.RoleName, result);
        }
        [TestMethod]
        public void ExceptionLogging_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            //act
            InvalidOperationException ex = new InvalidOperationException();
            bool executed = ado.LogException(ex);
            //assert
            Assert.IsTrue(executed);
        }

    }
}
