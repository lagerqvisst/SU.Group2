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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SU.Frontend.Views.UserControls
{
    /// <summary>
    /// Interaction logic for DownloadButton.xaml
    /// </summary>
    public partial class DownloadButton : UserControl
    {
        public DownloadButton()
        {
            InitializeComponent();
        }


        //Reason for code behind is that the button is a usercontrol and the command is binded to the viewmodel, needs flexibility to be easily binded to different viewmodels/views

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(DownloadButton), new PropertyMetadata(null));
    }
}
