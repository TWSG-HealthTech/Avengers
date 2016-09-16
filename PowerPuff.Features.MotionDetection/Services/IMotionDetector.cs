using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;
using System;
using System.Linq;

namespace PowerPuff.Features.MotionDetection.Services
{
    public class CameraMotionDetector : IMotionDetector, IDisposable
    {
        private readonly MotionDetector _detector =
           new MotionDetector(
              new TwoFramesDifferenceDetector(true));

        public CameraMotionDetector()
        {
            var info = new FilterInfoCollection(FilterCategory.VideoInputDevice).OfType<FilterInfo>().FirstOrDefault();
            _source = new VideoCaptureDevice(info.MonikerString);
            _source.NewFrame += VideoSourcePlayerNewFrame;
            _source.Start();
        }

        private void VideoSourcePlayerNewFrame(object sender, NewFrameEventArgs eventargs)
        {
            var motionValue = _detector.ProcessFrame(eventargs.Frame);
            if (motionValue > 0)
            {
                MotionDetected?.Invoke();
            }
        }

        private IVideoSource _source;

        public event Action MotionDetected;

        public void Dispose()
        {
            _source.SignalToStop();
            _source.WaitForStop();
        }
    }

    public interface IMotionDetector
    {
        event Action MotionDetected;
    }
}