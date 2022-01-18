using Academy_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy_Portal.Views.ViewModels
{
    public class FacultyDetailsViewModel
    {
        public ICollection<ApplicationUser> Faculty { get; set; }
    }
}