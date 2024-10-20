using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TestCryptoDCA.Models
{
    public class CryptoDCAContext : DbContext
    {
        public CryptoDCAContext(DbContextOptions<CryptoDCAContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<CryptoInvestment> CryptoInvestments { get; set; }

        public DbSet<CryptoDCAResult> DCAResults { get; set; }

    }
}
