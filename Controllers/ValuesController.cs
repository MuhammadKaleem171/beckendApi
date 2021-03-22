﻿using backend.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace backend.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IHttpActionResult Get()
        {
           
            Students sc = new Students();
            List<Students> s =s= sc.GetRecordss();
            return Ok(s);


        }
        [HttpPost]
        public IHttpActionResult Login(Students c)
        {
            Boolean b = false;
            try
            {
               b= c.LoginData(c);
                return Ok(b);
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // GET api/values/5
        [HttpGet]
        public  IHttpActionResult GetDatabase()
        {
            List<string> databaseName=new List<string>();
            string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name FROM master.dbo.sysdatabases WHERE name NOT IN ( 'tempdb', 'model', 'msdb')", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            databaseName.Add(dr[0].ToString());
                            Console.WriteLine(dr[0].ToString());
                        }
                    }
                }
            }
                return Ok(databaseName);
        }
        [HttpGet]
        public IHttpActionResult GetTable(string TableName)
        {
            
            List<string> databaseName = new List<string>();
            
            string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog='" + TableName + "';Integrated Security=True;User ID=sa;Password=l23";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select table_name from "+TableName+".INFORMATION_SCHEMA.TABLES where TABLE_TYPE = 'BASE TABLE'", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            databaseName.Add(dr[0].ToString());
                            Console.WriteLine(dr[0].ToString());
                        }
                    }
                }
            }
            return Ok(databaseName);
        }

        [HttpGet]
        public IHttpActionResult GetTableColumn(string table)
        {

            List<ColumnName> databaseName = new List<ColumnName>();
            int id = 1;
            string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=Ecomerce;Integrated Security=True;User ID=sa;Password=l23";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("EXEC sp_columns '"+table+"'", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ColumnName n = new ColumnName();
                            n.column = dr[3].ToString();
                            n.isChecked = false;
                            n.id = id;
                            databaseName.Add(n);
                            id = id + 1;
                        }
                    }
                }
            }
            return Ok(databaseName);
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post(Students c)
        {
            try
            {
                Console.WriteLine(c.UserName, c.U_Password);
                Students sc = new Students();
                sc.insert(c);
                return Ok(c);
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}