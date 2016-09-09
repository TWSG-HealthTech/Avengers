using System.Collections.Generic;
using System.Collections.ObjectModel;
using PowerPuff.Common;
using Prism.Commands;

namespace PowerPuff.Settings
{
    public class SocialConnection : ValidatableBase
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

        private string _newAlias;
        public string NewAlias
        {
            get { return _newAlias; }
            set
            {
                SetProperty(ref _newAlias, value);
                AddAliasCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> Aliases { get; set; }

        public SocialConnection()
        {
            Aliases = new ObservableCollection<string>();

            AddAliasCommand = new DelegateCommand(AddAlias, () => !string.IsNullOrWhiteSpace(NewAlias));
            DeleteAliasCommand = new DelegateCommand<string>(DeleteAlias);
        }

        public DelegateCommand AddAliasCommand { get; private set; }
        private void AddAlias()
        {
            if (!Aliases.Contains(NewAlias))
            {
                Aliases.Add(NewAlias);
            }

            NewAlias = string.Empty;
        }

        public DelegateCommand<string> DeleteAliasCommand { get; private set; }
        private void DeleteAlias(string alias)
        {
            if (Aliases.Contains(alias))
            {
                Aliases.Remove(alias);
            }
        }

        public override bool IsValid()
        {
            ClearErrors(nameof(Name));
            
            if (string.IsNullOrWhiteSpace(Name))
            {
                SetErrors(nameof(Name), new List<string> { "Name is required" });
            }

            return !HasErrors;
        }
    }
}