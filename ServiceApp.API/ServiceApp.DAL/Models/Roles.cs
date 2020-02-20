using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceApp.DAL.Models
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<UsersRoles> UsersRoles { get; set; }

    }
}
