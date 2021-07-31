using backend.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.IO;
using Microsoft.Extensions;
using HttpContext = System.Web.HttpContext;

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
        [HttpGet]
        public IHttpActionResult Login(string userName,string password)
        {
            Students c = new Students();
            c.UserName = userName;
            c.U_Password = password;
           
            try
            {
               Students stu= c.LoginData(c);
                return Ok(stu);
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
        public IHttpActionResult GetTableColumn(string table,string DatabaseName)
        {

            List<ColumnName> databaseName = new List<ColumnName>();
            int id = 1;
            string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog='" + DatabaseName + "';Integrated Security=True;User ID=sa;Password=l23";
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

        [HttpGet]

        public IHttpActionResult ExcQuery(string query, string Table)
        {
           
            try
            {
                ArrayList aa = new ArrayList();
                string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog='" + Table + "';Integrated Security=True;User ID=sa;Password=l23";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                int a = dt.Rows.Count;
                var r = dt.Rows[0].Table;
                aa.Add(r);

                con.Close();

                return Ok(aa);
} catch (Exception ex)
            {
                return BadRequest(ex.ToString());
             }
        }
   

        [HttpGet]
        public IHttpActionResult SaveQuery(string UserName)
        {
            try
            {
                SaveQuery qss = new SaveQuery();
              var qs= qss.GetSave_Query(UserName).ToList();
                foreach( var i in qs)
                {
                    Console.WriteLine(i);
                }
                return Ok(qs.ToList());

            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
          
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
        [HttpPost]
        public IHttpActionResult PostQuery(SaveQuery q)
        {
            try
            {
                SaveQuery qs = new SaveQuery();
                qs.UserName = "17-arid-3460";
                qs.Query_Name = q.Query_Name;
                qs.Query = q.Query;
                qs.DatabaseName = q.DatabaseName;
                qs.insertQuery(qs);
                return Ok("Successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

       [HttpPost]
        public IHttpActionResult UpdateQueryExecute( UpdateDTO model)
        {
            try
            {
                string TempTable = "User" + DateTime.Now.Millisecond.ToString();
                ArrayList aa = new ArrayList();
                string d = "";
                string query = "select * into " + TempTable + "  from " + model.TableName;
                //query+= "Update " + TempTable+" " + model.Query;
                // query += "update ##tempTable set productName='sumsung' where productId=1";
                string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=Ecomerce;Integrated Security=True;User ID=sa;Password=l23";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int c = cmd.ExecuteNonQuery();
                    }
                    //update query
                    string q = "Update " + TempTable + " " + model.Query;
                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        int c = cmd.ExecuteNonQuery();

                    }

                    q = "select * from " + TempTable;

                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        int a = dt.Rows.Count;
                        var r = dt.Rows[0].Table;
                        aa.Add(r);
                    }
                    string drop = "drop table " + TempTable;
                    using (SqlCommand cmd = new SqlCommand(drop, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(aa);
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        public IHttpActionResult receiveFile()
        {
            try
            {
              
                var httprequest = HttpContext.Current.Request;
                var f = httprequest.Files[0];

                var fileName = f.FileName;
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                var fileExtension = f.ContentType;
                var newFileName = String.Concat(myUniqueFileName, fileName);
  
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Resource/SqlFiles"),newFileName);
                f.SaveAs(path);
                //read file
                FileInfo file = new FileInfo(path);
                //
                int flag = Savepath(fileName, path);

                var script = file.OpenText().ReadToEnd();

                var sqlqueries = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("query", con))
                    {
                        foreach (var query in sqlqueries)
                        {
                            cmd.CommandText = query;
  
                            cmd.ExecuteNonQuery();
                        }

                        con.Close();
                    }


                }

                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        public int Savepath(string filename,string path)
        {
            try
            {
                string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("query", con))
                    {
                        cmd.CommandText = "insert into DatabaseLog ([DatabaseName],[DatabasePath]) values('" + filename + "','" + path + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                  }

                return 1;
            }catch(Exception ex)
            {
                return -1;
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
