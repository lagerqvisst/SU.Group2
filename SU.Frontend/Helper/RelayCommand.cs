using System.Windows.Input;

namespace SU.Frontend.Helper;

public class RelayCommand : ICommand
{
    private readonly Func<bool> _canExecute;
    private readonly Action _execute;

    // Konstruktor som bara tar emot 'Action'
    public RelayCommand(Action execute) : this(execute, () => true) // anropa huvudkonstruktorn med true som standard
    {
    }

    // Huvudkonstruktor som tar emot både 'Action' och 'canExecute'
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object? parameter)
    {
        _execute();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}