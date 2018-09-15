using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TenBisChallenge.Models
{
    public class Report
    {
        [DisplayName("User Id")]
        public int UserId { get; set; }

        [DisplayName("User Full Name")]
        public string UserFullName { get; set; }

        [DisplayName("PAN")]
        public string Pan { get; set; }

        [DisplayName("Total Volume")]
        public double TotalVolume { get; set; }
    }
}