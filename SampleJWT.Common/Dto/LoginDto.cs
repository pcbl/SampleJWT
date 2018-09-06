using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleJWT.Dto
{
    public class LoginDto
    {
        public string Domain { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}