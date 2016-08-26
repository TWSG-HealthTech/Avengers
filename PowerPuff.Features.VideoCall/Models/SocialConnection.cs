using Prism.Mvvm;

namespace PowerPuff.Features.VideoCall.Models
{
    public class SocialConnection : BindableBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _skypeId;

        public string SkypeId
        {
            get { return _skypeId; }
            set { SetProperty(ref _skypeId, value); }
        }

        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set { SetProperty(ref _avatarUrl, value); }
        }
    }
}
