using System.Windows;

namespace PowerPuff
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(ShellViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
