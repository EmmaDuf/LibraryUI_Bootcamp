using ClassLibraryCommon;
using System;
using System.Collections.Generic;
using ClassLibraryDatabase;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ConsoleUI
{
    public class Program
    {
        public static List<Role> all_roles = new List<Role>();
        public static User active_user = new User();
        static string _connection =ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
        static DbAdo ado = new DbAdo(_connection);
        public static List<Role> roles_in_db = ado.GetRolesFromDb();
        public static List<User> users_in_db = ado.GetUsersFromDb();
        static void Main(string[] args)
        {
            //List<Role> roles_in_db = ado.GetRoleFromDb();
            //Create Users in DB
            //ado.CreateUserFromDb(new User("Mary", "Christmas", "marychristmas01", "password", 3));
            //User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            //ado.DeleteUserInDb(user_test);

            EntryText();
        }
        static void EntryText()
        {
            Console.WriteLine("Welcome to the Library Console User Interface!");
            
            int user_input = 0;
            while(user_input != 16)
            {
                Console.WriteLine("Current user logged in: {0}", active_user.UserName);
                Console.WriteLine("Here are the numbered options to interact with this console:");
                Console.WriteLine("1) Create Role");
                Console.WriteLine("2) Create User");
                Console.WriteLine("3) Create Author");
                Console.WriteLine("4) Create Book");
                Console.WriteLine("5) Login");
                Console.WriteLine("6) Logout");
                Console.WriteLine("7) Delete User");
                Console.WriteLine("8) Delete Role");
                Console.WriteLine("9) Delete Author");
                Console.WriteLine("10) Delete Book");
                Console.WriteLine("11) Print User Profile");
                Console.WriteLine("12) Print Users");
                Console.WriteLine("13) Print Roles");
                Console.WriteLine("14) Print Authors");
                Console.WriteLine("15) Print Books");
                Console.WriteLine("16) Exit User Interface");
                user_input = Int32.Parse(Console.ReadLine());
                switch (user_input)
                {
                    case 1:
                        Console.WriteLine("Please enter a name for the new role:");
                        string rolename = Console.ReadLine();
                        CreateRole(rolename);
                        break;
                    case 2:
                        #region CreateUser
                        Console.WriteLine("Select a username for your new user account:");
                        string new_username = Console.ReadLine();
                        while (UserExists(new_username))
                        {
                            Console.WriteLine("I'm sorry, that username already exists, please try something else:");
                            new_username = Console.ReadLine();

                        }
                        Console.WriteLine("What is your first name?");
                        string firstname = Console.ReadLine();
                        Console.WriteLine("What is your last name?");
                        string lastname = Console.ReadLine();
                        Console.WriteLine("Please enter the password for this account:");
                        string password = Console.ReadLine();
                        Console.WriteLine("What is the role you want assigned to this account?:");
                        PrintRoles();
                        int roleid = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Would you like to enter a phone number for this account? (yes or no)");
                        string phonenumber = null;
                        string user_answer = Console.ReadLine();
                        if (user_answer.Trim().ToLower().Contains("y"))
                        {
                            bool get_phonenumber = false;
                            while (!get_phonenumber)
                            {
                                Console.WriteLine("Please enter a phone number in the form ###-###-####:");
                                string phone_num = Console.ReadLine();
                                if (ValidPhoneNumber(phone_num))
                                {
                                    get_phonenumber = true;
                                    phonenumber = phone_num;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter the valid syntax ");
                                    Console.WriteLine("If you would like to quit entering a phone number, input quit");
                                    string quit_number = Console.ReadLine();
                                    if (quit_number.Trim().ToLower().Contains("q"))
                                    {
                                        get_phonenumber = true;
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Would you like to enter an email for this account? (yes or no)");
                        string email= null;
                        user_answer = Console.ReadLine();
                        if (user_answer.Trim().ToLower().Contains("y"))
                        {
                            bool get_email = false;
                            while (!get_email)
                            {
                                Console.WriteLine("Please enter an email address:");
                                string email_try = Console.ReadLine();
                                if (ValidEmail(email_try))
                                {
                                    get_email = true;
                                    email = email_try;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter the valid syntax ");
                                    Console.WriteLine("If you would like to quit entering an email, input quit");
                                    string quit_number = Console.ReadLine();
                                    if (quit_number.Trim().ToLower().Contains("q"))
                                    {
                                        get_email = true;
                                    }
                                }
                            }
                        }
                        if(CreateUser(firstname, lastname, new_username, password, roleid, email, phonenumber))
                        {
                            Console.WriteLine("User with username {0} was created", new_username);
                        }
                        else
                        {
                            Console.WriteLine("For some reason, I did not catch that the username {0} already existed",new_username);
                            Console.WriteLine("Username {0} was then not created. Please try again with a different user name");
                        }
                        #endregion
                        break;
                    case 3:
                        #region CreateAuthor
                        Console.WriteLine("Please enter the first name of the author:");
                        string first_name = Console.ReadLine();
                        Console.WriteLine("Please enter the last name of the author:");
                        string last_name = Console.ReadLine();
                        string comment = null;
                        Console.WriteLine("Would you like to enter any comments about this author? (yes or no)");
                        if (Console.ReadLine().Trim().ToLower().Contains("y"))
                        {
                            Console.WriteLine("Please enter your comment for this author:");
                            comment = Console.ReadLine();
                        }
                        CreateAuthor(first_name,last_name,comment);
                        #endregion
                        break;
                    case 4:
                        #region CreateBook
                        Console.WriteLine("Please enter the Title of the book:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Please enter the Genre of the book:");
                        string genre = Console.ReadLine();
                        Console.WriteLine("Please enter the author's first name of the book:");
                        string auth_first_name = Console.ReadLine();
                        Console.WriteLine("Please enter the author's last name of the book:");
                        string auth_last_name = Console.ReadLine();
                        List<Author> author_list = ado.GetAuthorsFromDb();
                        Author a = author_list.Find(author => author.FirstName == auth_first_name & author.LastName == auth_last_name);
                        if (a == null)
                        {
                            Console.WriteLine("This author does not exist in the database. Would you like to create this author? (yes or no)");
                            string answer = Console.ReadLine();
                            if (answer.Trim().ToLower().Contains("y"))
                            {
                                Console.WriteLine("Please enter the first name of the author:");
                                first_name = Console.ReadLine();
                                Console.WriteLine("Please enter the last name of the author:");
                                last_name = Console.ReadLine();
                                comment = null;
                                Console.WriteLine("Would you like to enter any comments about this author? (yes or no)");
                                if (Console.ReadLine().Trim().ToLower().Contains("y"))
                                {
                                    Console.WriteLine("Please enter your comment for this author:");
                                    comment = Console.ReadLine();
                                }
                                CreateAuthor(first_name, last_name, comment);
                                Console.WriteLine("You may now try to create a book with the author {0} {1}",first_name,last_name);
                            }
                            break;
                        }
                        DateTime published_date = DateTime.Now;
                        if (a != null)
                        {
                            Console.WriteLine("Please enter the date (MM/DD/YYYY) the book was published:");
                            if(DateTime.TryParse(Console.ReadLine(), out DateTime _date))
                            {
                                Console.WriteLine("That was a valid date");
                                published_date = _date;
                            }
                        }
                        CreateBook(title,genre,a,published_date);
                        #endregion
                        break;
                    case 5:
                        Login();
                        break;
                    case 6:
                        Logout();
                        break;
                    case 7:
                        #region Delete User
                        bool task_completed = false;
                        while (!task_completed)
                        {
                            string active_username = active_user.UserName;
                            //if someone is logged in (not guest login)
                            if (UserExists(active_username) & !task_completed)
                            {
                                Console.WriteLine("Please re-enter the password of {0} to confirm deletion of profile",active_user.UserName);
                                string entered_password = Console.ReadLine();
                                if (entered_password.Equals(active_user.Password))
                                {
                                    DeleteUser(active_username);
                                    task_completed = true;
                                }
                                else
                                {
                                    Console.WriteLine("Password entered is incorrect. Could not delete profile");
                                    Console.WriteLine("Would you like to try entering the password again? (yes or no)");
                                    string user_response = Console.ReadLine();
                                    if (user_response.Trim().ToLower().Contains("n")){
                                        task_completed = true;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("You first need to login to the profile you want to delete");
                                Console.WriteLine("Woould you like to login? (yes or no)");
                                string answer = Console.ReadLine();
                                if (answer.Trim().ToLower().Contains("y"))
                                {
                                    Login();
                                }
                                else if (answer.Trim().ToLower().Contains("n"))
                                {
                                    task_completed = true;
                                }
                                else
                                {
                                    task_completed = true;
                                }
                            }

                        }
                        #endregion
                        break;
                    case 8:
                        PrintRoles();
                        Console.WriteLine("Please enter the role_id you would like to delete (enter a letter to quit):");
                        int role_id;
                        if (Int32.TryParse(Console.ReadLine(),out role_id))
                        {
                            DeleteRole(role_id);

                        }
                        break;
                    case 9:
                        PrintAuthors();
                        Console.WriteLine("Please enter the author_id you would like to delete (enter a letter to quit):");
                        int authorid;
                        if (Int32.TryParse(Console.ReadLine(), out authorid))
                        {
                            DeleteAuthor(authorid);

                        }
                        break;
                    case 10:
                        PrintBooks();
                        Console.WriteLine("Please enter the book_id you would like to delete (enter a letter to quit):");
                        int bookid;
                        if (Int32.TryParse(Console.ReadLine(), out bookid))
                        {
                            DeleteBook(bookid);

                        }
                        break;
                    case 11:
                        PrintProfile();
                        break;
                    case 12:
                        PrintUsers();
                        break;
                    case 13:
                        PrintRoles();
                        break;
                    case 14:
                        PrintAuthors();
                        break;
                    case 15:
                        PrintBooks();
                        break;
                    default:
                        break;
                }

            }
           
        }
        public static bool UserExists(string username)
        {
            List<User> active_users = ado.GetUsersFromDb();
            if (active_users.Exists(user => user.UserName == username))
            {
                return true;
            }
            return false;
        }
        public static bool RoleExists(int roleid)
        {
            roles_in_db = ado.GetRolesFromDb();
            if (roles_in_db.Exists(role => role.RoleID == roleid))
            {
                return true;
            }
            return false;
        }
        public static bool AuthorExists(int authorid)
        {
            List<Author> authors_in_db = ado.GetAuthorsFromDb();
            if (authors_in_db.Exists(author => author.AuthorID == authorid))
            {
                return true;
            }
            return false;
        }
        public static bool BookExists(int bookid)
        {
            List<Book> books_in_db = ado.GetBooksFromDb();
            if (books_in_db.Exists(book => book.BookID == bookid))
            {
                return true;
            }
            return false;
        }
        //Usernames are checked for availability before password is prompted for...
        public static bool CreateUser(string firstname, string lastname, string username, string password, int roleid, string Email = null,string PhoneNumber = null)
        {
            if (!UserExists(username))
            {
                roles_in_db = ado.GetRolesFromDb();
                if (!roles_in_db.Exists(role => role.RoleID == roleid))
                {
                    Console.WriteLine("The role ID you entered does not exist");
                    Console.WriteLine("If you would like to create a new role ID, enter yes. If you would like to enter a different role ID, enter different role");
                    string user_answer = Console.ReadLine();
                    if(user_answer.Trim().ToLower().Contains("y")){
                        Console.WriteLine("Please enter a role name:");
                        string new_rolename = Console.ReadLine();
                        roleid = CreateRole(new_rolename);
                    }
                }
                User new_user = new User(firstname, lastname, username, password, roleid);
                ado.CreateUserToDb(new_user);
                Console.WriteLine("Username {0} created",username);
                return true;
            }
            else
            {
                Console.WriteLine("That username already exists");
                return false;
            }
        }
        public static bool ValidPhoneNumber(string phone_num)
        {
            string phonenumber_pattern = @"^[0-9]{3}-[0-9]{3}-[0-9]{4}$";
            Regex phonenumber = new Regex(phonenumber_pattern);
            MatchCollection matchednumbers = phonenumber.Matches(phone_num);
            bool results = false;
            foreach (Match match in matchednumbers)
            {
                if (match.Value.Equals(phone_num))
                {
                    results = true;
                }
            }
            return results;
        }
        public static bool ValidEmail(string email)
        {
            string email_pattern = @"^\w+@[A-z]{4,}\.[A-z]{3}$";
            Regex phonenumber = new Regex(email_pattern);
            MatchCollection matchednumbers = phonenumber.Matches(email);
            bool results = false;
            foreach (Match match in matchednumbers)
            {
                if (match.Value.Equals(email))
                {
                    results = true;
                }
            }
            return results;
        }
        public static bool DeleteUser(string username)
        {
            if (UserExists(username)){
                List<User> active_users = ado.GetUsersFromDb();
                active_user = new User();
                ado.DeleteUserInDb(active_users.Find(user => user.UserName == username));
                Console.WriteLine("User {0} has been deleted",username);
                return true;
            }
            else
            {
                Console.WriteLine("User does not exist ");
                return false;
            }
        }
        public static int CreateRole(string rolename)
        {
            Role new_role = new Role();
            new_role.RoleName = rolename;
            int roleid = ado.CreateRoleToDb(new_role);
            new_role.RoleID = roleid;
            return roleid;
        }
        public static bool CreateRole(int roleid, string rolename)
        {
            if (!RoleExists(roleid))
            {
                Role new_role = new Role(roleid,rolename);
                all_roles.Add(new_role);
                ado.CreateRoleToDb(new_role);
                Console.WriteLine("New role has been added");
                return true;
            }
            else
            {
                Console.WriteLine("That role already exists");
                return false;
            }
        }
        public static int CreateAuthor(string firstname, string lastname, string comment = null)
        {

            Author a = new Author(firstname,  lastname, comment);
            int authorid= ado.CreateAuthorToDb(a);
            a.AuthorID = authorid;
            return authorid;
        }
        public static int CreateBook(string title, string genre, Author a, DateTime published_date)
        {
            //TODO
            //List<Author> authors = ado.GetAuthorsFromDb();
            //Author book_author= authors.Find(a.FirstName);
            Book b = new Book(a.AuthorID, title, genre, published_date);
            int bookid = ado.CreateBookToDb(b,a);
            b.BookID = bookid;
            return bookid;

        }
        public static bool DeleteRole(int roleid)
        {
            if (RoleExists(roleid))
            {
                roles_in_db = ado.GetRolesFromDb();
                Role delete_me = roles_in_db.Find(role => role.RoleID == roleid);
                all_roles.Remove(delete_me);
                ado.DeleteRoleInDb(delete_me);
                Console.WriteLine("Role has been deleted");
                return true;
            }
            else
            {
                Console.WriteLine("That role does not exist to delete");
                return false;
            }
        }
        public static bool DeleteAuthor(int authorid)
        {
            if (AuthorExists(authorid))
            {
                List<Author> authors_in_db = ado.GetAuthorsFromDb();
                Author delete_me = authors_in_db.Find(author => author.AuthorID == authorid);
                ado.DeleteAuthorInDb(delete_me);
                Console.WriteLine("Author has been deleted");
                return true;
            }
            else
            {
                Console.WriteLine("That authorID does not exist to delete");
                return false;
            }
        }
        public static bool DeleteBook(int bookid)
        {
            if (BookExists(bookid))
            {
                List<Book> books_in_db = ado.GetBooksFromDb();
                Book delete_me = books_in_db.Find(book => book.BookID == bookid);
                ado.DeleteBookInDb(delete_me);
                Console.WriteLine("Book has been deleted");
                return true;
            }
            else
            {
                Console.WriteLine("That bookID does not exist to delete");
                return false;
            }
        }
        public static void UpdatePassword(User user,string new_password)
        {
            user.Password = new_password;
            ado.UpdateUserPasswordInDb(user, new_password);
        }
        public static void Login()
        {
            List<User> active_users = ado.GetUsersFromDb();
            bool task_completed = false;
            while (!task_completed)
            {
                Console.WriteLine("Please enter your username:");
                string entered_username = Console.ReadLine();
                if (UserExists(entered_username))
                {
                    User identify_user = active_users.Find(user_name => user_name.UserName == entered_username);
                    Console.WriteLine("Please enter the password for {0}",identify_user.UserName);
                    string entered_password = Console.ReadLine();
                    if (entered_password.Equals(identify_user.Password))
                    {
                        active_user = identify_user;
                        task_completed = true;
                    }
                    else
                    {
                        Console.WriteLine("Password was incorrect");
                        Console.WriteLine("Would you like to try again? (yes or no)");
                        string user_answer = Console.ReadLine();
                        if (user_answer.Trim().ToLower().Contains("n"))
                        {
                            task_completed = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Username does not exist, would you like to try again? (yes or no)");
                    string user_answer = Console.ReadLine();
                    if (user_answer.Trim().ToLower().Contains("n"))
                    {
                        task_completed = true;
                    }
                }
            }
            

        }
        public static User Logout()
        {
            active_user = new User();
            return active_user;
        }
        public static void PrintUsers()
        {
            users_in_db = ado.GetUsersFromDb();
            foreach(User user in users_in_db)
            {
                Console.WriteLine("User Profile");
                Console.WriteLine("Username:{0}", user.UserName);
                Console.WriteLine("------------");
            }
        }
        public static void PrintProfile()
        {
            Console.WriteLine("User Profile");
            Console.WriteLine("Username:{0}", active_user.UserName);
            Console.WriteLine("FirstName:{0}", active_user.FirstName);
            Console.WriteLine("LastName:{0}", active_user.LastName);
            Console.WriteLine("RoleId:{0}", active_user.RoleID_FK);
        }
        public static void PrintRoles()
        {
            List<Role> roles_in_db = ado.GetRolesFromDb();
            foreach (Role role in roles_in_db)
            {
                Console.WriteLine("{0}) {1}",role.RoleID,role.RoleName);
            }
        }
        public static void PrintAuthors()
        {
            List<Author> authors_in_db = ado.GetAuthorsFromDb();
            foreach(Author a in authors_in_db)
            {
                Console.WriteLine("{0}) {1} {2}", a.AuthorID, a.FirstName, a.LastName);
            }
        }
        public static void PrintBooks()
        {
            List<Book> books_in_db = ado.GetBooksFromDb();
            foreach (Book b in books_in_db)
            {
                Console.WriteLine("{0}) {1}", b.BookID, b.Title);
            }
        }
    }
}

