using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;

namespace backend.Controllers
{
    public class StudentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getMarks(int lesson)
        {
            try
            {
                StudentAssignment up = new StudentAssignment();
                string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("select * from StudentAssignment where LessonNO=" + lesson + "", con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    up.Marks = int.Parse(sdr["Marks"].ToString());
                    if (up.Marks>0)
                    {
                        return Ok(up);
                    }
                    else
                    {
                        continue;
                    }

                }

                con.Close();
                return Ok(up);
               
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        // POST api/<controller>
        [HttpPost]
        public string PostAssignment(StudentAssignment sa )
        {
            try
            {
                //sa.AssignmentFile = Convert.FromBase64String(sa.base64);
                StudentAssignment s = new StudentAssignment();
                string d=s.InsertAssignment(sa);
                return d;
            }catch(Exception ex)
            {
               return ex.ToString();
            }
        }
        [HttpGet]
        public IHttpActionResult ListofDatabase()
        {
            try
            {
                List <DatabaseLog > dblist= new List<DatabaseLog>();
                Students  stu =new Students();
                dblist = stu.GetDatabaseLogs();
                return Ok(dblist);
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]

        public IHttpActionResult AddDatabaseByStudent(DatabaseLog db)
        {
            try
            {
                FileInfo file = new FileInfo(db.DatabasePath);

                var script = file.OpenText().ReadToEnd();

                var sqlqueries = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                sqlqueries[1] = "CREATE DATABASE ["+"user"+db.UserName+"]";
                sqlqueries[2] = "use ["+ "user"+db.UserName+"]";

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
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        public IHttpActionResult PostMarks(StudentAssignment student)
        {
            string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                try {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("query", con))
                    {
                        string q = "update StudentAssignment set Marks=" + student.Marks + "  where LessonNo=+" + student.LessonNo + "and UserName='" + student.UserName + "'";
                        cmd.CommandText = q;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return Ok();
                    }
                }catch(Exception ex)
                {
                    return BadRequest(ex.ToString());
                }

        }
        }
        



    }
}