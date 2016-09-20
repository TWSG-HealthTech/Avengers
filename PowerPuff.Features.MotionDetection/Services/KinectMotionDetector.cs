using Microsoft.Kinect;
using System;
using System.Linq;

namespace PowerPuff.Features.MotionDetection.Services
{
    public class KinectMotionDetector : IMotionDetector, IDisposable
    {
        private readonly KinectSensor _kinectSensor;
        private readonly BodyFrameReader _bodyFrameReader;
        private Body[] _bodies;

        public KinectMotionDetector()
        {
            _kinectSensor = KinectSensor.GetDefault();
            _bodyFrameReader = _kinectSensor.BodyFrameSource.OpenReader();
            _bodyFrameReader.FrameArrived += BodyFrameReaderOnFrameArrived;
            _kinectSensor.Open();
        }

        private void BodyFrameReaderOnFrameArrived(object sender, BodyFrameArrivedEventArgs bodyFrameArrivedEventArgs)
        {
            using (var frame = bodyFrameArrivedEventArgs.FrameReference.AcquireFrame())
            {
                if (_bodies == null) _bodies = new Body[frame.BodyCount];
                frame.GetAndRefreshBodyData(_bodies);
            }
            if (_bodies.Any(b => b.IsTracked)) MotionDetected?.Invoke();
        }


        public event Action MotionDetected;

        public void Dispose()
        {
            _bodyFrameReader.FrameArrived -= BodyFrameReaderOnFrameArrived;
            _bodyFrameReader.Dispose();
            _kinectSensor.Close();
        }
    }
}