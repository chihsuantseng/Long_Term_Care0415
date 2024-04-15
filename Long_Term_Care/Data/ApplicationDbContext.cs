using Long_Term_Care.Models;
using Microsoft.EntityFrameworkCore;
namespace Long_Term_Care.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //set Db Physicals
        public DbSet<Physicals> Physical { get; set; }
        public object Physicals { get; internal set; }

        //set Db HealthSupplement
        public DbSet<HealthSupplement> healthSupplements { get; set; }
        public object HealthSupplements { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Physicals>().HasData(
                new Physicals { PhysicalId = 1, Height = 176, Weight = 70, SBP = 123, DBP = 77, Waist = 78, WBC = 10.32F, RBC = 4.6F, HB = 12.7F, HCT = 48.1F, PLT = 267, HDL = 100, LDL = 230, TG = 105, CHOL = 120, ALT = 24, AST = 26, CREA = 0.8F, BUN = 21, Ca = 2.2F, HbA1C = 3.9F, GLU = 90, UIBC = 223, Fe = 201 }
                );

            //modelBuilder.Entity<HealthSupplement>().HasData(
            //    new HealthSupplements {}
            //    );
        }



        

        
    }
}
