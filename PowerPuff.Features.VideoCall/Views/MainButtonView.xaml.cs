using PowerPuff.Features.VideoCall.ViewModels;

namespace PowerPuff.Features.VideoCall.Views
{
    /// <summary>
    /// Interaction logic for MainButtonView.xaml
    /// </summary>
    public partial class MainButtonView
    {
        public MainButtonView(MainButtonViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
