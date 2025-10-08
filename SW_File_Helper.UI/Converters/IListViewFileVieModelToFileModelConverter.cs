using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;

namespace SW_File_Helper.Converters
{
    internal interface IListViewFileVieModelToFileModelConverter 
        : IConverter<ListViewFileViewModel, FileModel>
    {
    }
}
