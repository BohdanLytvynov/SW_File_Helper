using System;
using System.Collections.Generic;
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
        #endregion

        #region Properties

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
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewFavoritesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
