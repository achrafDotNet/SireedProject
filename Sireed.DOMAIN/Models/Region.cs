using System.Reflection.Metadata;

namespace Sireed.API.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public float Superficie { get; set; }
        public int Population { get; set; }

        public ICollection<Indicateur> Indicateurs { get; set; }
        public ICollection<ProjetRecherche> ProjetsRecherche { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Partenaire> Partenaires { get; set; }
        public ICollection<Actualite> Actualites { get; set; }
    }
}
