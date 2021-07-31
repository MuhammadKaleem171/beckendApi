using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend
{
    public class UpdateDTO
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string Query { get; set; }
        public string UserName { get; set; }
    }
}