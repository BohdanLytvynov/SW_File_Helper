using SW_File_Helper.ViewModels.Views;
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
using System.Windows.Shapes;

namespace SW_File_Helper.Views
{
    /// <summary>
    /// Interaction logic for FavoritesWindow.xaml
    /// </summary>
    public partial class FavoritesWindow : Window
    {
        public FavoritesWindow()
        {
            InitializeComponent();
        }

        public event Action<string> OnTypeNameSet;

        public event Action<List<Guid>> OnFavoritesSelected;

        public void SetTypeName(string fullTypeName)
        { 
            OnTypeNameSet?.Invoke(fullTypeName);
        }

        public void FavoritesSelected(List<Guid> ids)
        { 
            OnFavoritesSelected?.Invoke(ids);
        }
    }
}
