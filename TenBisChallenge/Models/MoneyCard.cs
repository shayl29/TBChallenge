using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenBisChallenge.Models
{
    public class MoneyCard
    {
        public int MoneyCardId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string LastFourDigits { get; set; }

        public Company Company { get; set; }
        public User User { get; set; }
    }
}