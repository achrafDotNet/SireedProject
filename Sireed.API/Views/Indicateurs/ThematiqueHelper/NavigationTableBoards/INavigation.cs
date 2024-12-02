using Sireed.APPLICATION.DTO;

namespace Sireed.API.Views.Indicateurs.ThematiqueHelper.NavigationTableBoards
{
    public interface INavigation
    {
        Task GetAllThematiques(ThematiqueDTO thematique);
        
    }
}
