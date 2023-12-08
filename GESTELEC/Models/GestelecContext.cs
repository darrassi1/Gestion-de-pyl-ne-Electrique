using System.Data.Entity;

namespace GESTELEC.Models
{
    public class GestelecContext : DbContext
    {
        public GestelecContext() : base("name=GestelecContext")
        {
                this.Configuration.ProxyCreationEnabled = false;
            
        }

        public DbSet<Pylone> Pylones { get; set; }
        public DbSet<Ouvrier> Ouvriers { get; set; }
        
        public DbSet<Paiement> Paiements { get; set; }
        public DbSet<Repos> Repos { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Consommation> Consommations { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add any custom model configuration here
        }
    }
}
