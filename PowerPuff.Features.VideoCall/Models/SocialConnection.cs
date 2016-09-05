using Prism.Mvvm;

namespace PowerPuff.Features.VideoCall.Models
{
    public class SocialConnection : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _skype;
        public string Skype
        {
            get { return _skype; }
            set { SetProperty(ref _skype, value); }
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }
    }
}
