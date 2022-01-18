using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Academy_Portal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Editable(true)]
        public DateTime? DoB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        //Can be Admin,Faculty,Employee
        [Display(Name = "User Category")]
        public string UserCategory { get; set; }

        [Display(Name ="User ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        //Create a new field for login status for all
        //Bydefault it is 1 for Admin and Employee and 0 for Faculty
        public int LoginStatus { get; set; }

        [Required]
        [Display(Name = "What city were you born in?")]
        [MaxLength(50)]
        public string SecurityParameter1 { get; set; }

        [Required]
        [Display(Name = "What is your oldest sibling’s last name?")]
        [MaxLength(50)]
        public string SecurityParameter2 { get; set; }

        [Required]
        [Display(Name = "In what city or town did your parents meet?")]
        [MaxLength(50)]
        public string SecurityParameter3 { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class FacultyUser : ApplicationUser
    {
        public string Proficiencylevel { get; set; }
        public int? TotalHoursOfTeaching { get; set; }

        //One-One Relationship between FacultyUser and Skills Table
        public int? SkillID { get; set; }

        //One to One Relationship between FacultyUser and Modules Tables
        public int? ModuleID { get; set; }


    }
    public class EmployeeUser : ApplicationUser
    {
        //Employee can register for a batch and admin can change this parameter to 0 - pending 1 - approved -1 - rejected
        public int? BatchNominationStatus { get; set; }
        //Employee will have the Id key of the batch they nominate for
        public int? BatchID { get; set; }
        //Many to Many Relationship between Batches and EmployeeUser
        //Because One Batches can be mapped to Many Employees And One Employee can have Many Batches
        //Adding a navigational property
        public virtual ICollection<Batch> Batches{ get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Defining Skills Table in Database for Admin to enter details
        public DbSet<Skill> Skills { get; set; }
        //Defining Modules Table in Database for Admin to enter details
        public DbSet<Module> Modules { get; set; }
        //Defining Batches Table in Database for Admin to enter details
        public DbSet<Batch> Batches { get; set; }
        //Defining the skillModule joining table to query seperately
        public DbSet<SkillModule> SkillModules { get; set; }
        //Defining the Help table in database for Faculty and Employee to Use
        public DbSet<Help> Help { get; set; }
        //Overriding COnventions
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //1. SkillModule Many-Many Relation between Skills and Modules
            modelBuilder.Entity<SkillModule>()
                .HasKey(sm => new { sm.SkillID, sm.ModuleID });
            modelBuilder.Entity<SkillModule>()
                .HasRequired(sm=>sm.Module)
                .WithMany(m=>m.SkillModules)
                .HasForeignKey(sm=>sm.ModuleID);
            modelBuilder.Entity<SkillModule>()
                .HasRequired(sm => sm.Skill)
                .WithMany(s => s.SkillModules)
                .HasForeignKey(sm => sm.SkillID);

            //2. One-Many btw Module and Batches
            modelBuilder.Entity<Batch>()
                .HasRequired(b => b.Module)
                .WithMany(m => m.Batches)
                .HasForeignKey(b => b.ModuleID);

            //3. One-Many btw Skill and Batches
            modelBuilder.Entity<Batch>()
                .HasRequired(b => b.Skill)
                .WithMany(m => m.Batches)
                .HasForeignKey(b => b.SkillID);

            //4. Many to Many btw EmployeeUser(Parent: User) and Batches

            base.OnModelCreating(modelBuilder);
        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Academy_Portal.Models.FacultyUser> ApplicationUsers { get; set; }
        public System.Data.Entity.DbSet<Academy_Portal.Models.EmployeeUser> EmployeeUsers { get; set; }
    }
}
