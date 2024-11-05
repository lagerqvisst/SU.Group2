using System.Windows.Input;

namespace SU.Frontend.Helper;

public abstract class CommandBase : ICommand
{
    public event EventHandler CanExecuteChanged;

    public virtual bool CanExecute(object parameter)
    {
        return true;
    }

    public abstract void Execute(object parameter);

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}