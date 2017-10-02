using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TestingLayout.Models
{
    public class NewUserViewModel
    {
        public User UserData { get; set; }
        public Dictionary<string, string> SystemLabels { get; set; }
        public SelectList gender { get; set; }

        public NewUserViewModel(string LanguageCode="en")
        {
            SystemLabels = LoadSystemLabels("NewUser", LanguageCode);
            gender = GetAllSexes(LanguageCode);
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

        public SelectList GetAllSexes(string language)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            List<SelectListItem> l = new List<SelectListItem>();

            using (SqlCommand cmd = new SqlCommand("SELECT SexCode, SexName FROM GL_Sex WHERE [LanguageCode] = @language", con))
            {
                cmd.Parameters.AddWithValue("@language", language);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string sc = reader.GetString(reader.GetOrdinal("SexCode")).Trim();
                            string sn = reader.GetString(reader.GetOrdinal("SexName")).Trim();
                            l.Add(new SelectListItem { Selected = false, Text = sn, Value = sc});
                        }
                    }
                }
            }
            return new SelectList(l, "Value", "Text");
        }
        private Dictionary<string, string> LoadSystemLabels(string ViewModel, string LanguageCode)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            Dictionary<string, string> ret = new Dictionary<string, string>();

            using (SqlCommand cmd = new SqlCommand("SELECT LabelCode, Label FROM SystemLabels WHERE [LanguageCode] = @language AND [ViewModel] = @ViewModel", con))
            {
                cmd.Parameters.AddWithValue("@language", LanguageCode);
                cmd.Parameters.AddWithValue("@viewmodel", ViewModel);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string k = reader.GetString(reader.GetOrdinal("LabelCode")).Trim();
                            string v = reader.GetString(reader.GetOrdinal("Label")).Trim();
                            ret.Add(k, v);
                        }
                    }
                }
            }
            return ret;
        }

        public string GetLabel(string LabelCode)
        {
            string ret;
            SystemLabels.TryGetValue(LabelCode.ToLower(), out ret);

            // SystemLabels[LabelCode.ToLower()]==null ? "**"+LabelCode+"**" : SystemLabels[LabelCode.ToLower()]
            // string a = SystemLabels.ContainsKey(LabelCode.ToLower()) ? SystemLabels[LabelCode.ToLower()] : "**"

            if (ret == null)
                return "**" + LabelCode + "**";

            return ret;
        }

    }
}