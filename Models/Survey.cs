using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Capstone.Web.Models
{
    public class Survey
    {
        public int Id { get; set; }

        public string ParkCode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //[StringLength(2, ErrorMessage ="Please enter two letter state abbreviation (i.e. OH)")]
        public string State { get; set; }

        public string ActivityLevel { get; set; }
    }
}


