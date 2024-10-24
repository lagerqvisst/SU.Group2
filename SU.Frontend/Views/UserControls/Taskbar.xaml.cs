﻿using SU.Frontend.ViewModels;
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
    /// Interaction logic for Taskbar.xaml
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


}
