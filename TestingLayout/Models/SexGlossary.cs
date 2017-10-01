using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestingLayout.Models
{
    public class CSex
    {
        public string SexCode { get; set; }
        public string SexName { get; set; }
    }

    public class SexGlossary
    {
        public List<CSex> seznam { get; set; }

        public SexGlossary()
        {
            seznam = new List<CSex>();
        }

        public List<CSex> GetAllSexes(string language)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT SexCode, SexName FROM GL_Sex WHERE [Language] = @language", con))
            {
                cmd.Parameters.AddWithValue("@language", language);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            CSex s = new CSex();
                            s.SexCode = reader.GetString(reader.GetOrdinal("Sex"));
                            s.SexName = reader.GetString(reader.GetOrdinal("SexName"));
                            seznam.Add(s);
                        }
                    }
                }
            }
            return seznam;
        }
     
    }
}