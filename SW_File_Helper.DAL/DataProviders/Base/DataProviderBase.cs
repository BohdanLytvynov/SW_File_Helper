using Newtonsoft.Json;
using SW_File_Helper.DAL.Helpers;
using System.Diagnostics;

namespace SW_File_Helper.DAL.DataProviders.Base
{
    public abstract class DataProviderBase<TObject> : IDataProvider<TObject>
        where TObject : new()
    {
        public JsonSerializerSettings? ReaderSettings { get; protected set; }
        public JsonSerializerSettings? WriterSettings { get; protected set; }

        public virtual TObject GetData()
        {
            if (!IsDataLoaded())
                LoadData();

            return m_data;
        }

        protected TObject m_data;

        private string m_pathToFie;

        protected string PathToFile { get => m_pathToFie; set => m_pathToFie = value; }

        public DataProviderBase()
        {
            string pathToExe = Environment.CurrentDirectory;
            string pathToDataFolder = pathToExe + Path.DirectorySeparatorChar + "Data";
            IOHelper.CreateDirectoryIfNotExists(pathToDataFolder);
            m_pathToFie = pathToDataFolder;
        }

        public void LoadData()
        {
            try
            {
                string value = IOHelper.ReadAll(PathToFile);
                if (!string.IsNullOrEmpty(value))
                {
                    m_data = JsonHelper.DeSerialize(value,
                        new TObject(),
                        ReaderSettings);
                }
                else
                {
                    m_data = new TObject();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"Fail to Load {m_data.GetType().Name}! Error: " + ex.Message);
#endif
            }
        }

        public void SaveData()
        {
            try
            {
                string jsonStr = JsonHelper.Serialize(
                    m_data ?? throw new ArgumentNullException(nameof(m_data)),
                    WriterSettings);

                IOHelper.WriteAll(PathToFile, jsonStr);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Fail to save Favorites! Error: " + ex.Message);
#endif
            }
        }

        public bool IsDataLoaded()
        {
            return m_data != null;
        }

        public string GetPathToFile()
        {
            return m_pathToFie;
        }
    }
}
