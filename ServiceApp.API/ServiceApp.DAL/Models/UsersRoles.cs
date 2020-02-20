namespace ServiceApp.DAL.Models
{
    public class UsersRoles
    {
        public int UsersId { get; set; }
        public int RolesId { get; set; }
        public Users Users { get; set; }
        public Roles Roles { get; set; }
    }
}
