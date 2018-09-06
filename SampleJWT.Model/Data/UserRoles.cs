
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleJWT.Model.Data
{
    
    [Dapper.Contrib.Extensions.Table("tblUserRole")]
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Column("colUserName")]
        public string UserName { get; set; }
        [Column("colRole")]
        public string Role { get; set; }
    }
}
