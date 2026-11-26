using System.ComponentModel.DataAnnotations;

namespace GestionStock.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Relation 1 -> N : une catégorie contient plusieurs produits
        public List<Produit> Produits { get; set; } = new();
    }
}
