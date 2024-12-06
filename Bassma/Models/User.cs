using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bassma.Models
{
	public class User : IdentityUser
	{
		public string ?Role { get; set; } // Le "?" indique que Role est nullable
		public ICollection<Produit> Produits { get; set; }
		public ICollection<Commmentaire> Commentaires { get; set; }
		public ICollection<Commande> Commandes { get; set; }
		public ICollection<Livraison> Livraisons { get; set; }

	}
}
