using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCommon
{
    public class Author
    {
        public int AuthorID { get; set; } //primary key, db auto-increment, not null
        public string FirstName { get; set; } // not null
        public string LastName { get; set; } //not null
        public string Comment { get; set; } //null
        public Author(string firstname, string lastname, string comment = null)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Comment = comment;
        }
        public Author()
        {

        }
    }
}
