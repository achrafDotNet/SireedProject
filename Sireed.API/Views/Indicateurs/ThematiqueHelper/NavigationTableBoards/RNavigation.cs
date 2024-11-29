namespace Sireed.API.Views.Indicateurs.ThematiqueHelper.NavigationTableBoards
{
    public class RNavigation : INavigation
    {

        private readonly ThematiquesHelpersController _thematiques;
        public RNavigation(ThematiquesHelpersController thematiques) {
        
            _thematiques = thematiques;
        
        }

        public void Agricultureetindustrie()
        {
            _thematiques.Agricultureetindustrie();
        }

        public void Air()
        {
            _thematiques.Air();
        }

        public void Dechets()
        {
            _thematiques.Dechets();
        }

        public void Eauetassainissement()
        {
            _thematiques.Eauetassainissement();
        }

        public void Littoraletbiodiversite()
        {
            _thematiques.Littoraletbiodiversite();
        }

        public void Populationeteducationenvironnementale()
        {
            _thematiques.Populationeteducationenvironnementale();
        }
    }
}
