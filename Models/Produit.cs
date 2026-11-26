using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionStock.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(150)]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0, 1000000, ErrorMessage = "Prix invalide")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prix { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantité invalide")]
        [Display(Name = "Quantité")]
        public int Quantite { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date d'ajout")]
        public DateTime DateAjout { get; set; } = DateTime.Today;

        // Clé étrangère vers Categorie
        [Required(ErrorMessage = "La catégorie est obligatoire")]
        [Display(Name = "Catégorie")]
        public int CategorieId { get; set; }

        public Categorie? Categorie { get; set; }
    }
}
