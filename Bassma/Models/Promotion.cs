namespace Bassma.Models
{
    public class Promotion
    {
		public int Id { get; set; }
		public decimal NewPrix { get; set; }
		public int ProduitId { get; set; } // Clé étrangère vers Produit
		public DateTime DateDebut { get; set; }
		public DateTime DateFin { get; set; }

		// Navigation property
		public Produit Produit { get; set; }

		// Relation avec Produit


	}
}
