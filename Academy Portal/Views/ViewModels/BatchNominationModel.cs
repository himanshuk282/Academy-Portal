using Academy_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Academy_Portal.Views.ViewModels
{
    public class BatchNominationModel
    {
        public FacultyUser Faculty {get; set; }
        public Batch Batch{ get; set; }
        //Didn't add ambiguous property of Proficiency
    }
}