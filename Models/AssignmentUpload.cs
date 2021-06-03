using System;
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
               string q= "insert into UploadAssignment (T_id,ClassID,AssignmtNo,AssignmentName,AssignmentFile,DateOfSubmission) values (@T_id, @ClassID,@AssignmtNo,@assName,@AssignmentFile, @Date)";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@T_id", up.T_id);
                cmd.Parameters.AddWithValue("@ClassID", up.ClassID);
                cmd.Parameters.AddWithValue("@AssignmtNo", up.AssignmtNo);
                cmd.Parameters.AddWithValue("@assName", up.AssignmentName);

                cmd.Parameters.AddWithValue("@AssignmentFile", up.AssignmentFile);

                cmd.Parameters.AddWithValue("@Date", Date);

                cmd.ExecuteNonQuery();
                con.Close();



                return "succesfull ";
            }catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public AssignmentUpload gets( int AssignmentNO)
        {
            con.Open();
            AssignmentUpload up = new AssignmentUpload();
            SqlCommand cmd = new SqlCommand("select * from UploadAssignment where AssignmtNo=" + AssignmentNO + "", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                up.AssignmentName = sdr["AssignmentName"].ToString();
                up.AssignmentFile = (byte[])sdr["AssignmentFile"];
                up.ss = Convert.ToBase64String(up.AssignmentFile);
            }
           
            con.Close();
            return up;
        }




    }
}