using ClassLibraryCommon;
using System;
using System.Collections.Generic;
using ClassLibraryDatabase;
using System.Configuration;

namespace ConsoleUI
{
    public class Program
    {
        public static List<User> active_users = new List<User>();
        public static List<Role> all_roles = new List<Role>();
        public static User active_user = new User();
        static string _connection =ConfigurationManager.ConnectionStrings["DBCONN"].ToString();
        static DbAdo ado = new DbAdo(_connection);
        static void Main(string[] args)
        {
            //ado.DeleteAllRolesInDb();
            //string[] description = { "Administrator", "Staff", "Patron" };
            //for (int i = 0; i <3 ; i++)
            //{
            // int roleid = ado.CreateRoleFromDb( new Role(description[i]));
            //}
            //List<Role> roles_in_db = ado.GetRoleFromDb();
            //Create Users in DB
            //ado.CreateUserFromDb(new User("Mary", "Christmas", "marychristmas01", "password", 3));
            //User user_test = new User("Mary", "Christmas", "marychristmas01", "password", 3);
            //ado.DeleteUserInDb(user_test);
            EntryText();
        }
        static void EntryText()
        {
            string[] description = { "Administrator", "Staff", "Patron" };
            for(int i = 1; i <= 3; i++)
            {
                all_roles.Add(new Role(i, description[i - 1]));
            }
            Console.WriteLine("Welcome to the Library Console User Interface!");
            int user_input = 0;
            while(user_input != 8)
            {
                Console.WriteLine("Current user logged in: {0}", active_user.UserName);
                Console.WriteLine("Here are the numbered options to interact with this console:");
                Console.WriteLine("1) Create User");
                Console.WriteLine("2) Login");
                Console.WriteLine("3) Logout");
                Console.WriteLine("4) Delete User");
                Console.WriteLine("5) Print User Profile");
                Console.WriteLine("6) Print Users");
                Console.WriteLine("7) Print Roles");
                Console.WriteLine("8) Exit User Interface");
                user_input = Int32.Parse(Console.ReadLine());
                switch (user_input)
                {
                    case 1:
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
                        CreateUser(firstname, lastname, new_username, password, roleid);
                        #endregion
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        Logout();
                        break;
                    case 4:
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
                    case 5:
                        PrintProfile();
                        break;
                    case 6:
                        PrintUsers();
                        break;
                    case 7:
                        PrintRoles();
                        break;
                    default:
                        break;
                }

            }
           
        }
        public static bool UserExists(string username)
        {
            if (active_users.Exists(user => user.UserName == username))
            {
                return true;
            }
            return false;
        }
        public static bool RoleExists(int roleid)
        {
            if (all_roles.Exists(role => role.RoleID == roleid))
            {
                return true;
            }
            return false;
        }
        //Usernames are checked for availability before password is prompted for...
        public static bool CreateUser(string firstname, string lastname, string username, string password, int roleid)
        {
            if (!UserExists(username))
            {
                if(!all_roles.Exists(role => role.RoleID == roleid))
                {
                    Console.WriteLine("The role ID you entered does not exist");
                    Console.WriteLine("If you would like to create a new role ID, enter yes. If you would like to enter a different role ID, enter");
                }
                User new_user = new User(firstname, lastname, username, password, roleid);
                active_users.Add(new_user);
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
        public static bool DeleteUser(string username)
        {
            if (UserExists(username)){
                active_users.Remove(active_users.Find(user => user.UserName == username));
                active_user = new User();
                Console.WriteLine("User {0} has been deleted",username);
                return true;
            }
            else
            {
                Console.WriteLine("User does not exist ");
                return false;
            }
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
        public static bool DeleteRole(int roleid)
        {
            if (RoleExists(roleid))
            {
                Role delete_me = all_roles.Find(role => role.RoleID == roleid);
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
        public static void UpdatePassword(User user,string new_password)
        {
            user.Password = new_password;
        }
        public static void Login()
        {
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
            foreach(User user in active_users)
            {
                Console.WriteLine("User Profile");
                Console.WriteLine("Username:{0}", user.UserName);
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
            foreach (Role role in all_roles)
            {
                Console.WriteLine("{0}) {1}",role.RoleID,role.RoleName);
            }
        }
    }
}

