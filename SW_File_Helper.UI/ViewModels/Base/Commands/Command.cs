namespace SW_File_Helper.ViewModels.Base.Commands
{
    internal class Command : CommandBase
    {
        private readonly Action<object> m_execute;

        private readonly Func<object, bool> m_canExecute;

        public Func<object, bool> CanExecuteGetter { get => m_canExecute; }

        public Command(Action<object> execute, Func<object, bool> canExecute)
        {
            m_execute = execute ?? throw new ArgumentNullException(nameof(execute));
            m_canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => m_canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => m_execute.Invoke(parameter);
    }
}
