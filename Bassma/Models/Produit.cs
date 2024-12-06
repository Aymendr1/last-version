namespace Bassma.Models

{
    public class Produit
    {
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Description { get; set; }
		public decimal Prix { get; set; }
        public string ImagePath { get; set; } // Path to the product image
        public int? UserId { get; set; } // Clé étrangère vers User

		// Navigation properties
		public User User { get; set; }
		public ICollection<Commmentaire> Commentaires { get; set; }
		public ICollection<ProduitCommande> ProduitCommandes { get; set; }
		public ICollection<Promotion> Promotions { get; set; }
	}




}
