using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryCommon;
using System.Data.SqlClient;
//ADO.NET implements IDbCommand interface for StoredProcedure 
using System.Data;
using System.Configuration;

namespace ClassLibraryDatabase
{
    public class DbAdo
    {
        //this is the connection string
        private string _conn;
        public DbAdo(string conn)
        {
            _conn = conn;
        }
        //Read Operation from CRUD. using System.Data.SqlClient 
        public List<Role> GetRolesFromDb()
        {
            List<Role> _list = new List<Role>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spGetRoles", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //potential for a lot of errors .Open()
                        con.Open();
                        Role _role; // = new Role();
                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //initilizer {}
                                _role = new Role
                                {
                                    //index out of range exception, initialize everything at once
                                    //Giancarlo was not able to find the error
                                    RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                    RoleName = (string)reader["RoleName"]
                                    //DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified"))
                                };
                                _list.Add(_role); //add object to the list object
                            }
                        }
                        con.Close(); //close the connection to the server.database.table
                        return _list;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            
            return _list; // each object represents a row of our data in the table
        }
        public List<User> GetUsersFromDb()
        {
            List<User> _list = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spGetUsers", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //potential for a lot of errors .Open()
                        con.Open();
                        User _user; // = new Role();
                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //initilizer {}
                                _user = new User
                                {
                                    //index out of range exception, initialize everything at once
                                    //Giancarlo was not able to find the error
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    UserName = (string)reader["UserName"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    Password = (string)reader["Password"],
                                    RoleID_FK = (int)reader["RoleID_FK"]
                                    //DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified"))
                                };
                                _list.Add(_user); //add object to the list object
                            }
                        }
                        con.Close(); //close the connection to the server.database.table
                        return _list;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            
            return _list; // each object represents a row of our data in the table
        }
        public List<Author> GetAuthorsFromDb()
        {
            List<Author> _list = new List<Author>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spGetAuthors", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //potential for a lot of errors .Open()
                        con.Open();
                        Author _author; // = new Role();
                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //initilizer {}
                                _author = new Author
                                {
                                    //index out of range exception, initialize everything at once
                                    //Giancarlo was not able to find the error
                                    AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID")),
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    
                                    Comment = ConvertFromDBVal<string>(reader["Comment"])
                                    //DateModified = reader.GetDateTime(reader.GetOrdinal("DateModified"))
                                };
                                _list.Add(_author); //add object to the list object
                            }
                        }
                        con.Close(); //close the connection to the server.database.table
                        return _list;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }

            return _list; // each object represents a row of our data in the table
        }
        public List<Book> GetBooksFromDb()
        {
            List<Book> _list = new List<Book>();
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spGetBooks", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //potential for a lot of errors .Open()
                        con.Open();
                        Book _book; // = new Book();
                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //var Published_Date = ConvertFromDBVal<DateTime>(reader.GetDateTime(reader.GetOrdinal("Published_Date")));

                                //var Checked_Out = reader.IsDBNull(reader.GetOrdinal("Checked_Out")) ? false : (bool)reader.GetSqlBoolean(reader.GetOrdinal("Checked_Out"));
                                //var Checkout_Date = reader.IsDBNull(reader.GetOrdinal("Checkout_Date")) ? DateTime.Now: ConvertFromDBVal<DateTime>(reader.GetSqlDateTime(reader.GetOrdinal("Checkout_Date")));
                               // var Checked_Out_ByUser_FK = ConvertFromDBVal<int>(reader.GetOrdinal("Checked_Out_ByUserID_FK"));
                               

                            //initilizer {}
                            _book = new Book
                                {
                                    BookID = reader.GetInt32(reader.GetOrdinal("BookID")),
                                    AuthorID_FK = reader.GetInt32(reader.GetOrdinal("AuthorID_FK")),
                                    Title = (string)reader["Title"],
                                    Genre = (string)reader["Genre"],
                                    Published_Date = ConvertFromDBVal<DateTime>(reader.GetDateTime(reader.GetOrdinal("Published_Date"))),

                                    Checked_Out = reader.IsDBNull(reader.GetOrdinal("Checked_Out")) ? false : (bool)reader.GetSqlBoolean(reader.GetOrdinal("Checked_Out")),
                                    Checkout_Date = reader.IsDBNull(reader.GetOrdinal("Checkout_Date")) ? DateTime.Now : ConvertFromDBVal<DateTime>(reader.GetSqlDateTime(reader.GetOrdinal("Checkout_Date"))),
                                    Checked_Out_ByUser_FK = reader.IsDBNull(reader.GetOrdinal("Checked_Out_ByUserID_FK")) ? -1: ConvertFromDBVal<int>(reader.GetInt32(reader.GetOrdinal("Checked_Out_ByUserID_FK")))
                            };
                                _list.Add(_book); //add object to the list object
                            }
                        }
                        con.Close(); //close the connection to the server.database.table
                        return _list;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }

            return _list; // each object represents a row of our data in the table
        }
        public string GetRoleNameofUserFromDb(User u)
        {
            string RoleName = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spGetRoleNameFromUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = u.UserID;
                        _sqlCommand.Parameters.Add(_paramUserID);
                        
                        ////RoleID_FK
                        //SqlParameter _paramRoleID_FK = _sqlCommand.CreateParameter();
                        //_paramRoleID_FK.DbType = DbType.Int32;
                        //_paramRoleID_FK.ParameterName = "@RoleID_FK";
                        //_paramRoleID_FK.Value = u.RoleID_FK;
                        //_sqlCommand.Parameters.Add(_paramRoleID_FK);

                        //Output value specs
                        SqlParameter _paramRoleNameReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramRoleNameReturn.Direction = ParameterDirection.Output;
                        _paramRoleNameReturn.DbType = DbType.String;
                        _paramRoleNameReturn.Size = 200;
                        //Getting the value that is being auto-incrememnted back
                        _paramRoleNameReturn.ParameterName = "@RoleNameOut";
                        var pk = _sqlCommand.Parameters.Add(_paramRoleNameReturn);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        RoleName = (string)_paramRoleNameReturn.Value; //has the auto-incremented value returned
                        con.Close();
                        //return result;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return RoleName;
        }
        // "C - INSERT" for CRUD
        public int CreateRoleToDb(Role r)
        {
            int result;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCreateRole", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRolename = _sqlCommand.CreateParameter();
                        _paramRolename.DbType = DbType.String;
                        _paramRolename.ParameterName = "@RoleName";
                        _paramRolename.Value = r.RoleName;
                        _sqlCommand.Parameters.Add(_paramRolename);

                        SqlParameter _paramComment = _sqlCommand.CreateParameter();
                        _paramComment.DbType = DbType.String;
                        _paramComment.ParameterName = "@Comment";
                        _paramComment.Value = r.Comment;
                        _sqlCommand.Parameters.Add(_paramComment);

                        SqlParameter _paramDateModified = _sqlCommand.CreateParameter();
                        _paramDateModified.DbType = DbType.DateTime;
                        _paramDateModified.ParameterName = "@DateModified";
                        _paramDateModified.Value = r.DateModified;
                        _sqlCommand.Parameters.Add(_paramDateModified);

                        SqlParameter _paramModifiedByUserID = _sqlCommand.CreateParameter();
                        _paramModifiedByUserID.DbType = DbType.DateTime;
                        _paramModifiedByUserID.ParameterName = "@ModifiedByUserID";
                        _paramModifiedByUserID.Value = r.ModifiedByUserID;
                        _sqlCommand.Parameters.Add(_paramModifiedByUserID);

                        SqlParameter _paramRoleIDReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramRoleIDReturn.Direction = ParameterDirection.Output;
                        _paramRoleIDReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramRoleIDReturn.ParameterName = "@RoleID";
                        var pk = _sqlCommand.Parameters.Add(_paramRoleIDReturn);



                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        result = (int)_paramRoleIDReturn.Value; //has the auto-incremented value returned
                        con.Close();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result = 0;
                this.LogException(ex);
            }
            return result;
        }
        public int CreateUserToDb(User u)
        {
            int result;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCreateUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramUserName = _sqlCommand.CreateParameter();
                        _paramUserName.DbType = DbType.String;
                        _paramUserName.ParameterName = "@UserName";
                        _paramUserName.Value = u.UserName;
                        _sqlCommand.Parameters.Add(_paramUserName);
                        //@FirstName varchar(200),@LastName varchar(200),@Password varchar(200), @RoleID_FK int
                        SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
                        _paramFirstName.DbType = DbType.String;
                        _paramFirstName.ParameterName = "@FirstName";
                        _paramFirstName.Value = u.FirstName;
                        _sqlCommand.Parameters.Add(_paramFirstName);
                        //LastName
                        SqlParameter _paramLastName = _sqlCommand.CreateParameter();
                        _paramLastName.DbType = DbType.String;
                        _paramLastName.ParameterName = "@LastName";
                        _paramLastName.Value = u.LastName;
                        _sqlCommand.Parameters.Add(_paramLastName);
                        //Password
                        SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                        _paramPassword.DbType = DbType.String;
                        _paramPassword.ParameterName = "@Password";
                        _paramPassword.Value = u.Password;
                        _sqlCommand.Parameters.Add(_paramPassword);
                        //RoleID_FK
                        SqlParameter _paramRoleID_FK = _sqlCommand.CreateParameter();
                        _paramRoleID_FK.DbType = DbType.Int32;
                        _paramRoleID_FK.ParameterName = "@RoleID_FK";
                        _paramRoleID_FK.Value = u.RoleID_FK;
                        _sqlCommand.Parameters.Add(_paramRoleID_FK);
                        //Email
                        SqlParameter _paramEmail = _sqlCommand.CreateParameter();
                        _paramEmail.DbType = DbType.String;
                        _paramEmail.ParameterName = "@Email";
                        _paramEmail.Value = u.Email;
                        _sqlCommand.Parameters.Add(_paramEmail);
                        //PhoneNumber
                        SqlParameter _paramPhonenumber = _sqlCommand.CreateParameter();
                        _paramPhonenumber.DbType = DbType.String;
                        _paramPhonenumber.ParameterName = "@PhoneNumber";
                        _paramPhonenumber.Value = u.PhoneNumber;
                        _sqlCommand.Parameters.Add(_paramPhonenumber);
                        //Output value specs
                        SqlParameter _paramUserIDReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramUserIDReturn.Direction = ParameterDirection.Output;
                        _paramUserIDReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramUserIDReturn.ParameterName = "@UserID";
                        var pk = _sqlCommand.Parameters.Add(_paramUserIDReturn);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        result = (int)_paramUserIDReturn.Value; //has the auto-incremented value returned
                        con.Close();
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                result = 0;
                this.LogException(ex);
            }
            return result;

        }
        public int CreateAuthorToDb(Author a)
        {
            int result;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCreateAuthor", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        
                        //@FirstName varchar(200),@LastName varchar(200),@Password varchar(200), @RoleID_FK int
                        SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
                        _paramFirstName.DbType = DbType.String;
                        _paramFirstName.ParameterName = "@FirstName";
                        _paramFirstName.Value = a.FirstName;
                        _sqlCommand.Parameters.Add(_paramFirstName);
                        //LastName
                        SqlParameter _paramLastName = _sqlCommand.CreateParameter();
                        _paramLastName.DbType = DbType.String;
                        _paramLastName.ParameterName = "@LastName";
                        _paramLastName.Value = a.LastName;
                        _sqlCommand.Parameters.Add(_paramLastName);
                        //Password
                        SqlParameter _paramComment = _sqlCommand.CreateParameter();
                        _paramComment.DbType = DbType.String;
                        _paramComment.ParameterName = "@Comment";
                        _paramComment.Value = ConvertFromDBVal<string>(a.Comment);
                        _sqlCommand.Parameters.Add(_paramComment);
                        //Output value specs
                        SqlParameter _paramAuthorIDReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramAuthorIDReturn.Direction = ParameterDirection.Output;
                        _paramAuthorIDReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramAuthorIDReturn.ParameterName = "@AuthorID";
                        var pk = _sqlCommand.Parameters.Add(_paramAuthorIDReturn);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        result = (int)_paramAuthorIDReturn.Value; //has the auto-incremented value returned
                        con.Close();
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                result = 0;
                this.LogException(ex);
            }
            return result;
        }
        public int CreateBookToDb(Book b,Author a)
        {
            int result;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCreateBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //@FirstName varchar(200),@LastName varchar(200),@Password varchar(200), @RoleID_FK int
                        SqlParameter _paramTitle = _sqlCommand.CreateParameter();
                        _paramTitle.DbType = DbType.String;
                        _paramTitle.ParameterName = "@Title";
                        _paramTitle.Value = b.Title;
                        _sqlCommand.Parameters.Add(_paramTitle);
                        //LastName
                        SqlParameter _paramGenre = _sqlCommand.CreateParameter();
                        _paramGenre.DbType = DbType.String;
                        _paramGenre.ParameterName = "@Genre";
                        _paramGenre.Value = b.Genre;
                        _sqlCommand.Parameters.Add(_paramGenre);
                        //Password
                        SqlParameter _paramPublishedDate = _sqlCommand.CreateParameter();
                        _paramPublishedDate.DbType = DbType.DateTime;
                        _paramPublishedDate.ParameterName = "@Published_Date";
                        _paramPublishedDate.Value = b.Published_Date;
                        _sqlCommand.Parameters.Add(_paramPublishedDate);
                        //RoleID_FK
                        SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
                        _paramFirstName.DbType = DbType.String;
                        _paramFirstName.ParameterName = "@AuthorFirstName";
                        _paramFirstName.Value = a.FirstName;
                        _sqlCommand.Parameters.Add(_paramFirstName);
                        //Email
                        SqlParameter _paramLastName = _sqlCommand.CreateParameter();
                        _paramLastName.DbType = DbType.String;
                        _paramLastName.ParameterName = "@AuthorLastName";
                        _paramLastName.Value = a.LastName;
                        _sqlCommand.Parameters.Add(_paramLastName);
                        //Output value specs
                        SqlParameter _paramUserIDReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramUserIDReturn.Direction = ParameterDirection.Output;
                        _paramUserIDReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramUserIDReturn.ParameterName = "@BookID";
                        var pk = _sqlCommand.Parameters.Add(_paramUserIDReturn);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        result = (int)_paramUserIDReturn.Value; //has the auto-incremented value returned
                        con.Close();
                        return result;
                    }
                }

            }
            catch (Exception ex)
            {
                result = 0;
                this.LogException(ex);
            }
            return result;
        }
        public bool LogException(Exception inException)
        {
            bool result = false;
            using (SqlConnection con = new SqlConnection(_conn))
            {
                //pass the name of the stored procedure and the connection
                using (SqlCommand _sqlCommand = new SqlCommand("spCreateLogException", con))
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;
                    //StackTrace, Message, Source, URL, Logdate
                    SqlParameter _paramStackTrace = _sqlCommand.CreateParameter();
                    _paramStackTrace.DbType = DbType.String;
                    _paramStackTrace.ParameterName = "@parmStackTrace";
                    _paramStackTrace.Value = inException.StackTrace;
                    _sqlCommand.Parameters.Add(_paramStackTrace);
                    //Message
                    SqlParameter _paramMessage = _sqlCommand.CreateParameter();
                    _paramMessage.DbType = DbType.String;
                    _paramMessage.ParameterName = "@parmMessage";
                    _paramMessage.Value = inException.Message;
                    _sqlCommand.Parameters.Add(_paramMessage);
                    //Source
                    SqlParameter _paramSource = _sqlCommand.CreateParameter();
                    _paramSource.DbType = DbType.String;
                    _paramSource.ParameterName = "@parmSource";
                    _paramSource.Value = inException.Source;
                    _sqlCommand.Parameters.Add(_paramSource);
                    //URL
                    SqlParameter _paramURL = _sqlCommand.CreateParameter();
                    _paramURL.DbType = DbType.String;
                    _paramURL.ParameterName = "@parmURL";
                    _paramURL.Value = inException.HelpLink;
                    _sqlCommand.Parameters.Add(_paramURL);
                    //Logdate
                    SqlParameter _paramLogdate = _sqlCommand.CreateParameter();
                    _paramLogdate.DbType = DbType.DateTime;
                    _paramLogdate.ParameterName = "@parmLogdate";
                    _paramLogdate.Value = DateTime.Now;
                    _sqlCommand.Parameters.Add(_paramLogdate);

                    con.Open();
                    _sqlCommand.ExecuteNonQuery(); //calls the sp
                    con.Close();
                    result = true;
                }
            }
            return result;
        }
        //D - 'DELETE' for CRUD
        public void DeleteAllRolesInDb()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spDeleteAllRoles", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }
        public void DeleteRoleInDb(Role r)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spDeleteRole", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRoleName = _sqlCommand.CreateParameter();
                        _paramRoleName.DbType = DbType.String;
                        _paramRoleName.ParameterName = "@RoleName";
                        _paramRoleName.Value = r.RoleName;
                        _sqlCommand.Parameters.Add(_paramRoleName);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }

        }
        public void DeleteUserInDb(User u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spDeleteUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramUserName = _sqlCommand.CreateParameter();
                        _paramUserName.DbType = DbType.String;
                        _paramUserName.ParameterName = "@UserName";
                        _paramUserName.Value = u.UserName;
                        _sqlCommand.Parameters.Add(_paramUserName);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
        }
        public bool DeleteAuthorInDb(Author a)
        {
            bool successful = false;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spDeleteAuthor", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramAuthorID = _sqlCommand.CreateParameter();
                        _paramAuthorID.DbType = DbType.Int32;
                        _paramAuthorID.ParameterName = "@AuthorID";
                        _paramAuthorID.Value = a.AuthorID;
                        _sqlCommand.Parameters.Add(_paramAuthorID);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        con.Close();
                        successful = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in deleting author. You might have books that depend on this author");
                this.LogException(ex);
            }
            return successful;
        }
        public bool DeleteBookInDb(Book b)
        {
            bool successful = false;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spDeleteBook", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramBookID = _sqlCommand.CreateParameter();
                        _paramBookID.DbType = DbType.Int32;
                        _paramBookID.ParameterName = "@BookID";
                        _paramBookID.Value = b.BookID;
                        _sqlCommand.Parameters.Add(_paramBookID);
                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        con.Close();
                        successful = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in deleting book");
                this.LogException(ex);
            }
            return successful;
        }
        //U - 'UPDATE' for CRUD
        public string UpdateRoleNameInDb(Role r,string new_RoleName)
        {
            string RoleName_result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spUpdateRoleName", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //find the role you want to update
                        SqlParameter _paramRoleID = _sqlCommand.CreateParameter();
                        _paramRoleID.DbType = DbType.String;
                        _paramRoleID.ParameterName = "@RoleID";
                        _paramRoleID.Value = r.RoleID;
                        _sqlCommand.Parameters.Add(_paramRoleID);
                        //update role with new name
                        SqlParameter _paramRoleName = _sqlCommand.CreateParameter();
                        _paramRoleName.DbType = DbType.String;
                        _paramRoleName.ParameterName = "@RoleName";
                        _paramRoleName.Value = new_RoleName;
                        _sqlCommand.Parameters.Add(_paramRoleName);

                        SqlParameter _paramRoleNameReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramRoleNameReturn.Direction = ParameterDirection.Output;
                        _paramRoleNameReturn.DbType = DbType.String;
                        _paramRoleNameReturn.Size = 200;
                        //Getting the value that is being auto-incrememnted back
                        _paramRoleNameReturn.ParameterName = "@RoleNameOut";
                        var pk = _sqlCommand.Parameters.Add(_paramRoleNameReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        RoleName_result = (string)_paramRoleNameReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return RoleName_result;
        }
        public string UpdateUserNameInDb(User u, string new_UserName)
        {
            string UserName_result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spUpdateUserName", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //find the user you want to update
                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.String;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = u.UserID;
                        _sqlCommand.Parameters.Add(_paramUserID);
                        //update user with new username
                        SqlParameter _paramUserName = _sqlCommand.CreateParameter();
                        _paramUserName.DbType = DbType.String;
                        _paramUserName.ParameterName = "@UserName";
                        _paramUserName.Value = new_UserName;
                        _sqlCommand.Parameters.Add(_paramUserName);

                        SqlParameter _paramUserNameReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramUserNameReturn.Direction = ParameterDirection.Output;
                        _paramUserNameReturn.DbType = DbType.String;
                        _paramUserNameReturn.Size = 200;
                        //Getting the value that is being auto-incrememnted back
                        _paramUserNameReturn.ParameterName = "@UserNameOut";
                        var pk = _sqlCommand.Parameters.Add(_paramUserNameReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        UserName_result = (string)_paramUserNameReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return UserName_result;
        }
        public string UpdateUserFirstNameInDb(User u, string new_FirstName)
        {
            string FirstName_result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spUpdateFirstName", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //find the user you want to update
                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.String;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = u.UserID;
                        _sqlCommand.Parameters.Add(_paramUserID);
                        //update user with new username
                        SqlParameter _paramFirstName = _sqlCommand.CreateParameter();
                        _paramFirstName.DbType = DbType.String;
                        _paramFirstName.ParameterName = "@FirstName";
                        _paramFirstName.Value = new_FirstName;
                        _sqlCommand.Parameters.Add(_paramFirstName);

                        SqlParameter _paramFirstNameReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramFirstNameReturn.Direction = ParameterDirection.Output;
                        _paramFirstNameReturn.DbType = DbType.String;
                        _paramFirstNameReturn.Size = 200;
                        //Getting the value that is being auto-incrememnted back
                        _paramFirstNameReturn.ParameterName = "@FirstNameOut";
                        var pk = _sqlCommand.Parameters.Add(_paramFirstNameReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        FirstName_result = (string)_paramFirstNameReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return FirstName_result;
        }
        public string UpdateUserLastNameInDb(User u, string new_LastName)
        {
            string LastName_result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spUpdateLastName", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //find the user you want to update
                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.String;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = u.UserID;
                        _sqlCommand.Parameters.Add(_paramUserID);
                        //update user with new username
                        SqlParameter _paramLastName = _sqlCommand.CreateParameter();
                        _paramLastName.DbType = DbType.String;
                        _paramLastName.ParameterName = "@LastName";
                        _paramLastName.Value = new_LastName;
                        _sqlCommand.Parameters.Add(_paramLastName);

                        SqlParameter _paramLastNameReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramLastNameReturn.Direction = ParameterDirection.Output;
                        _paramLastNameReturn.DbType = DbType.String;
                        _paramLastNameReturn.Size = 200;
                        //Getting the value that is being auto-incrememnted back
                        _paramLastNameReturn.ParameterName = "@LastNameOut";
                        var pk = _sqlCommand.Parameters.Add(_paramLastNameReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        LastName_result = (string)_paramLastNameReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return LastName_result;
        }
        public string UpdateUserPasswordInDb(User u, string new_password)
        {
            string Password_result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spUpdatePassword", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //find the user you want to update
                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.String;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = u.UserID;
                        _sqlCommand.Parameters.Add(_paramUserID);
                        //update user with new username
                        SqlParameter _paramPassword = _sqlCommand.CreateParameter();
                        _paramPassword.DbType = DbType.String;
                        _paramPassword.ParameterName = "@Password";
                        _paramPassword.Value = new_password;
                        _sqlCommand.Parameters.Add(_paramPassword);
                        //output specs
                        SqlParameter _paramPasswordReturn = _sqlCommand.CreateParameter();
                        //auto-incremented specification
                        _paramPasswordReturn.Direction = ParameterDirection.Output;
                        _paramPasswordReturn.DbType = DbType.String;
                        _paramPasswordReturn.Size = 200;
                        //Getting the value that is being auto-incrememnted back
                        _paramPasswordReturn.ParameterName = "@PasswordOut";
                        var pk = _sqlCommand.Parameters.Add(_paramPasswordReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        Password_result = (string)_paramPasswordReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return Password_result;
        }
        public int CountRolesInDb()
        {
            int count = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCountRoles", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //output specs
                        SqlParameter _paramRoleCountReturn = _sqlCommand.CreateParameter();
                        _paramRoleCountReturn.Direction = ParameterDirection.Output;
                        _paramRoleCountReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramRoleCountReturn.ParameterName = "@RoleCountOut";
                        var pk = _sqlCommand.Parameters.Add(_paramRoleCountReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        count = (int)_paramRoleCountReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return count;
        }
        public int CountUsersInDb()
        {
            int count = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCountUsers", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //output specs
                        SqlParameter _paramUserCountReturn = _sqlCommand.CreateParameter();
                        _paramUserCountReturn.Direction = ParameterDirection.Output;
                        _paramUserCountReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramUserCountReturn.ParameterName = "@UserCountOut";
                        var pk = _sqlCommand.Parameters.Add(_paramUserCountReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        count = (int)_paramUserCountReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return count;
        }
        public int CountAuthorsInDb()
        {
            int count = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCountAuthors", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //output specs
                        SqlParameter _paramUserCountReturn = _sqlCommand.CreateParameter();
                        _paramUserCountReturn.Direction = ParameterDirection.Output;
                        _paramUserCountReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramUserCountReturn.ParameterName = "@AuthorCountOut";
                        var pk = _sqlCommand.Parameters.Add(_paramUserCountReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        count = (int)_paramUserCountReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return count;
        }
        public int CountBooksInDb()
        {
            int count = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(_conn))
                {
                    //pass the name of the stored procedure and the connection
                    using (SqlCommand _sqlCommand = new SqlCommand("spCountBooks", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;
                        //output specs
                        SqlParameter _paramUserCountReturn = _sqlCommand.CreateParameter();
                        _paramUserCountReturn.Direction = ParameterDirection.Output;
                        _paramUserCountReturn.DbType = DbType.Int32;
                        //Getting the value that is being auto-incrememnted back
                        _paramUserCountReturn.ParameterName = "@BookCountOut";
                        var pk = _sqlCommand.Parameters.Add(_paramUserCountReturn);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); //calls the sp
                        count = (int)_paramUserCountReturn.Value;
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                this.LogException(ex);
            }
            return count;
        }
        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }

    }
}
