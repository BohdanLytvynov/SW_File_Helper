using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.DataProviders.Base
{
    public interface IDataProvider
    {
        bool IsDataLoaded();
        string GetPathToFile();
        void LoadData();
        void SaveData();
    }
}
