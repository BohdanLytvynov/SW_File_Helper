using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.BL.FileProcessors
{
    public interface IFileProcessor
    {
        void Process(List<FileModel> fileModels, string newExtension);
    }
}
