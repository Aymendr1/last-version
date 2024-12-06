namespace Bassma.Models
{
    public class ProduitCommande
    {
		public int Id { get; set; }
		public int CommandeId { get; set; } // Clé étrangère vers Commande
		public int ProduitId { get; set; } // Clé étrangère vers Produit
		public int Quantite { get; set; }

		// Navigation properties
		public Commande Commande { get; set; }
		public Produit Produit { get; set; }


	}
}
