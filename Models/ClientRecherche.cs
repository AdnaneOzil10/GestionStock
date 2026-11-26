using System.ComponentModel.DataAnnotations;

namespace GestionStock.Models
{
    // Critères de la recherche avancée (un critère par attribut de Client)
    public class ClientRecherche
    {
        public int? Id { get; set; }
        public string? Nom { get; set; }
        [Display(Name = "Prénom")]
        public string? Prenom { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Téléphone")]
        public string? Telephone { get; set; }
        public string? Ville { get; set; }
        [Display(Name = "Âge min")]
        public int? AgeMin { get; set; }
        [Display(Name = "Âge max")]
        public int? AgeMax { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Inscrit du")]
        public DateTime? DateDebut { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "au")]
        public DateTime? DateFin { get; set; }

        public bool EstVide =>
            Id == null && string.IsNullOrWhiteSpace(Nom) && string.IsNullOrWhiteSpace(Prenom)
            && string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Telephone)
            && string.IsNullOrWhiteSpace(Ville) && AgeMin == null && AgeMax == null
            && DateDebut == null && DateFin == null;

        // Résultats
        public List<Client> Resultats { get; set; } = new();
    }
}
