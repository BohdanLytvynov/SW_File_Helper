using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.BL.FileProcessors;
using SW_File_Helper.BL.Net.TCPClients;
using SW_File_Helper.Converters;
using SW_File_Helper.DAL.DataProviders.Favorites;
using SW_File_Helper.DAL.DataProviders.Settings;
using SW_File_Helper.DAL.Repositories.Favorites;
using SW_File_Helper.Loggers;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper.ViewModels.Views;
using SW_File_Helper.ViewModels.Views.Pages;
using SW_File_Helper.Views;
using SW_File_Helper.Views.Pages;
using System.Windows;

namespace SW_File_Helper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceProvider m_serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (m_serviceProvider == null)
                    m_serviceProvider = Init().BuildServiceProvider();

                return m_serviceProvider;
            }
        }

        private static IServiceCollection Init()
        {
            var services = new ServiceCollection();

            #region Do Initial Setup Here
            services.AddSingleton<ServiceWrapper>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IFavoritesDataProvider, FavoritesDataProvider>();
            services.AddSingleton<IFavoritesRepository, FavoritesRepository>();
            services.AddSingleton<IFavoriteListViewFileViewModelToFileModelConverter, FavoriteListViewFileViewModelToFileModelConverter>();
            services.AddSingleton<IFavoriteFileViewModelToDestPathModelConverter, FavoriteFileViewModelToDestPathModelConverter>();
            services.AddSingleton<IListViewFileViewModelToFileModelConverter, ListViewFileViewModelToFileModelConverter>();
            services.AddSingleton<IListViewFileViewModelToFavoriteListViewFileViewModelConverter, ListViewFileViewModelToFavoriteListViewFileViewModelConverter>();
            services.AddSingleton<IFileViewModelToFavoriteFileViewModelConverter, FileViewModelToFavoriteFileViewModelConverter>();
            services.AddSingleton<IFileViewModelToDestPathModelConverter, FileViewModelToDestPathModelConverter>();
            services.AddSingleton<IFileProcessor, FileProcessor>();
            services.AddSingleton<IConsoleLogger, ConsoleLogger>();
            services.AddSingleton<ISettingsDataProvider, SettingsDataProvider>();
            services.AddSingleton<SettingsPageViewModel>();

            services.AddScoped<FavoritesWindowViewModel>();
            services.AddSingleton(config =>
            {
                var vm = config.GetRequiredService<SettingsPageViewModel>();
                var settingsPage = new SettingsPage();

                settingsPage.DataContext = vm;
                vm.Dispatcher = settingsPage.Dispatcher;

                return settingsPage;

            });

            services.AddSingleton(c =>
            {
                var vm = c.GetRequiredService<MainWindowViewModel>();
                var mainWindow = new MainWindow();
                vm.WindowClosed += (sender, args) =>
                {
                    mainWindow.Close();
                };

                mainWindow.DataContext = vm;
                vm.Dispatcher = mainWindow.Dispatcher;

                return mainWindow;
            });

            services.AddTransient(c =>
            {
                var scope = c.CreateScope();

                var vm = scope.ServiceProvider.GetRequiredService<FavoritesWindowViewModel>();
                var favoritesWindow = new FavoritesWindow();
                favoritesWindow.DataContext = vm;
                vm.Dispatcher = favoritesWindow.Dispatcher;
                favoritesWindow.OnTypeNameSet += vm.OnTypeNameSet;
                vm.OnFavoritesSelected += favoritesWindow.FavoritesSelected;

                favoritesWindow.Closed += (object sender, EventArgs e) =>
                {
                    scope.Dispose();
                };
                return favoritesWindow;
            });

            #endregion

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            GlobalServiceWrapper.Services = ServiceProvider;
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var SettingsDataProvider = m_serviceProvider.GetService<ISettingsDataProvider>();

            SettingsDataProvider.SaveData();

            base.OnExit(e);
        }
    }

}
