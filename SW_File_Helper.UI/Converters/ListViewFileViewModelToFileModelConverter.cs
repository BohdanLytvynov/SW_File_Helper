using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal class ListViewFileViewModelToFileModelConverter : IListViewFileViewModelToFileModelConverter
    {
        public FileModel Convert(ListViewFileViewModel src)
        {
            var fm = new FileModel() { PathToFile = src.FilePath};

            foreach (var item in src.DestFiles)
            {
                if (item.IsEnabled && item.IsValid)
                {
                    fm.PathToDst.Add((item as FileViewModel)!.FilePath);
                }
            }

            return fm;
        }

        public ListViewFileViewModel ReverseConvert(FileModel src)
        {
            var vm = new ListViewFileViewModel() { FilePath = src.PathToFile};

            foreach (var path in src.PathToDst)
            {
                vm.AddFilePath(new FileViewModel() { FilePath = path});
            }

            return vm;
        }
    }
}
