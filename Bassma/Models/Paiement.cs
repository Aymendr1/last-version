namespace Bassma.Models
{
    public class Paiement
    {

		public int Id { get; set; }
		public int CommandeId { get; set; } // Clé étrangère vers Commande
		public string Status { get; set; }

		// Navigation property
		public Commande Commande { get; set; }
	}
}
