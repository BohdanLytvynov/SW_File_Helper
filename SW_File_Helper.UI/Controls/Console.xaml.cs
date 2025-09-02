using System.Windows;
using System.Windows.Controls;

namespace SW_File_Helper.Controls
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl
    {
        #region Properties DP

        public object MessageToWrite
        {
            get { return (object)GetValue(MessageToWriteProperty); }
            set { SetValue(MessageToWriteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageToWrite.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageToWriteProperty =
            DependencyProperty.Register("MessageToWrite", typeof(object), 
                typeof(Console), 
                new PropertyMetadata(null, OnMessageToWriteCalled));

        #endregion

        #region Ctor

        public Console()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods
        private static void OnMessageToWriteCalled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var This = (Console)d;
            This.ConsoleWindow.Items.Add(e.NewValue);
        }

        #endregion

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.ConsoleWindow.Items.Clear();
        }
    }
}
