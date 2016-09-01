using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace PowerPuff.Settings
{
    public class SocialConnection : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _skype;
        public string Skype
        {
            get { return _skype; }
            set { SetProperty(ref _skype, value); }
        }

        public ObservableCollection<string> Aliases { get; set; }

        public string AliasesDisplay => string.Join(", ", Aliases);

        public SocialConnection()
        {
            Aliases = new ObservableCollection<string>();
        }
    }
}
