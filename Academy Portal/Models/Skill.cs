using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Academy_Portal.Models
{
    public class Skill
    {
        public int SkillID { get; set; }

        [Required(ErrorMessage ="Please provide the Skill Name")]
        [Display(Name="Skill Name")]
        public string SkillName { get; set; }
        //Configure relation ships
        //1. Modules
        public ICollection<SkillModule> SkillModules { get; set; }
        //2. Batches Table
        public ICollection<Batch> Batches { get; set; }

    }
}