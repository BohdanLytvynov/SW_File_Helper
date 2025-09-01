using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SW_File_Helper.Controls
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl
    {
        #region Properties DP

        public Paragraph Paragraph
        {
            get { return (Paragraph)GetValue(ParagraphProperty); }
            set { SetValue(ParagraphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Paragraph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParagraphProperty;

        private static void WriteToConsole(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var This = (Console)d;
            var paragraph = (Paragraph)e.NewValue;
            if(paragraph != null)
                This.ConsoleWindow.Document.Blocks.Add(paragraph);
        }

        #endregion

        #region Ctor

        static Console()
        {
            ParagraphProperty =
            DependencyProperty.Register("Paragraph", typeof(Paragraph), 
            typeof(Console), 
            new PropertyMetadata(null, WriteToConsole));
        }

        public Console()
        {
            InitializeComponent();
            this.ConsoleWindow.Document = new FlowDocument();
        }

        #endregion

        #region Methods

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.ConsoleWindow.Document.Blocks.Clear();
        }

        #endregion
    }
}
