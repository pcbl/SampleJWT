using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SampleJWT.Security
{
    public class CustomUserIdentity: GenericIdentity
    {
        public CustomUserIdentity(string userName)
               : base(userName)
        {
            UserName = userName;
            Roles = new string[] { };
        }


        public string UserName { get; set; }
        public string Sid { get; set; }

        public string[] Roles { get; set; }

        //Here we will add any Business Related information we might need to track
    }
}
