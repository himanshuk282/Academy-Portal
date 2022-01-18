using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy_Portal.Models
{
    public class Module
    {
        public int ModuleID { get; set; }
        [Required]
        public string Technology { get; set; }
        [Required]
        [Display(Name ="Proficiency Level")]
        [MaxLength(50)]
        public string ProficiencyLevel { get; set; }
        [Required]
        [Display(Name = "Execution Type")]
        public string ExecutionType { get; set; }
        [Required]
        [Display(Name = "Certification Type")]

        public string CertificationType { get; set; }
        [Required]
        [Display(Name = "Certification Name")]
        [MaxLength(200)]
        public string CertificationName { get; set; }
        //Add Relationships
        //Module have many to many relationship with Skill
        //1. Skills Table
        public virtual ICollection<SkillModule> SkillModules { get; set; }
        //2. Batches Table
        public virtual ICollection<Batch> Batches { get; set; }
    }
}