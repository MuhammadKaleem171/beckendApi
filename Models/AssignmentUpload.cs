using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class AssignmentUpload
    {
        public int AssignmentID { get; set; }
        public string T_id { get; set; }
        public int ClassID { get; set; }
        public int AssignmtNo { get; set; }
        public string AssignmentName { get; set; }
        public byte[] AssignmentFile { get; set; }
        public string DatabaseName { get; set; }
        public int LessonNo { get; set; }

        public string UserName { get; set; }
        public DateTime DateOFUpload { get; set; }
        public string ss { get; set; }
        static string connectionString = @"Data Source=MALIKKALEEM\SQLEXPRESS01;Initial Catalog=fyp;Integrated Security=True;User ID=sa;Password=l23";
        SqlConnection con = new SqlConnection(connectionString);

        public string insert(AssignmentUpload up)
        {
            try
            {
                string Date = DateTime.Now.ToShortDateString();
                con.Open();
                string q = "insert into UploadAssignment (T_id,ClassID,LessonNo,AssignmentName,AssignmentFile,DateOfSubmission,DatabaseName) values (@T_id, @ClassID,@LessonNo,@assName,@AssignmentFile, @Date,@DatabaseName)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@T_id", up.T_id);
                cmd.Parameters.AddWithValue("@ClassID", up.ClassID);
                cmd.Parameters.AddWithValue("@LessonNo", up.LessonNo);
                cmd.Parameters.AddWithValue("@assName", up.AssignmentName);

                cmd.Parameters.AddWithValue("@AssignmentFile", up.AssignmentFile);

                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@DatabaseName", up.DatabaseName);

                cmd.ExecuteNonQuery();
                con.Close();



                return "succesfull ";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public AssignmentUpload gets(int AssignmentNO)
        {
            try
            {
                con.Open();
                AssignmentUpload up = new AssignmentUpload();
                SqlCommand cmd = new SqlCommand("select * from UploadAssignment where LessonNO=" + AssignmentNO + "", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    up.AssignmentName= sdr["AssignmentName"].ToString();
                    up.AssignmentFile = (byte[])sdr["AssignmentFile"];
                    up.ss = Convert.ToBase64String(up.AssignmentFile);
                    up.LessonNo = int.Parse(sdr["LessonNO"].ToString());
                    up.AssignmentID = int.Parse(sdr["AssignmentID"].ToString());
                }

                con.Close();
                return up;
            }catch(Exception ex)
            {
                return null;
            }

        }
        public List<AssignmentUpload> getListOfstudents(int lessonNo)
        {
            con.Open();
            List<AssignmentUpload> stList = new List<AssignmentUpload>();
            try
            {
                SqlCommand cmd = new SqlCommand("select DISTINCT UserName,LessonNo,DatabaseName from StudentAssignment where LessonNo =" + lessonNo + "", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    AssignmentUpload up = new AssignmentUpload();
                    up.UserName = sdr["UserName"].ToString();
                    up.LessonNo = int.Parse(sdr["LessonNo"].ToString());
                    up.DatabaseName = sdr["DatabaseName"].ToString();
                    stList.Add(up);
                }

                con.Close();
                return stList;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

       

    }
}