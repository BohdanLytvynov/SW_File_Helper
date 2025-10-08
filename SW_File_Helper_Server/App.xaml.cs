using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.BL.FileProcessors;
using SW_File_Helper.Loggers;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper_Server.ViewModels;
using SW_File_Helper.Converters;
using System.Windows;
using SW_File_Helper.BL.Net.TCPListeners;
using SW_File_Helper.BL.Net.NetworkStreamProcessorWrappers.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.Base;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.MesssageStreamProcessor;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.CommandStreamProcessors;
using SW_File_Helper.BL.Net.NetworkStreamProcessors.FileStreamProcessors;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using SW_File_Helper.DAL.DataProviders.ServerSetting;

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
            services.AddSingleton<IServerSettingsDataProvider, ServerSettingsDataProvider>();
            services.AddSingleton<IProcessFilesCommandToFileModelConverter, ProcessFilesCommandToFileModelConverter>();
            services.AddSingleton<ServiceWrapper>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IConsoleLogger, ConsoleLogger>();
            services.AddSingleton<IFileProcessor>(c =>
            {
                var logger = c.GetRequiredService<IConsoleLogger>();
                var pathToTemp = ConfigurationManager.AppSettings["PathToTemp"];
                return new FileProcessor(logger, pathToTemp);
            });
            services.AddSingleton<IMessageStreamProcessor>(c => 
            {
                var logger = c.GetRequiredService<IConsoleLogger>();
                return new MessageStreamProcessor(logger);
            });
            services.AddSingleton<ICommandStreamProcessor>( c =>
            {
                var logger = c.GetRequiredService<IConsoleLogger>();
                return new CommandStreamProcessor(logger);
            });
            services.AddSingleton<IFileStreamProcessor>(c => 
            {
                var logger = c.GetRequiredService<IConsoleLogger>();
                IFileStreamProcessor fileStreamProcessor = new FileStreamProcessor(logger, Environment.CurrentDirectory + 
                    Path.DirectorySeparatorChar + ConfigurationManager.AppSettings["PathToTemp"]);
                return fileStreamProcessor;
            });
            services.AddSingleton<INetworkStreamProcessorWrapper>(c => 
            {
                var logger = c.GetRequiredService<IConsoleLogger>();

                INetworkStreamProcessor msgProcessor = c.GetRequiredService<IMessageStreamProcessor>();
                INetworkStreamProcessor commandProcessor = c.GetRequiredService<ICommandStreamProcessor>();
                msgProcessor.Next = commandProcessor;
                IFileStreamProcessor fileProcessor = c.GetRequiredService<IFileStreamProcessor>();
                commandProcessor.Next = fileProcessor;

                INetworkStreamProcessorWrapper networkStreamProcessorWrapper = 
                new NetworkStreamProcessorWrapper(logger, msgProcessor);

                return networkStreamProcessorWrapper;
            });
            services.AddSingleton<ITCPListener>(c =>
            {
                var logger = c.GetRequiredService<IConsoleLogger>();
                var networkStreamProcessorWrapper = c.GetRequiredService<INetworkStreamProcessorWrapper>();

                ITCPListener tCPListener = new TCPListener(logger, networkStreamProcessorWrapper, "TCP Listener");
                return tCPListener;
            });
            services.AddSingleton(c => 
            {
                var logger = c.GetRequiredService<IConsoleLogger>();

                return new FileProcessor(logger);
            });
            services.AddSingleton<MainWindow>(c =>
            {
                var vm = c.GetRequiredService<MainWindowViewModel>();
                var mainWindow = new MainWindow();
                mainWindow.Closing += (object s, CancelEventArgs e) => {
                    vm.StopServers();
                };

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
            base.OnExit(e);
        }
    }

}
