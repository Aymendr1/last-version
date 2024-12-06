using Bassma.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bassma.Data
{
    public class BakeryDbContext : IdentityDbContext<User>
    {
        public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
            : base(options)
        { }

        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commmentaire> Commentaires { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<ProduitCommande> ProduitCommandes { get; set; }
        public DbSet<Paiement> Paiments { get; set; }
        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Produit
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nom).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Prix).HasPrecision(18, 2);
            });

            // Configure Commmentaire
            modelBuilder.Entity<Commmentaire>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Description).IsRequired().HasMaxLength(500);
            });

            // Configure Commande
            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Adresse).IsRequired().HasMaxLength(500);
                entity.Property(c => c.Total).HasPrecision(18, 2);
            });

            // Configure ProduitCommande
            modelBuilder.Entity<ProduitCommande>(entity =>
            {
                entity.HasKey(pc => pc.Id);
                entity.HasOne(pc => pc.Commande)
                      .WithMany(c => c.ProduitCommandes)
                      .HasForeignKey(pc => pc.CommandeId);
                entity.HasOne(pc => pc.Produit)
                      .WithMany(p => p.ProduitCommandes)
                      .HasForeignKey(pc => pc.ProduitId);
            });

            // Configure Paiement
            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Commande)
                      .WithOne(c => c.Paiement)
                      .HasForeignKey<Paiement>(p => p.CommandeId);
            });

            // Configure Livraison
            modelBuilder.Entity<Livraison>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.HasOne(l => l.User)
                      .WithMany(u => u.Livraisons)
                      .HasForeignKey(l => l.UserId);
                entity.HasOne(l => l.Commande)
                      .WithOne(c => c.Livraison)
                      .HasForeignKey<Livraison>(l => l.CommandeId);
            });

            // Configure Promotion
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Produit)
                      .WithMany(pr => pr.Promotions)
                      .HasForeignKey(p => p.ProduitId);
            });
        }
    }
}
