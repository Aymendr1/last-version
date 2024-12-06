namespace Bassma.Models
{
    public class Livraison
    {
		public int Id { get; set; }
		public String UserId { get; set; } // Clé étrangère vers User
		public int CommandeId { get; set; } // Clé étrangère vers Commande
		public DateTime Date { get; set; }
		public string Status { get; set; }

		// Navigation properties
		public User User { get; set; }
		public Commande Commande { get; set; }

	}

}
