using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy_Portal.Models
{
    public class Batch
    {
        public int BatchID { get; set; }

        [Required]
        public string Technology { get; set; }

        [Required]
        [Display(Name="Batch Start Date")]
        [DataType(DataType.Date)]
        [Editable(true)]
        public DateTime? BatchStartDate { get; set; }

        [Required]
        [Display(Name = "Batch End Date")]
        [DataType(DataType.Date)]
        [Editable(true)]
        public DateTime? BatchEndDate { get; set; }

        [Required]
        [Display(Name ="Batch Capacity")]
        public int BatchCapacity { get; set; }

        [Required]
        [Display(Name ="Classroom Name")]
        [MaxLength(100)]
        public string ClassroomName { get; set; }

        [Required]
        [Display(Name ="Faculty Id")]
        public int FacultyID { get; set; }//This will refer to the UserId-Custom that we created
        public int BatchApproval { get; set; }//This will be used to accept or reject bacth
        public int RemainingCapacity { get; set; }
        //Relative Data - FK
        //1. Module Relations
        public int ModuleID { get; set; }
        public Module Module { get; set; }
        //2.Skill Relations
        public int SkillID { get; set; }
        public Skill Skill { get; set; }

        //3. Employee User - Batch Many to Many
        public virtual ICollection<EmployeeUser> EmployeeUsers { get; set; }=new HashSet<EmployeeUser>();



    }
}