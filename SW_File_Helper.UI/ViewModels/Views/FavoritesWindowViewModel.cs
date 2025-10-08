using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.Converters;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.DAL.Repositories.Favorites;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using SW_File_Helper.ViewModels.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Views
{
    internal class FavoritesWindowViewModel : ViewModelBase
    {
        #region Events
        public event Action<List<Guid>> OnFavoritesSelected;
        #endregion

        #region Fields
        private string m_title;

        private string m_typeName;

        private IFavoritesRepository m_favoritesRepository;

        private ObservableCollection<FavoriteBaseViewModel> m_favorites;

        private IFavoriteFileViewModelToDestPathModelConverter m_favoriteFileViewModelToDestPathModelConverter;
        private IFavoriteListViewFileViewModelToFileModelConverter m_favoriteListViewFileViewModelToFileModelConverter;
        #endregion

        #region Properties
        public string Title 
        { get=>m_title; set=>Set(ref m_title, value); }

        public ObservableCollection<FavoriteBaseViewModel> Favorites
        { get=>m_favorites; set=>m_favorites = value; }

        
        #endregion

        #region Commands
        public ICommand OnSelectButtonPressed { get; }

        public ICommand OnDeleteButtonPressed { get; }
        #endregion

        #region Ctor
        public FavoritesWindowViewModel()
        {
            #region Fields
            m_title = "Favorites";

            m_favorites = new ObservableCollection<FavoriteBaseViewModel>();
            var serviceProvider = GlobalServiceWrapper.Services;

            m_favoritesRepository = serviceProvider.GetRequiredService<IFavoritesRepository>();
            m_favoriteFileViewModelToDestPathModelConverter = serviceProvider
                .GetRequiredService<IFavoriteFileViewModelToDestPathModelConverter>();
            m_favoriteListViewFileViewModelToFileModelConverter = serviceProvider
                .GetRequiredService<IFavoriteListViewFileViewModelToFileModelConverter>();

            if (m_favoritesRepository == null)
                throw new ArgumentNullException(nameof(m_favoritesRepository));

            if (m_favoriteFileViewModelToDestPathModelConverter == null)
                throw new ArgumentNullException(nameof(m_favoriteFileViewModelToDestPathModelConverter));

            if (m_favoriteListViewFileViewModelToFileModelConverter == null)
                throw new ArgumentNullException(nameof(m_favoriteListViewFileViewModelToFileModelConverter));

            m_favoritesRepository.LoadData();

            #endregion

            #region Init Commands
            OnSelectButtonPressed = new Command(
                OnSelectButtonPressedExecute,
                CanOnSelectButtonPressedExecute
                );

            OnDeleteButtonPressed = new Command(
                OnDeleteButtonPressedExecute,
                CanOnDeleteButtonPressedExecute
                );
            #endregion
        }
        #endregion

        #region Methods
        public void OnTypeNameSet(string fullTypeName)
        { 
            var arr = fullTypeName.Split('.');
            m_typeName = arr[arr.Length - 1];

            Init();
        }

        private void Init()
        { 
            var favorites = m_favoritesRepository.GetAll();

            IEnumerable<ModelBase> filtered = null;

            if (m_typeName.Equals(nameof(FileViewModel))) //DestPathModel
            {
                filtered = favorites.Where(x => x.GetType().Name == nameof(DestPathModel));
            }
            else if (m_typeName.Equals(nameof(ListViewFileViewModel))) //FileModel
            {
                filtered = favorites.Where(x => x.GetType().Name == nameof(FileModel));
            }
            else
            {
                throw new NotSupportedException($"Unable to determine type! Type was: {m_typeName}");
            }

            foreach (var item in filtered)
            {
                if (m_typeName.Equals(nameof(FileViewModel))) //DestPathModel -> FavoriteFileViewModel
                {
                    var element = m_favoriteFileViewModelToDestPathModelConverter.ReverseConvert((DestPathModel)item);
                    element.Number = Favorites.Count + 1;
                    Favorites.Add(element);
                }
                else if (m_typeName.Equals(nameof(ListViewFileViewModel))) //FileModel -> FavoriteListViewFileViewModel
                {
                    var element = m_favoriteListViewFileViewModelToFileModelConverter.ReverseConvert((FileModel)item);
                    element.Number = Favorites.Count + 1;
                    Favorites.Add(element);
                }
            }
        }

        #region On Select Button Pressed

        private bool CanOnSelectButtonPressedExecute(object p) => CheckSelection();

        private void OnSelectButtonPressedExecute(object p)
        { 
            var ids = Favorites.Where(x => x.IsSelected == true).Select(x => x.Id).ToList();

            OnFavoritesSelected?.Invoke(ids);
        }

        #endregion

        #region On Delete Button Pressed
        private bool CanOnDeleteButtonPressedExecute(object p) => CheckSelection();

        private void OnDeleteButtonPressedExecute(object p)
        {
            var toDelete = Favorites.Where(x => x.IsSelected == true).ToList();

            foreach (var item in toDelete)
            {
                Favorites.Remove(item);

                if (m_typeName.Equals(nameof(FileViewModel))) //DestPathModel -> FavoriteFileViewModel
                {
                    m_favoritesRepository.Delete(m_favoriteFileViewModelToDestPathModelConverter.Convert((FavoriteFileViewModel)item));
                }
                else if (m_typeName.Equals(nameof(ListViewFileViewModel))) //FileModel -> FavoriteListViewFileViewModel
                {
                    m_favoritesRepository.Delete(m_favoriteListViewFileViewModelToFileModelConverter.Convert((FavoriteListViewFileViewModel)item));
                }
            }
        }

        private bool CheckSelection()
        { 
            return Favorites.Where(x => x.IsSelected).Any();
        }
        #endregion

        #endregion
    }
}
