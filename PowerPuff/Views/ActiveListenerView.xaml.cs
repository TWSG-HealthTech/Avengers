using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PowerPuff.Views
{
    /// <summary>
    /// Interaction logic for ActiveListener.xaml
    /// </summary>
    public partial class ActiveListenerView : ButtonBase, IActiveListenerView
    {
        public event Action OnListnerActivatorClick;
        public void ActiveListeningStarted()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                rectangleScale.CenterX = square.Width/2;
                rectangleScale.CenterY = square.Height/2;
                circleTransform.CenterX = circle.Width/2;
                circleTransform.CenterY = circle.Height/2;
                triangleTransform.CenterX = triangle.Width/2;
                triangleTransform.CenterY = triangle.Height/2;


                var daSquare = new DoubleAnimation();
                daSquare.From = 1;
                daSquare.To = 1.5;
                daSquare.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                daSquare.EasingFunction = new CubicEase {EasingMode = EasingMode.EaseInOut};

                daSquare.AutoReverse = true;
                daSquare.RepeatBehavior = RepeatBehavior.Forever;

                var daCircle = daSquare.Clone();
                daCircle.Duration = new Duration(TimeSpan.FromMilliseconds(750));


                var daTriangle = daCircle.Clone();
                daTriangle.Duration = new Duration(TimeSpan.FromMilliseconds(1000));


                rectangleScale.BeginAnimation(ScaleTransform.ScaleXProperty, daSquare);
                rectangleScale.BeginAnimation(ScaleTransform.ScaleYProperty, daSquare);

                circleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, daCircle);
                circleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, daCircle);

                triangleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, daTriangle);
                triangleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, daTriangle);
            }));
        }

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
