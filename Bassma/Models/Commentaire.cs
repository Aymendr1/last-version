namespace Bassma.Models
{
	public class Commmentaire
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public int UserId { get; set; } // Clé étrangère vers User
		public int ProduitId { get; set; } // Clé étrangère vers Produit

		// Navigation properties
		public User User { get; set; }
		public Produit Produit { get; set; }
	}
}
