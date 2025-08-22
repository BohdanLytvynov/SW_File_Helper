using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.ViewModels.Views;
using System.Configuration;
using System.Data;
using System.Reflection;
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
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton(c =>
            {
                var vm = c.GetRequiredService<MainWindowViewModel>();
                var mainwindow = new MainWindow();

                mainwindow.DataContext = vm;
                vm.Dispatcher = mainwindow.Dispatcher;

                return mainwindow;
            });
            
            //Mapper configuration
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    var assembly = Assembly.GetExecutingAssembly();

            //    var profiles = assembly.DefinedTypes.Where(t => t.BaseType != null && t.BaseType.Name.Equals(nameof(Profile))).Select(t => t);

            //    foreach (var p in profiles)
            //    {
            //        mc.AddProfile((Profile)Activator.CreateInstance(p));
            //    }
            //});

            //IMapper mapper = mapperConfig.CreateMapper();

            //services.AddSingleton(mapper);
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
