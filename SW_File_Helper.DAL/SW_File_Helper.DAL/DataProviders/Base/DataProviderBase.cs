using SW_File_Helper.DAL.Helpers;

namespace SW_File_Helper.DAL.DataProviders.Base
{
    public abstract class DataProviderBase : IDataProvider
    {
        private string m_pathToFie;
        private bool m_dataLoaded;

        protected string PathToFile { get => m_pathToFie; set => m_pathToFie = value; }
        protected bool DataLoaded { get=>m_dataLoaded; set => m_dataLoaded = value; }

        protected DataProviderBase()
        {
            m_dataLoaded = false;
            string pathToExe = Environment.CurrentDirectory;
            string pathToDataFolder = pathToExe + Path.DirectorySeparatorChar + "Data";
            IOHelper.CreateDirectoryIfNotExists(pathToDataFolder);
            m_pathToFie = pathToDataFolder;
        }

        public abstract void LoadData();

        public abstract void SaveData();

        public bool IsDataLoaded()
        {
            return m_dataLoaded;
        }

        public string GetPathToFile()
        {
            return m_pathToFie;
        }
    }
}
