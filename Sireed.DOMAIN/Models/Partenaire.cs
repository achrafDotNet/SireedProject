namespace Sireed.API.Models
{
    public class Partenaire
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
