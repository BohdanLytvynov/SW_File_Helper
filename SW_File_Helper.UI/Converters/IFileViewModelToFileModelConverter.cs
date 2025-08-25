using SW_File_Helper.DAL.Models;
using SW_File_Helper.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.Converters
{
    internal interface IFileViewModelToFileModelConverter : IConverter<ListViewFileViewModel, FileModel>
    {
    }
}
