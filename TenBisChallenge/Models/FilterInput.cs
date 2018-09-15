using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenBisChallenge.Models
{
    public class FilterInput
    {
        [Required]
        [Range(0, int.MaxValue)]
        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        public FilterInput()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }
    }
}