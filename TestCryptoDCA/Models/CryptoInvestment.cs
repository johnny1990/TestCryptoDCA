using System.ComponentModel.DataAnnotations;

namespace TestCryptoDCA.Models
{
    public class CryptoInvestment
    {
        [Key]
        public int Id { get; set; }
      
        public DateTime StartDate { get; set; }

        public decimal MonthlyAmount { get; set; }

        public string CryptoSymbol { get; set; }
    }
}
