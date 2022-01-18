using Academy_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy_Portal.Views.ViewModels
{
    public class SkillModuleViewModel
    {
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Module> Modules { get; set; }
        public SkillModule SkillModule { get; set; }
    }
}