using System.ComponentModel.DataAnnotations;

namespace Bassma.Models
{
    public class Commande
        
    {

		public int Id { get; set; }
		public decimal Total { get; set; }
		public int UserId { get; set; } // Clé étrangère vers User
		public string Adresse { get; set; }

		// Navigation properties
		public User User { get; set; }
		public ICollection<ProduitCommande> ProduitCommandes { get; set; }
		public Paiement Paiement { get; set; }
		public Livraison Livraison { get; set; }


	}
}
