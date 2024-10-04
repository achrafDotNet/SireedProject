namespace Sireed.API.Models
{
    public class ProjetRecherche
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public int Annee { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
