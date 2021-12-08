using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCommon
{
    public class Role
    {
        public int RoleID { get; set; } // auto increment
        public string RoleName { get; set; } // not null
        public string Comment { get; set; } //null
        public DateTime DateModified { get; set; } = DateTime.Now; //null
        public DateTime ModifiedByUserID { get; set; } = DateTime.Now;//null
        public Role(int roleid, string rolename)
        {
            this.RoleID = roleid;
            this.RoleName = rolename;
        }
        public Role()
        {

        }
        public Role(string rolename,DateTime datemodified, DateTime modifiedbyuserid, string comment = null)
        {
            this.RoleName = rolename;
            this.Comment = comment;
            this.DateModified = datemodified;
            this.ModifiedByUserID = modifiedbyuserid;
        }
        public Role(string rolename, string comment = null)
        {
            this.RoleName = rolename;
            this.Comment = comment;
        }
    }
}
