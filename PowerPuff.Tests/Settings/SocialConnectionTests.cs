using System.Linq;
using Machine.Specifications;
using PowerPuff.Settings;
using M = Moq;

namespace PowerPuff.Tests.Settings
{
    [Subject(typeof(SocialConnection))]
    public class SocialConnectionTests
    {
        Establish context = () =>
        {
            _subject = new SocialConnection();
        };

        private static SocialConnection _subject;

        public class When_add_alias_is_clicked_and_new_alias_is_not_in_the_list
        {
            private Establish context = () =>
            {
                _newAlias = "some alias";
                _subject.NewAlias = _newAlias;
            };

            Because of = () => _subject.AddAliasCommand.Execute();

            private It should_add_new_alias_to_list_of_available_aliases = () =>
                    _subject.Aliases.Contains(_newAlias);

            private static string _newAlias;
        }

        public class When_add_alias_is_clicked_and_new_alias_is_in_the_list
        {
            private Establish context = () =>
            {
                _newAlias = "some alias";
                _subject.Aliases.Add(_newAlias);
                _subject.NewAlias = _newAlias;
            };

            Because of = () => _subject.AddAliasCommand.Execute();

            private It should_not_add_duplicate_alias = () =>
                    _subject.Aliases.Count(a => a == _newAlias).ShouldEqual(1);

            private static string _newAlias;
        }

        public class When_delete_alias_is_clicked_and_alias_is_in_the_list
        {
            private Establish context = () =>
            {
                _subject.Aliases.Add("some alias");
            };

            Because of = () => _subject.DeleteAliasCommand.Execute("some alias");

            private It should_remove_that_alias = () =>
                    _subject.Aliases.ShouldNotContain("some alias");
        }

        public class When_delete_alias_is_clicked_and_alias_is_not_in_the_list
        {
            Because of = () => _subject.DeleteAliasCommand.Execute("some alias");

            private It should_ignore_the_request = () =>
                    _subject.Aliases.ShouldNotContain("some alias");
        }
    }
}
