using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace PowerPuff.ViewModels
{
    public class Profile : BindableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public ObservableCollection<SocialConnection> Connections { get; set; }

        public Profile()
        {
            Connections = new ObservableCollection<SocialConnection>();
        }
    }
}
