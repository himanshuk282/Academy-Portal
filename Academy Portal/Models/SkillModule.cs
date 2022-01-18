using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy_Portal.Models
{
    public class SkillModule
    {
        public int SkillID { get; set; }
        public int ModuleID { get; set; }
        public Skill Skill { get; set; }
        public Module Module { get; set; }
    }
}