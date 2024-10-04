namespace Sireed.API.Models
{
    public class Actualite
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DatePublication { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
