using SW_File_Helper.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SW_File_Helper.Controls
{
    /// <summary>
    /// Interaction logic for EditableListView.xaml
    /// </summary>
    public partial class EditableListView : UserControl
    {
        #region Fields
        private const string DEFAULTTITLE = "Please Enter your title.";

        private ObservableCollection<FileViewModel> m_files;

        private List<FileViewModel> m_selectedIndexes;
        #endregion

        #region Properties
        
        public ObservableCollection<FileViewModel> Files
        { get => m_files; set => m_files = value; }

        #region DP

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), 
                typeof(EditableListView), new PropertyMetadata(DEFAULTTITLE, OnTitleChanged));

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditableListView).Title = e.NewValue.ToString();
        }

        #endregion

        #endregion

        public EditableListView()
        {
            InitializeComponent();

            this.DataContext = this;

            m_files = new ObservableCollection<FileViewModel>();
            m_selectedIndexes = new List<FileViewModel>();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Files.Add(new FileViewModel(Files.Count + 1));
        }

        private void ViewFavoritesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Debug.WriteLine("Removing Items");
#endif
            if (m_selectedIndexes.Count > 0) 
            {
                foreach (var selected in m_selectedIndexes) 
                {
                    Files.Remove(selected);
#if DEBUG
                    Debug.WriteLine($"Item Removed: {selected}");
                    Debug.WriteLine("");
#endif
                }
            }
        }

        private void FileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_selectedIndexes.Clear();
#if DEBUG
            Debug.WriteLine("List Vie Selection Detected...");
#endif
            foreach (var selected in e.AddedItems)
            {
                m_selectedIndexes.Add(selected as FileViewModel ?? throw new InvalidCastException());
#if DEBUG
                Debug.WriteLine(selected);
                Debug.WriteLine("");
#endif
            }            
        }
    }
}
