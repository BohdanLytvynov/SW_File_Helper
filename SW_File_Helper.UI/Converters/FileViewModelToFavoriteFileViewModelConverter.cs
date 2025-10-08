using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal class FileViewModelToFavoriteFileViewModelConverter
        : IFileViewModelToFavoriteFileViewModelConverter
    {
        public FavoriteFileViewModel Convert(FileViewModel src)
        {
            return new FavoriteFileViewModel(){ Path = src.FilePath };
        }

        public FileViewModel ReverseConvert(FavoriteFileViewModel src)
        {
            return new FileViewModel() { FilePath = src.Path };
        }
    }
}
