using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for ActiveListener.xaml
    /// </summary>
    public partial class ActiveListenerView : ButtonBase, IActiveListenerView
    {
        public event Action OnListnerActivatorClick;

        public ActiveListenerView()
        {
            InitializeComponent();
        }

        private void ActiveListenerView_OnClick(object sender, RoutedEventArgs e)
        {
            OnListnerActivatorClick?.Invoke();
        }
    }
}
