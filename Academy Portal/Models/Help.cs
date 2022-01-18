using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Academy_Portal.Models
{
    public class Help
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public string Issue { get; set; }
        public string Description { get; set; }
        public string ResolutionProvided { get; set; }
        public int ResolutionStatus { get; set; }
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfTicket { get; set; }//Should be auto-populated
    }
}