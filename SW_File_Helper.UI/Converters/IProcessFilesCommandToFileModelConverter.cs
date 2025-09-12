using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Models.TCPModels;

namespace SW_File_Helper.Converters
{
    public interface IProcessFilesCommandToFileModelConverter : IConverter<ProcessFilesCommand, FileModel>
    {
    }
}
