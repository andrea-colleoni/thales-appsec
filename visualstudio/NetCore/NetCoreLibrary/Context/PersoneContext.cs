using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Model;
using System.Threading.Tasks;

namespace NetCoreLibrary.Context
{
    public class PersoneContext : IdentityDbContext
    {

        public PersoneContext()
        {

        }

        public PersoneContext(DbContextOptions<PersoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Persone { get; set; }

        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        // questo metodo viene eseguito se non ricevo una configurazione dalla dependency injection

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive information in your connection string, 
                // you should move it out of source code. You can avoid scaffolding the connection string by using the
                // Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=20210317-persone;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .HasKey(p => p.Email);

            modelBuilder.Entity<Persona>()
                .Property("Nome")
                .HasMaxLength(100);

            modelBuilder.Entity<Persona>()
                .Property("Cognome")
                .HasMaxLength(100);
        }
    }
}
