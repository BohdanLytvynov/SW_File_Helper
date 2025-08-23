using SW_File_Helper.ViewModels.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SW_File_Helper.Controls
{    
    /// <summary>
    /// Interaction logic for EditableListView.xaml
    /// </summary>       
    public partial class EditableListView : UserControl
    {
        #region Nested Classes

        public class UnableToCreateInstanceException : Exception 
        {
            public UnableToCreateInstanceException(string assembly, string typeName) 
                : base($"Exception while trying to create Item's type. Type: {assembly}.{typeName}")
            {}
        }

        #endregion

        #region Fields
        private const string DEFAULTTITLE = "Please Enter your title.";
        
        private List<CustomListViewItem> m_selectedItems;
        #endregion

        #region Properties

        #region DP

        public string ListViewItemAssemblyPath
        {
            get { return (string)GetValue(ListViewItemAssemblyPathProperty); }
            set { SetValue(ListViewItemAssemblyPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListViewItemAssemblyPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListViewItemAssemblyPathProperty;

        public string ListViewItemTypeName
        {
            get { return (string)GetValue(ListViewItemTypeNameProperty); }
            set { SetValue(ListViewItemTypeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListViewItemTypeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListViewItemTypeNameProperty;

        public ObservableCollection<CustomListViewItem> Items
        {
            get { return (ObservableCollection<CustomListViewItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty;
                                        
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty;
        
        #endregion

        #endregion

        #region Ctor

        static EditableListView()
        {
            TitleProperty =
            DependencyProperty.Register("Title", typeof(string),
                typeof(EditableListView), new PropertyMetadata(DEFAULTTITLE,
                OnTitleChanged));

            ListViewItemTypeNameProperty =
            DependencyProperty.Register("ListViewItemTypeName", typeof(string),
                typeof(EditableListView),
                new PropertyMetadata(string.Empty,
                    OnListViewItemTypeNameChanged));

            ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<CustomListViewItem>),
                typeof(EditableListView),
                new PropertyMetadata(null,
                    OnItemsPropertyChanged));

            ListViewItemAssemblyPathProperty =
            DependencyProperty.Register(nameof(ListViewItemAssemblyPath),
                typeof(string),
                typeof(EditableListView), new PropertyMetadata(string.Empty, 
                OnListViewItemAssemblyPathPropertyChanged));
        }

        public EditableListView()
        {
            InitializeComponent();
            
            m_selectedItems = new List<CustomListViewItem>();
           
            EnableDisableRemoveButton();
        }       

        #endregion

        #region Methods

        #region Property Changed Handlers
        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var This = (d as EditableListView);

            This.Title = e.NewValue.ToString();
            This.ItemsListViewHeader.Text = e.NewValue.ToString();
        }
        private static void OnListViewItemTypeNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditableListView).ListViewItemTypeName = e.NewValue.ToString();
        }
        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditableListView).Items = (ObservableCollection<CustomListViewItem>)e.NewValue;
        }
        private static void OnListViewItemAssemblyPathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EditableListView).ListViewItemAssemblyPath = e.NewValue.ToString();
        }

        #endregion

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var itemObject = Activator.CreateInstanceFrom(ListViewItemAssemblyPath, ListViewItemTypeName);
            
            CustomListViewItem item = null;

            if (itemObject == null)
                throw new UnableToCreateInstanceException(ListViewItemAssemblyPath, ListViewItemTypeName);
            else
            {
                item = itemObject.Unwrap() as CustomListViewItem ?? throw new InvalidCastException("Unable to cast to CustomListViewItem! ListViewItem type must be inherited from the CustomListViewItem and contain the Empty Constructor.");
                var t = item.GetType();
                Debug.WriteLine(t.FullName);
                item.Id = Items.Count + 1;
                Items.Add(item);
                this.ItemsListView.Items.Add(item);
            }
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
            if (m_selectedItems.Count > 0)
            {
                for (int i = 0; i < m_selectedItems.Count; ++i)
                {
#if DEBUG
                    Debug.WriteLine($"Item Removed: {m_selectedItems[i]}");
                    Debug.WriteLine("");
#endif
                    for (int j = 0; j < Items.Count; j++)
                    {
                        if (Items[j].Equals(m_selectedItems[i]))
                        {
                            Items.RemoveAt(j);
                        }
                    }

                    for (int k = 0; k < this.ItemsListView.Items.Count; k++)
                    {
                        if (ItemsListView.Items[k].Equals(m_selectedItems[i]))
                        { 
                            ItemsListView.Items.RemoveAt(k);
                        }
                    }
                }

                m_selectedItems.Clear();

                EnableDisableRemoveButton();
            }
        }

        private void FileListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_selectedItems.Clear();
#if DEBUG
            Debug.WriteLine("List View Selection Detected...");
#endif
            foreach (var selected in e.AddedItems)
            {
                m_selectedItems.Add(selected as CustomListViewItem ?? throw new InvalidCastException());
#if DEBUG
                Debug.WriteLine(selected);
                Debug.WriteLine("");
#endif
            }

            EnableDisableRemoveButton();
        }

        private void EnableDisableRemoveButton()
        {
            if (m_selectedItems.Count == 0)
            {
                this.RemoveButton.IsEnabled = false;
            }
            else
            {
                this.RemoveButton.IsEnabled = true;
            }
        }

        #endregion        
    }
}
