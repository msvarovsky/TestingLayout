using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestingLayout.Models.Shared;

namespace TestingLayout.Models
{
    public class User : CLabelManagerBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //public string lName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        //public string lSex { get; set; }
        public bool Active { get; set; }
        public System.DateTime? LastLogin { get; set; }

        public User():base()
        {

        }

        public bool IsReadOnly(bool a)
        {
            return a;
            //return a? "readonly" : "";
        }

        public User(string ViewModel, string LanguageCode="en"): base(ViewModel, LanguageCode="en")
        {
            Name = "New name...";
            Active = true;
        }
    }

    public class UsersViewModel
    {
        public List<User> UserData { get; set; }
        public Dictionary<string, string> SystemLabels { get; set; }

        public UsersViewModel(string LanguageCode="en")
        {
            UserData = new List<User>();
            SystemLabels = new Dictionary<string, string>();
            
            LoadUserData();
        }
        
        private void LoadUserData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand("SELECT Id, Name, Sex FROM Users", con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User u = new User();
                            u.ID = reader.GetInt32(reader.GetOrdinal("Id"));
                            u.Name = reader.GetString(reader.GetOrdinal("name"));
                            u.Sex = reader.GetString(reader.GetOrdinal("sex"));
                            UserData.Add(u);
                        }
                    }
                }
            }
        }

        public Dictionary<string, string> LoadSystemLabels(string ViewModel, string LanguageCode)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(connectionString);
            Dictionary<string, string> ret = new Dictionary<string, string>();

            using (SqlCommand cmd = new SqlCommand("SELECT LabelCode, Label FROM SystemLabels WHERE [LanguageID] = @language AND [ViewModel] = @ViewModel", con))
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

            if (ret == null)
                return "**" + LabelCode + "**"; 
                        
            return ret;
        }

       
    }
}