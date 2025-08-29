using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.DataProviders.Base
{
    public interface IDataProvider<TObject>
        where TObject : new()
    {
        JsonSerializerSettings? WriterSettings { get; }
        JsonSerializerSettings? ReaderSettings { get; }

        TObject GetData();

        bool IsDataLoaded();
        string GetPathToFile();
        void LoadData();
        void SaveData();
    }
}
