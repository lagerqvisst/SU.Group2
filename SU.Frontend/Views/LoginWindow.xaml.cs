using System.Windows;
using SU.Frontend.ViewModels;

namespace SU.Frontend.Views;

/// <summary>
///     Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    private readonly LoginViewModel _loginViewModel;

    public LoginWindow(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        _loginViewModel = loginViewModel;
        DataContext = _loginViewModel;
    }
}