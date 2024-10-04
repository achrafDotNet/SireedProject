namespace Sireed.API.Models
{
    public class Indicateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public float Valeur { get; set; }
        public int Annee { get; set; }
        public string Type { get; set; }
        public string Unite { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
