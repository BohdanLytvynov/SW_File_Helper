using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.BL.FileProcessors
{
    public interface IFileProcessor
    {
        void Process(List<FileModel> fileModels);
    }
}
