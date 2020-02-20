using Microsoft.EntityFrameworkCore;
using ServiceApp.DAL.Models;
using System;
using System.Text;

namespace ServiceApp.DAL.EFContext
{
    public class ServiceContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UsersRoles> UsersRoles { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        static ServiceContext()
        {

        }
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UsersRoles>()
                .HasKey(bc => new { bc.UsersId, bc.RolesId });
            builder.Entity<UsersRoles>()
                .HasOne(bc => bc.Users)
                .WithMany(b => b.UsersRoles)
                .HasForeignKey(bc => bc.UsersId);
            builder.Entity<UsersRoles>()
                .HasOne(bc => bc.Roles)
                .WithMany(c => c.UsersRoles)
                .HasForeignKey(bc => bc.RolesId);


            Roles adminRole = new Roles { RoleId = 1, RoleName = "admin" };
            Roles userRole = new Roles { RoleId = 2, RoleName = "user" };
            builder.Entity<Roles>().HasData(new Roles[] { adminRole, userRole });

            Users adminUsers = new Users
            {
                UserId = 1,
                UserEmail = "admin@mail.ru",
                Password = GetHashString("123456"),
                Name = "Admin",
            };

            builder.Entity<Users>().HasData(new Users[] { adminUsers });

            UsersRoles usersRoles1 = new UsersRoles
            {
                RolesId = adminRole.RoleId,
                UsersId = adminUsers.UserId
            };

            UsersRoles usersRoles2 = new UsersRoles
            {
                RolesId = userRole.RoleId,
                UsersId = adminUsers.UserId
            };

            builder.Entity<UsersRoles>().HasData(new UsersRoles[] { usersRoles1, usersRoles2 });


            base.OnModelCreating(builder);
        }


        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] byteHash = GetHash(bytes);

            string hash = Convert.ToBase64String(byteHash);

            return hash;
        }
        private byte[] GetHash(byte[] bytes)
        {
            using (var sha = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha.ComputeHash(bytes);
                return hash;
            }
        }
    }
}
