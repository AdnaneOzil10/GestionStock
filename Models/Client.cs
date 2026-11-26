using System.ComponentModel.DataAnnotations;

namespace GestionStock.Models
{
    // 3ème classe ajoutée après la création de la base (migration "AjoutClient")
    public class Client
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Téléphone invalide")]
        [StringLength(20)]
        [Display(Name = "Téléphone")]
        public string? Telephone { get; set; }

        [StringLength(100)]
        public string? Ville { get; set; }

        [Range(0, 120, ErrorMessage = "Âge invalide")]
        [Display(Name = "Âge")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date d'inscription")]
        public DateTime DateInscription { get; set; } = DateTime.Today;
    }
}
