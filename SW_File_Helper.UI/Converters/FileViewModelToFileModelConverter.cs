using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.Converters
{
    internal class FileViewModelToFileModelConverter : IFileViewModelToFileModelConverter
    {
        public FileModel Convert(ListViewFileViewModel src)
        {
            var fm = new FileModel() { PathToFile = src.FilePath };

            foreach (var item in src.DestFiles)
            {
                if (item.IsEnabled && item.IsValid)
                {
                    fm.PathToDst.Add((item as FileViewModel).FilePath);
                }
            }

            return fm;
        }

        public ListViewFileViewModel ReverseConvert(FileModel src)
        {
            var vm = new ListViewFileViewModel() { FilePath = src.PathToFile};

            foreach (var path in src.PathToDst)
            {
                vm.DestFiles.Add(new FileViewModel() { FilePath = path, Number = vm.DestFiles.Count + 1 });
            }

            return vm;
        }
    }
}
