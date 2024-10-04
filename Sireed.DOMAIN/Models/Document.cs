namespace Sireed.API.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Type { get; set; }
        public string FichierURL { get; set; }
        public DateTime DatePublication { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
