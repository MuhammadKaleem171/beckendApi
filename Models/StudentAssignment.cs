using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class StudentAssignment
    {
        public int StuAssID {get ;set;}
        public int AssignmentID { get; set; }
        public int classID { get; set; }
        public string UserName { get; set; }
        public byte[] AssignmentFile { get; set; }
        public string QuestionNo { get; set; }
        public string Answer { get; set; }
        public string DatabaseName { get; set; }

        public string base64 { get; set; }
        static string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
        SqlConnection con = new SqlConnection(connectionString);



        public string InsertAssignment(StudentAssignment sp)
        {
            try
            {
          con.Open();
      string q = "insert into StudentAssignment (UserName,AssignmentID,classID,AssignmentFile) values (@User,@assID, @ClassID,@AssignmentFile)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@User", sp.UserName);
                cmd.Parameters.AddWithValue("@assID", sp.AssignmentID);
                cmd.Parameters.AddWithValue("@ClassID", sp.classID);
                cmd.Parameters.AddWithValue("@AssignmentFile", sp.AssignmentFile);

                cmd.ExecuteNonQuery();
                con.Close();


                return "SuccessFul";
            }catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        public List<StudentAssignment> getListOfAnswer(int lesson, string Username)
        {
          List<StudentAssignment> stlist = new List<StudentAssignment>();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select QuestionNo,Answer,DatabaseName from StudentAssignment where LessonNo=" + lesson + "and UserName='" + Username + "'", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    StudentAssignment up = new StudentAssignment();
                 up.QuestionNo  = sdr["QuestionNo"].ToString();
                    up.Answer= sdr["Answer"].ToString();

                    up.DatabaseName = sdr["DatabaseName"].ToString();
                    stlist.Add(up);
                }

                con.Close();
                return stlist;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}