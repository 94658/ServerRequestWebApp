using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServerRequestWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, MyAuthentication.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<ApplicationUser>().ToTable("tb_User");
           
            modelBuilder.Entity<IdentityUserClaim>().ToTable("tb_UserClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("tb_Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("tb_UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("tb_UserLogin");
            modelBuilder.Entity<ServerAccessModel>().ToTable("tb_ServerAccess");
            modelBuilder.Entity<UserProfileModel>().ToTable("tb_UserProfile");
            modelBuilder.Entity<DepartmentModel>().ToTable("tb_Department");
            modelBuilder.Entity<UserLogonModel>().ToTable("tb_UserLogon");
            modelBuilder.Entity<UserLogonModel>().HasKey(t => t.UserProfileId);
            //modelBuilder.Entity<UserProfileModel>().HasRequired(x => x.UserLogons);
            //modelBuilder.Entity<UserLogonModel>().HasRequired<UserProfileModel>(u=>u.UserProfileModel).WithMany(u=>u.UserLogons).HasForeignKey(u=>u.UserProfileId);
            //modelBuilder.Entity<UserProfileModel>().HasRequired(x=>x.UserLogons)
            //modelBuilder.Entity<UserLogonModel>()
            //    .HasRequired(u => u.UserProfileModel).WithMany().HasForeignKey(u => u.UserProfileId);
            //modelBuilder.Entity<TestModel>().ToTable("tb_Test");

            //modelBuilder.Entity<LoginsModel>().ToTable("tb_Logins");
            //modelBuilder.Entity<PermissionsModel>().ToTable("tb_Permissions");
            //modelBuilder.Entity<RolePermissionModel>().ToTable("tb_lnk_role_permissions");

            //modelBuilder.Entity<PermissionsModel>()
            //  .HasMany(e => e.Roles)
            //  .WithMany(e => e.Permissions)
            //  .Map(m => m.ToTable("tb_lnk_role_permission").MapLeftKey("Permission_Id").MapRightKey("Role_Id"));

            // modelBuilder.Entity<RoleModel>()
            //.HasMany(e => e.Userss)
            //.WithMany(e => e.Roles)
            //.Map(m => m.ToTable("tb_lnk_user_role").MapLeftKey("Role_Id").MapRightKey("User_Id"));

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ServerRequestWebApp.Models.ServerAccessModel> ServerAccessModels { get; set; }

        public System.Data.Entity.DbSet<ServerRequestWebApp.Models.UserProfileModel> UserProfile { get; set; }
        public System.Data.Entity.DbSet<ServerRequestWebApp.Models.DepartmentModel> Departments { get; set; }

    }
}