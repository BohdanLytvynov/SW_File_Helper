using SW_File_Helper.ViewModels.Models.Logs.Base;
using System.Windows;
using System.Windows.Controls;

namespace SW_File_Helper.Controls
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class Console : UserControl
    {
        #region Fields

        private Thread m_UpdateConsoleThread;

        private Queue<LogViewModel> m_Buffer;
        #endregion

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
            m_Buffer = new Queue<LogViewModel>();
            m_UpdateConsoleThread = new Thread(new ThreadStart(UpdateConsole));
            m_UpdateConsoleThread.IsBackground = true;

            InitializeComponent();
            m_UpdateConsoleThread.Start();
        }

        #endregion

        #region Methods
        private static void OnMessageToWriteCalled(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var This = (Console)d;
            This.m_Buffer.Enqueue((LogViewModel)e.NewValue);
        }

        #endregion

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.ConsoleWindow.Items.Clear();
        }

        private void UpdateConsole()
        {
            for ( ; ; )
            {
                this.ConsoleWindow.Dispatcher.Invoke(() =>
                {
                    if (m_Buffer.Count > 0)
                        this.ConsoleWindow.Items.Add(m_Buffer.Dequeue());
                });

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}
