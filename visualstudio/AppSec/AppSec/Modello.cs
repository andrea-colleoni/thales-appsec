using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AppSec
{
    public partial class Modello : DbContext
    {
        public Modello()
            : base("name=Modello")
        {
        }

        public virtual DbSet<utente> utente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<utente>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<utente>()
                .Property(e => e.cognome)
                .IsUnicode(false);

            modelBuilder.Entity<utente>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<utente>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
