using System.Windows.Controls;
using PowerPuff.Features.VideoCall.ViewModels;

namespace PowerPuff.Features.VideoCall.Views
{
    /// <summary>
    /// Interaction logic for VideoMainView.xaml
    /// </summary>
    public partial class VideoMainView : UserControl
    {
        public VideoMainView(VideoMainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
