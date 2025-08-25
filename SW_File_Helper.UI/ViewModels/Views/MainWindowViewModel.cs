using Microsoft.Extensions.DependencyInjection;
using SW_File_Helper.BL.FileProcessors;
using SW_File_Helper.Converters;
using SW_File_Helper.DAL.Models;
using SW_File_Helper.ServiceWrappers;
using SW_File_Helper.ViewModels.Base.Commands;
using SW_File_Helper.ViewModels.Base.VM;
using SW_File_Helper.ViewModels.Models;
using SW_File_Helper.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SW_File_Helper.ViewModels.Views
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Events

        public EventHandler WindowClosed;

        #endregion

        #region Fields
        private string m_title;

        private string m_currentAssembly;
        
        private string m_ListViewFileViewModelTypeName;

        private ObservableCollection<CustomListViewItem> m_files;

        private SettingsPage m_settingsPage;

        private ServiceWrapper m_serviceWrapper;

        private IFileViewModelToFileModelConverter m_fileViewModelToFileModelConverter;

        private IFileProcessor m_fileProcessor;
        #endregion

        #region Properties
        public ObservableCollection<CustomListViewItem> Files
        { get=>m_files; set=>m_files = value; }
        public string Title { get => m_title; set => Set(ref m_title, value); }
        public string CurrentAssembly 
        { get=>m_currentAssembly; set=>Set(ref m_currentAssembly, value); }       
        public string ListViewFileViewModel
        { get=>m_ListViewFileViewModelTypeName; set=>Set(ref m_ListViewFileViewModelTypeName, value); }
        public SettingsPage SettingsPage { get=> m_settingsPage; set=> Set(ref m_settingsPage, value); }
        #endregion

        #region Commands
        public ICommand OnProcessButtonPressed { get; }

        public ICommand OnQuiteButtonPressed { get; }
        #endregion

        #region Ctor
        public MainWindowViewModel(ServiceWrapper serviceWrapper) : this()
        {
            if(serviceWrapper == null) 
                throw new ArgumentNullException(nameof(serviceWrapper));

            m_serviceWrapper = serviceWrapper;

            m_settingsPage = m_serviceWrapper.Services.GetRequiredService<SettingsPage>();
            m_fileViewModelToFileModelConverter = m_serviceWrapper
                .Services.GetRequiredService<IFileViewModelToFileModelConverter>();
            m_fileProcessor = m_serviceWrapper.Services.GetRequiredService<IFileProcessor>();
        }

        public MainWindowViewModel()
        {
            #region Field Initialization
            m_files = new ObservableCollection<CustomListViewItem>();
            m_title = "SW File Helper v 1.0.0.0";
            m_currentAssembly = Assembly.GetEntryAssembly().Location;
            m_ListViewFileViewModelTypeName = typeof(ListViewFileViewModel).FullName;
            #endregion

            #region Init Commands

            OnProcessButtonPressed = new Command(
                OnProcessButtonPressedExecute,
                CanOnProcessButtonPressedExecute
                );

            OnQuiteButtonPressed = new Command(
                OnQuiteButtonPressedExecute, 
                CanOnQuiteButtonPressedExecute
                );

            #endregion
        }
        #endregion

        #region Methods
        #region On Process Button Pressed

        private bool CanOnProcessButtonPressedExecute(object p)
        {
            if(Files.Count == 0)
                return false;

            foreach (var item in Files)
            {
                (item as ListViewFileViewModel)!.Validate();

                if(!item.IsValid && item.IsEnabled)
                    return false;
            }

            return true;
        }

        private void OnProcessButtonPressedExecute(object p)
        {
            var filesToProcess = Files.Where(x => x.IsValid && x.IsEnabled).Select(x => x as ListViewFileViewModel);

            List<FileModel> fileModels = new List<FileModel>();

            foreach (var file in filesToProcess)
            {
                fileModels.Add(m_fileViewModelToFileModelConverter.Convert(file));
            }

            m_fileProcessor.Process(fileModels);
        }

        #endregion

        #region On Quite Button Pressed

        private bool CanOnQuiteButtonPressedExecute(object p) => true;

        private void OnQuiteButtonPressedExecute(object p)
        { 
            WindowClosed?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #endregion
    }
}
