using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class SaveQuery
    {
      public  string DatabaseName { get; set; }
      public   string Query_Name { get; set; }
       public string Query { get; set; }

        public List<SaveQuery> GetSave_Query(string UserName)

        {
             List<SaveQuery>qs = new List<SaveQuery>();
            string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from SaveQuery where UserName='"+UserName+"'", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                SaveQuery s = new SaveQuery();
                s.Query = sdr[1].ToString();
                s.DatabaseName = sdr[2].ToString();
                s.Query_Name = sdr[4].ToString();
                qs.Add(s);
            }
           
            con.Close();
            return qs;

        }


    }  
}