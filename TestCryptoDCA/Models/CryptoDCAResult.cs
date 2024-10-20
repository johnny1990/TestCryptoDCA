﻿using System.ComponentModel.DataAnnotations;

namespace TestCryptoDCA.Models
{
    public class CryptoDCAResult
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal InvestedAmount { get; set; }
        public decimal CryptoAmount { get; set; }
        public decimal ValueToday { get; set; }
        public decimal ROI { get; set; }
    }
}
