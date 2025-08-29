using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Repositories.Base;

namespace SW_File_Helper.DAL.Repositories.Favorites
{
    public interface IFavoritesRepository : IRepository<ModelBase, Guid>
    {}
}
