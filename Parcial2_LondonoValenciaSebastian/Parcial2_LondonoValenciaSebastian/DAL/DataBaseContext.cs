using Microsoft.EntityFrameworkCore;
using Parcial2_LondonoValenciaSebastian.DAL.Entities;

namespace Parcial2_LondonoValenciaSebastian.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        #region Properties
        public DbSet<NaturalPerson> NaturalsPersons { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NaturalPerson>().HasIndex(s => s.Document).IsUnique();


        }
    }
}
