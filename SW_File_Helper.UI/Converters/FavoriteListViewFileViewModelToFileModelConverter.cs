using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal class FavoriteListViewFileViewModelToFileModelConverter : IFavoriteListViewFileViewModelToFileModelConverter
    {
        public FileModel Convert(FavoriteListViewFileViewModel src)
        {
            FileModel fileModel = new FileModel() { Id = src.Id, PathToFile = src.Path};

            foreach (var path in src.DestPathes)
            {
                fileModel.PathToDst.Add(path.Path);
            }

            return fileModel;
        }

        public FavoriteListViewFileViewModel ReverseConvert(FileModel src)
        {
            FavoriteListViewFileViewModel favoriteListViewFileViewModel = new FavoriteListViewFileViewModel()
            { Id = src.Id, Path = src.PathToFile };

            foreach (var p in src.PathToDst)
            {
                favoriteListViewFileViewModel.AddPath(new FavoriteFileViewModel() { Path = p });
            }

            return favoriteListViewFileViewModel;
        }
    }
}
