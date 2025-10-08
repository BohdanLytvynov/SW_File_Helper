using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.Converters;
using SW_File_Helper.DAL.DataProviders.Favorites;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Repositories.Favorites;
using SW_File_Helper.Interfaces;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper.ViewModels.Views;
using SW_File_Helper.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SW_File_Helper.Controls.EditableListView;

namespace SW_File_Helper.ViewModels.Models
{
    public sealed class ListViewFileViewModel : FileViewModel, IManualDrawable
    {
        #region Fields
        ObservableCollection<CustomListViewItem> m_destFiles;

        private bool m_ManualDraw;

        private string m_CurrentAssembly;

        private string m_FileViewModelTypeName;

        private OnShowFavoritesFired m_OnShowFavoritesFired;

        private OnAddToFavoritesFired m_OnAddToFavoritesFired;

        private IFavoritesRepository m_favoritesRepository;

        private IFileViewModelToDestPathModelConverter m_FileViewModelToDestPathModelConverter;

        #endregion

        #region Properties
        public bool ManualDraw 
        { get => m_ManualDraw; set => Set(ref m_ManualDraw, value); }

        public string CurrentAssembly
        { get=>m_CurrentAssembly; set=>Set(ref m_CurrentAssembly, value); }

        public string FileViewModelTypeName
        { get => m_FileViewModelTypeName; set => Set(ref m_FileViewModelTypeName, value); }

        public ObservableCollection<CustomListViewItem> DestFiles
        {
            get=>m_destFiles; set=>m_destFiles = value;
        }

        public OnShowFavoritesFired ShowFavoritesFired
        { get => m_OnShowFavoritesFired; set => Set(ref m_OnShowFavoritesFired, value); }

        public OnAddToFavoritesFired AddToFavoritesFired
        { get => m_OnAddToFavoritesFired; set => Set(ref m_OnAddToFavoritesFired, value); }
        #endregion

        #region Ctor
        public ListViewFileViewModel() : base()
        {
            Init();
        }

        #endregion

        #region Methods
        private void Init() 
        {
            m_ManualDraw = false;
            m_favoritesRepository = GlobalServiceWrapper
                .Services.GetRequiredService<IFavoritesRepository>();
            m_FileViewModelToDestPathModelConverter = GlobalServiceWrapper
                .Services.GetRequiredService<IFileViewModelToDestPathModelConverter>();

            m_destFiles = new ObservableCollection<CustomListViewItem>();

            m_CurrentAssembly = Assembly.GetEntryAssembly().Location;
            m_FileViewModelTypeName = typeof(FileViewModel).FullName;
            
            m_OnAddToFavoritesFired = new OnAddToFavoritesFired(items =>
            {
                foreach (var item in items)
                {
                    m_favoritesRepository.Add(m_FileViewModelToDestPathModelConverter.Convert(item as FileViewModel));
                }
            });

            m_OnShowFavoritesFired = new OnShowFavoritesFired(favoritesType =>
            {
                var favoritesWindow = GlobalServiceWrapper.Services.GetRequiredService<FavoritesWindow>();
                favoritesWindow.OnFavoritesSelected += FavoritesWindow_OnFavoritesSelected;
                favoritesWindow.Topmost = true;
                favoritesWindow.SetTypeName(favoritesType);
                favoritesWindow.ShowDialog();
            });
        }

        private void FavoritesWindow_OnFavoritesSelected(List<Guid> ids)
        {
            var files = m_favoritesRepository.GetAll(ids).ToList();

            foreach (var file in files)
            {
                AddFilePath(m_FileViewModelToDestPathModelConverter.ReverseConvert((DestPathModel)file));
            }

            Draw();
        }

        public void Validate()
        { 
            bool destFilesAreValid = true;

            foreach (var item in m_destFiles)
            {
                if (!item.IsValid)
                { 
                    destFilesAreValid = false;
                    break;
                }
            }

            IsValid = destFilesAreValid;
        }

        public void AddFilePath(CustomListViewItem item)
        { 
            if(item == null) throw new ArgumentNullException(nameof(item));
            item.Number = DestFiles.Count + 1;
            DestFiles.Add(item);
        }

        public void Draw()
        {
            ManualDraw = !m_ManualDraw;
        }
        #endregion
    }
}
