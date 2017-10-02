using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestingLayout.Models.Shared
{
    public class CLabelManagerBase
    {
        public Dictionary<string, string> SystemLabels { get; set; }

        public CLabelManagerBase()
        {
            SystemLabels = new Dictionary<string, string>();
        }
        public CLabelManagerBase(string ViewModel, string LanguageCode)
        {
            SystemLabels = LoadSystemLabels(ViewModel, LanguageCode);
        }

        //private Dictionary<string, string> LoadSystemLabels(string ViewModel, string LanguageCode)
        //{
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
        //    SqlConnection con = new SqlConnection(connectionString);
        //    Dictionary<string, string> ret = new Dictionary<string, string>();

        //    using (SqlCommand cmd = new SqlCommand("SELECT LabelCode, Label FROM SystemLabels WHERE [LanguageID] = @language AND [ViewModel] = @ViewModel", con))
        //    {
        //        cmd.Parameters.AddWithValue("@language", LanguageCode);
        //        cmd.Parameters.AddWithValue("@viewmodel", ViewModel);
        //        con.Open();
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    string k = reader.GetString(reader.GetOrdinal("LabelCode")).Trim();
        //                    string v = reader.GetString(reader.GetOrdinal("Label")).Trim();
        //                    ret.Add(k, v);
        //                }
        //            }
        //        }
        //    }
        //    return ret;
        //}

        private Dictionary<string, string> LoadSystemLabels(string ViewModel, string LanguageCode)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\!Martin\Osobni\Programovani\TestingLayout\TestingLayout\App_Data\Data.mdf;Integrated Security=True";
            Dictionary<string, string> ret = new Dictionary<string, string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetLabels", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@LanguageCode", Value = LanguageCode});
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@ViewModel", Value = ViewModel});
                
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string k = reader.GetString(reader.GetOrdinal("LabelCode")).Trim();
                            string v = reader.GetString(reader.GetOrdinal("Label")).Trim();
                            //ret.Add(k, v);
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