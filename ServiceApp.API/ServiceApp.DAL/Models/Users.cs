using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceApp.DAL.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }
        public ICollection<UsersRoles> UsersRoles { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
