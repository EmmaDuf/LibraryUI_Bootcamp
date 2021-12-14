using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibraryCommon;
using ConsoleUI;
using System.Collections.Generic;
using ClassLibraryDatabase;
using System.Configuration;
using System.Text.RegularExpressions;

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
        public void AddAuthor_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            Author author_test = new Author("Charlotte", "Brontë");
            //act
            int result_authorid = ado.CreateAuthorToDb(author_test);
            author_test.AuthorID = result_authorid;
            int expected_roleid = author_test.AuthorID;
            ado.DeleteAuthorInDb(author_test);
            //assert
            Assert.AreEqual(result_authorid, expected_roleid);
        }
        [TestMethod]
        public void AddBook_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            Author author_test = new Author("Charlotte", "Brontë");
            int result_authorid = ado.CreateAuthorToDb(author_test);
            author_test.AuthorID = result_authorid;
            DateTime published_date = new DateTime(1847, 10, 16);
            Book book_test = new Book(author_test.AuthorID, "Jane Eyre", "Fiction", published_date);
            //act
            int result_bookid = ado.CreateBookToDb(book_test,author_test);
            book_test.BookID = result_bookid;
            int expected_bookid = book_test.BookID;
            ado.DeleteAuthorInDb(author_test);
            ado.DeleteBookInDb(book_test);
            //assert
            Assert.AreEqual(result_bookid, expected_bookid);
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
        public void GetAuthor_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            Author author_test = new Author("Charlotte", "Brontë");
            int result_authorid = ado.CreateAuthorToDb(author_test);
            author_test.AuthorID = result_authorid;
            Author author_test2 = new Author("F. Scott", "Fitzgerald");
            int result_authorid2 = ado.CreateAuthorToDb(author_test2);
            author_test2.AuthorID = result_authorid2;
            //act
            List<Author> authors = ado.GetAuthorsFromDb();
            bool passed = false;
            bool check1 = authors.Exists(author => author.AuthorID == author_test.AuthorID);
            bool check2 = authors.Exists(author => author.AuthorID == author_test2.AuthorID);
            if (authors.Exists(author => author.AuthorID == author_test.AuthorID) & authors.Exists(author => author.AuthorID == author_test2.AuthorID))
            {
                passed = true;
            }
            //assert
            ado.DeleteAuthorInDb(author_test);
            ado.DeleteAuthorInDb(author_test2);
            Assert.IsTrue(passed);

        }
        [TestMethod]
        public void GetBook_Db_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            //--authors
            Author author_test = new Author("Charlotte", "Brontë");
            int result_authorid = ado.CreateAuthorToDb(author_test);
            author_test.AuthorID = result_authorid;
            Author author_test2 = new Author("F. Scott", "Fitzgerald");
            int result_authorid2 = ado.CreateAuthorToDb(author_test2);
            author_test2.AuthorID = result_authorid2;
            //--books
            DateTime published_date = new DateTime(1847, 10, 16);
            Book first_book = new Book(author_test.AuthorID, "Jane Eyre", "Fiction", published_date);
            int result_bookid = ado.CreateBookToDb(first_book, author_test);
            first_book.BookID = result_bookid;
            DateTime published_date2 = new DateTime(1925, 4, 10);
            Book second_book = new Book(author_test2.AuthorID, "The Great Gatsby", "Fiction", published_date);
            result_bookid = ado.CreateBookToDb(second_book, author_test2);
            second_book.BookID = result_bookid;
            //act
            List<Book> books = ado.GetBooksFromDb();
            bool passed = false;
            if (books.Exists(book => book.BookID == first_book.BookID) & books.Exists(book => book.BookID == second_book.BookID))
            {
                passed = true;
            }
            //assert
            ado.DeleteAuthorInDb(author_test);
            ado.DeleteAuthorInDb(author_test2);
            ado.DeleteBookInDb(first_book);
            ado.DeleteBookInDb(second_book);
            Assert.IsTrue(passed);
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
        [TestMethod]
        public void CountRolesInDb_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            //act
            int count = ado.CountRolesInDb();
            Assert.IsTrue(count >= 0);
        }
        [TestMethod]
        public void CountUsersInDb_Test()
        {
            //arrange
            string _connection = ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
            DbAdo ado = new DbAdo(_connection);
            //act
            int count = ado.CountUsersInDb();
            Assert.IsTrue(count >= 0);
        }
        [TestMethod]
        public void isNotPhoneNumber_Test()
        {
            string number_test = "763-276-5505 ";
            string phonenumber_pattern = @"^[0-9]{3}-[0-9]{3}-[0-9]{4}$";
            Regex phonenumber = new Regex(phonenumber_pattern);
            MatchCollection matchednumbers = phonenumber.Matches(number_test);
            bool results = true;
            foreach(Match match in matchednumbers)
            {
                if (match.Value.Equals(number_test))
                {
                    results = false;
                }
            }
            //assert
            Assert.IsTrue(results);
        }
        [TestMethod]
        public void isPhoneNumber_Test()
        {
            string number_test = "763-276-5555";
            string phonenumber_pattern = @"^[0-9]{3}-[0-9]{3}-[0-9]{4}$";
            Regex phonenumber = new Regex(phonenumber_pattern);
            MatchCollection matchednumbers = phonenumber.Matches(number_test);
            bool results = false;
            foreach (Match match in matchednumbers)
            {
                if (match.Value.Equals(number_test))
                {
                    results = true;
                }
            }
            //assert
            Assert.IsTrue(results);
        }
        [TestMethod]
        public void isEmail_Test(){
            string email_test = "tes23t@something.com";
            string email_pattern = @"^\w+@[A-z]{4,}\.[A-z]{3}$";
            Regex email = new Regex(email_pattern);
            MatchCollection matchedemails = email.Matches(email_test);
            bool results = false;
            foreach (Match match in matchedemails)
            {
                if (match.Value.Equals(email_test))
                {
                    results = true;
                }
            }
            //assert
            Assert.IsTrue(results);

        }
        [TestMethod]
        public void isNotEmail_Test()
        {
            string email_test = "67@gm.com";
            string email_pattern = @"^\w+@[A-z]{4,}\.[A-z]{3}$";
            Regex email= new Regex(email_pattern);
            MatchCollection matchedemails = email.Matches(email_test);
            bool results = true;
            foreach (Match match in matchedemails)
            {
                if (match.Value.Equals(email_test))
                {
                    results = false;
                }
            }
            //assert
            Assert.IsTrue(results);
        }


    }
}
