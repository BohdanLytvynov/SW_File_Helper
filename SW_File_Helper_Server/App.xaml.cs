using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.BL.Net.MessageProcessors.MessageProcessor;
using SW_File_Helper.BL.Net.MessageProcessors.ProcessFilesCommandProcessors;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.MessageProcessors;
using SW_File_Helper.BL.Net.TCPMessageListener;
using SW_File_Helper.Loggers;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper_Server.ViewModels;
using System.Windows;

namespace SW_File_Helper_Server
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
            services.AddSingleton<IConsoleLogger, ConsoleLogger>();
            services.AddSingleton<MainWindow>(c =>
            {
                var vm = c.GetRequiredService<MainWindowViewModel>();
                var mainWindow = new MainWindow();

                mainWindow.DataContext = vm;
                vm.Dispatcher = mainWindow.Dispatcher;

                return mainWindow;
            });

            #endregion

            return services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //var SettingsDataProvider = m_serviceProvider.GetService<ISettingsDataProvider>();

            //SettingsDataProvider.SaveData();

            base.OnExit(e);
        }
    }

}
