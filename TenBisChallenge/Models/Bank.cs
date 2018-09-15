using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenBisChallenge.Models
{
    public class Bank
    {
        [Key]
        public int TxId { get; set; }
        public DateTime OrderTime { get; set; }
        public double TxValue { get; set; }
        public int MoneyCardId { get; set; }

        public MoneyCard MoneyCard { get; set; }
    }
}