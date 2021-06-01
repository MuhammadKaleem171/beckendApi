using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backend.Models;
namespace backend.Controllers
{
    public class TeacherController : ApiController
    {
        // GET: api/Teacher


        // GET: api/Teacher/5
        public void Get()
        {
            AssignmentUpload p = new AssignmentUpload();
            p.gets();
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
