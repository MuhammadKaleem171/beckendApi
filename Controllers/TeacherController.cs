using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using backend.Models;
namespace backend.Controllers
{
    public class TeacherController : ApiController
    {
        // GET: api/Teacher
        static string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
        SqlConnection con = new SqlConnection(connectionString);

        [HttpPost]
        public IHttpActionResult Login(AssignmentUpload c)
        {
  

            try
            {
                bool b =c.TeacherLogin(c);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        // GET: api/Teacher/5
        public AssignmentUpload Get(int AssignmentNO)
        {
            try
            {
                AssignmentUpload p = new AssignmentUpload();
                p=p.gets(AssignmentNO);
                return p;
            }catch(Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        
        public string PostAssignment(AssignmentUpload p)
        {
            AssignmentUpload up = new AssignmentUpload();
            try
            {
                p.AssignmentFile = System.Convert.FromBase64String(p.ss);


                string s = up.insert(p);
                return s;
            }catch(Exception ex)
            {
                return ex.ToString();
            }

        }

        [HttpGet]
       public IHttpActionResult GetStudentList(int Lesson)
        {
            AssignmentUpload up = new AssignmentUpload();
            try
            {
               List <AssignmentUpload> stlist = up.getListOfstudents(Lesson);
                return Ok(stlist);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult getAnswer(int lesson ,string userName)
        {
            try
            {
               StudentAssignment up= new StudentAssignment();
                List<StudentAssignment> stlist = up.getListOfAnswer(lesson,userName);
                return Ok(stlist);
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        } 


        [HttpPost]

        public string ImportDatabase(ImportDB db)
        {
            try
            {

                con.Open();
                byte[] data = Convert.FromBase64String(db.ss);
                string decodedString = Encoding.UTF8.GetString(data);

                string s=Base64Encode(db.ss);

                // split script on GO command
                IEnumerable<string> commandStrings = Regex.Split(decodedString, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        new SqlCommand(commandString, con).ExecuteNonQuery();
                    }
                }
               return "Database updated successfully.";

            }
            catch (SqlException er)
            {
                return er.Message;
            }
            finally
            {
                con.Close();
            }
        }
        public static string Base64Encode(string plainText)
        {
            string base64Decoded;
            byte[] data = System.Convert.FromBase64String(plainText);
            base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);


            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
           var decodedString = Encoding.UTF8.GetString(plainTextBytes);
            return decodedString;
        }

        // PUT: api/Teacher/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Teacher/5
        public void Delete(int id)
        {
        }
    }
}
