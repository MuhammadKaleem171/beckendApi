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
        public string UserName { get; set; }
       public string Query { get; set; }
        static string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
        SqlConnection con = new SqlConnection(connectionString);
        public List<SaveQuery> GetSave_Query(string UserName)

        {
             List<SaveQuery>qs = new List<SaveQuery>();
           
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

        public string insertQuery(SaveQuery qs)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into SaveQuery (Query,DatabaseName,UserName,Query_Name) values ('" + qs.Query + "','" + qs.DatabaseName + "','" + qs.UserName +" ','" + qs.Query_Name + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                return "success";
            }catch(Exception ex)
            {
                return ex.ToString();
            }
        }

    }  
}