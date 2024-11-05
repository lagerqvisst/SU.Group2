using System.Windows.Controls;
using SU.Frontend.ViewModels;

namespace SU.Frontend.Views.UserControls;

/// <summary>
///     Interaction logic for Taskbar.xaml
/// </summary>
public partial class Taskbar : UserControl
{
    public Taskbar()
    {
        InitializeComponent();
    }

    public Taskbar(LoginViewModel loginViewModel) : this()
    {
        DataContext = loginViewModel;
    }
}