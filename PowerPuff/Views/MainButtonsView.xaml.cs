﻿using System.Windows.Controls;
using PowerPuff.ViewModels;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for MainButtonsView.xaml
    /// </summary>
    public partial class MainButtonsView : UserControl
    {
        public MainButtonsView(MainButtonsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}