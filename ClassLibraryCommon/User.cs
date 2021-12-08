using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCommon
{
    public class User
    {
        public int UserID { get; set; } // auto increment, not null
        public string FirstName { get; set; } // not null
        public string LastName { get; set; } // not null
        public string UserName { get; set; } // not null
        public string Password { get; set; } // not null
        public int RoleID_FK { get; set; } // null

        public User(string firstname, string lastname, string username, string password, int roleid)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.UserName = username;
            this.Password = password;
            this.RoleID_FK = roleid;
        }
        public User(string firstname, string lastname, string username, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.UserName = username;
            this.Password = password;
        }
        public User(string username = "guest")
        {
            UserName = username;
        }
    }
}
