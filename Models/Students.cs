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
        public string UserName { get; set; }
        public string U_Password { get; set; }
        public int ClassID { get; set; }
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

        public Students LoginData( Students c)
        {
            conn.Open();
            string q = "select * from[fyp].[dbo].[Students] where UserName='" +c.UserName + "' and U_Password='" + c.U_Password + "'";
            string p = q;
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            Students stu = new Students();
            if (sdr.Read() )
            {
                stu.UserName = sdr["UserName"].ToString();
                stu.U_Password = sdr["U_Password"].ToString();
                stu.ClassID = int.Parse(sdr["ClassID"].ToString());
                return stu;
            }
            else
            {
                return null;
            }
    
            }

        public List<DatabaseLog> GetDatabaseLogs()
        {
            List<DatabaseLog> dblist = new List<DatabaseLog>()
;            conn.Open();
            string q = "select * from[fyp].[dbo].[DatabaseLog]";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                DatabaseLog db = new DatabaseLog();
                db.DatabaseName = sdr[1].ToString();
                db.DatabasePath= sdr[2].ToString();
                 dblist.Add(db);
            }
                return dblist;
        }

    

  
        

    }
}