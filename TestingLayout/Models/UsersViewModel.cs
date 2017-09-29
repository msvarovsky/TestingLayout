using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestingLayout.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string lName { get; set; }

        public string Sex { get; set; }
        public string lSex { get; set; }
    }
    public class UsersViewModel
    {
        public List<User> seznam { get; set; }

        public UsersViewModel()
        {
            seznam = new List<User>();
        }

        public string Add(User u)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@name", Value = u.Name });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@sex", Value = u.Sex });

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                return null;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public void Load()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, Sex FROM Users", con))
            {
                con.Open();


                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            User u = new User();

                            u.ID = reader.GetInt32(reader.GetOrdinal("Id"));
                            u.Name = reader.GetString(reader.GetOrdinal("name"));
                            u.Sex = reader.GetString(reader.GetOrdinal("sex"));

                            seznam.Add(u);
                        }
                    }
                }
            }


        }

       
    }
}