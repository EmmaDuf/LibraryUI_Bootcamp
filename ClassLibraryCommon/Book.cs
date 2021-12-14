using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryCommon
{
    public class Book
    {
        public int BookID { get; set; } // primary key, auto-incremented, not null
        public int AuthorID_FK { get; set; } // foregin key, not null
        public string Title { get; set; } //not null
        public string Genre { get; set; } // not null
        public DateTime Published_Date { get; set; } = DateTime.Now;//null
        public bool? Checked_Out { get; set; } // null
        public DateTime Checkout_Date { get; set; } = DateTime.Now;// null
        public int? Checked_Out_ByUser_FK { get; set; } // null
        //for creating Books to Db
        public Book(int authorid_fk, string title, string genre, DateTime published_date, bool checked_out = false, int checked_out_byuser_fk = -1)
        {
            this.AuthorID_FK = authorid_fk;
            this.Title = title;
            this.Genre = genre;
            this.Published_Date = published_date;
            this.Checked_Out = checked_out;
            this.Checked_Out_ByUser_FK = checked_out_byuser_fk;
        }
        //for getting books from Db
        public Book(int authorid_fk, string title, string genre, DateTime published_date, DateTime checkout_date, bool checked_out = false, int checked_out_byuser_fk = -1)
        {
            this.AuthorID_FK = authorid_fk;
            this.Title = title;
            this.Genre = genre;
            this.Published_Date = published_date;
            this.Checked_Out = checked_out;
            this.Checkout_Date = checkout_date;
            this.Checked_Out_ByUser_FK = checked_out_byuser_fk;
        }
        public Book()
        {

        }
    }
}
