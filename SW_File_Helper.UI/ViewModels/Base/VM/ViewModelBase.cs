using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace SW_File_Helper.ViewModels.Base.VM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Dispatcher m_Dispatcher;

        public Dispatcher Dispatcher
        {
            get => m_Dispatcher;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                m_Dispatcher = value;
            }
        }
        
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            var temp = Volatile.Read(ref this.PropertyChanged);
            temp?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region Setter
        public virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));

            if (field.Equals(value))
                return false;
            else
            {
                field = value;
                OnPropertyChanged(propName);
                return true;
            }
        }

        #endregion
                
        protected void QueueJobToDispatcher(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            m_Dispatcher.Invoke(action);
        }
    }
}
