using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal class FileViewModelToDestPathModelConverter
        : IFileViewModelToDestPathModelConverter
    {
        public DestPathModel Convert(FileViewModel src)
        {
            return new DestPathModel() { PathToFile = src.FilePath };
        }

        public FileViewModel ReverseConvert(DestPathModel src)
        {
            return new FileViewModel() { FilePath = src.PathToFile };
        }
    }
}
