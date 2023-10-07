using Microsoft.EntityFrameworkCore;
using Parcial2_LondonoValenciaSebastian.DAL.Entities;

namespace Parcial2_LondonoValenciaSebastian.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        //Aquí estoy mapeando mi entidad para convertirla en un DBSet (tabla)
        #region Properties
        public DbSet<NaturalPerson> NaturalsPersons { get; set; }
        #endregion

        //Se crean los indices de las tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NaturalPerson>().HasIndex(s => s.Document).IsUnique();

        }
    }
}
