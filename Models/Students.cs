using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Students
    {
        static string con = ConfigurationManager.ConnectionStrings["backend.Properties.Settings.ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(con);
        public String UserName { get; set; }
        public String U_Password { get; set; }

        public List<Students> GetRecordss()
        {

            List<Students> stlist = new List<Students>();

            conn.Open();
            string q = "select * from[fyp].[dbo].[Students]";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                Students cs = new Students();
                cs.UserName = sdr[0].ToString();
                cs.U_Password = sdr[1].ToString();
                
                stlist.Add(cs);
            }

            return stlist;
        }

        public void insert(Students c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText= "insert into [fyp].[dbo].[Students] values('" + c.UserName + "','" + c.U_Password + "')";
            cmd.Connection = conn;
            conn.Open();
             cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool LoginData( Students c)
        {
            conn.Open();
            string q = "select * from[fyp].[dbo].[Students] where UserName='" +c.UserName + "' and U_Password='" + c.U_Password + "'";
            string p = q;
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
    
            }

  
        

    }
}