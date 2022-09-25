using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Context
{
    public class SeckillContext : DbContext
    {
        public SeckillContext(DbContextOptions<SeckillContext> options) : base(options)
        {
        }

        public DbSet<Seckill> Seckills { set; get; }
        public DbSet<SeckillRecord> SeckillRecords { set; get; }
        public DbSet<SeckillTime> SeckillTimes { get; set; }
    }
}