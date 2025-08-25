using SW_File_Helper.DAL.DataProviders.Base;
using SW_File_Helper.DAL.Helpers;
using SW_File_Helper.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_File_Helper.DAL.DataProviders.Settings
{
    public sealed class SettingsDataProvider : DataProviderBase, ISettingsDataProvider
    {
        public Models.Settings? Settings { get; set; }

        #region Ctor
        public SettingsDataProvider() : base()
        {
            PathToFile += Path.DirectorySeparatorChar + "Settings.json";
            IOHelper.CreateFileIfNotExists(PathToFile);

            if(!IsDataLoaded())
                LoadData();
        }
        #endregion

        #region Methods
        public override void LoadData()
        {
            try
            {
                string value = IOHelper.ReadAll(PathToFile);
                if (!string.IsNullOrEmpty(value))
                {
                    Settings = JsonHelper.DeSerialize<Models.Settings>(value, Settings);
                    DataLoaded = true;
                }
                else
                { 
                    Settings = new Models.Settings();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Fail to Load Data! Error: " + ex.Message);
#endif
            }
        }

        public override void SaveData()
        {
            try
            {
                string jsonStr = JsonHelper.Serialize(Settings ?? throw new ArgumentNullException(nameof(Settings)));
                IOHelper.WriteAll(PathToFile, jsonStr);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine("Fail to save data! Error: " + ex.Message);
#endif
            }
        }
        #endregion
    }
}
