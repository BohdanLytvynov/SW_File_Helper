using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal class FavoriteFileViewModelToDestPathModelConverter : IFavoriteFileViewModelToDestPathModelConverter
    {
        public DestPathModel Convert(FavoriteFileViewModel src)
        {
            return new DestPathModel() { Id = src.Id, PathToFile = src.Path };
        }

        public FavoriteFileViewModel ReverseConvert(DestPathModel src)
        {
            return new FavoriteFileViewModel() { Id = src.Id, Path = src.PathToFile };
        }
    }
}
