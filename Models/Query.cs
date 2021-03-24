using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Query
    {
        string DatabaseName { get; set; }
        string tableName { get; set; }
        string query { get; set; }

        //public ArrayList ExQuery()

        //{ 
        //    string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=Ecomerce;Integrated Security=True;User ID=sa;Password=l23";
        //    SqlConnection con = new SqlConnection(connectionString);
        //    con.Open();
        //         SqlCommand cmd = new SqlCommand("select * from Product", con);
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    var data = sdr;
        //    con.Close();


        //}


    }  
}