using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal class ListViewFileViewModelToFavoriteListViewFileViewModelConverter
        : IListViewFileViewModelToFavoriteListViewFileViewModelConverter
    {
        public FavoriteListViewFileViewModel Convert(ListViewFileViewModel src)
        {
            FavoriteListViewFileViewModel favoriteListViewFileViewModel = new FavoriteListViewFileViewModel()
            { 
                Path = src.FilePath
            };

            foreach (var item in src.DestFiles)
            {
                favoriteListViewFileViewModel.AddPath(new FavoriteFileViewModel() 
                { Path = (item as FileViewModel)!.FilePath });
            }

            return favoriteListViewFileViewModel;
        }

        public ListViewFileViewModel ReverseConvert(FavoriteListViewFileViewModel src)
        {
            ListViewFileViewModel listViewFileViewModel = new ListViewFileViewModel()
            { FilePath = src.Path };

            foreach (var item in src.DestPathes)
            {
                listViewFileViewModel.AddFilePath(new FileViewModel() { FilePath = item.Path });
            }

            return listViewFileViewModel;
        }
    }
}
