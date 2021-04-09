using Microsoft.EntityFrameworkCore;
using OreLavorateLib.Model;
using System.Collections.Generic;

#nullable disable

namespace OreLavorateLib.Context
{
    public partial class OrelavorateContext : DbContext
    {
        public OrelavorateContext()
        {
        }

        public OrelavorateContext(DbContextOptions<OrelavorateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Commessa> Commessas { get; set; }
        public virtual DbSet<OreLavorate> OreLavorates { get; set; }
        public virtual DbSet<Utente> Utentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Commessa>(entity =>
            {
                entity.HasKey(e => e.IdCommessa);

                entity.ToTable("commessa");

                entity.Property(e => e.IdCommessa).HasColumnName("id_commessa");

                entity.Property(e => e.DataFine)
                    .HasColumnType("date")
                    .HasColumnName("data_fine");

                entity.Property(e => e.DataInizio)
                    .HasColumnType("date")
                    .HasColumnName("data_inizio");

                entity.Property(e => e.Titolo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("titolo");
            });

            modelBuilder.Entity<OreLavorate>(entity =>
            {
                entity.HasKey(e => e.IdOreLavorate);

                entity.ToTable("ore_lavorate");

                entity.Property(e => e.IdOreLavorate).HasColumnName("id_ore_lavorate");

                entity.Property(e => e.Data)
                    .HasColumnType("date")
                    .HasColumnName("data");

                entity.Property(e => e.IdCommessa).HasColumnName("id_commessa");

                entity.Property(e => e.NumeroOre).HasColumnName("numero_ore");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdCommessaNavigation)
                    .WithMany(p => p.OreLavorates)
                    .HasForeignKey(d => d.IdCommessa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ore_lavorate_commessa");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.OreLavorates)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ore_lavorate_utente");
            });

            modelBuilder.Entity<Utente>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("utente");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
