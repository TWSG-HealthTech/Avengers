using System.Linq;
using Machine.Specifications;
using SUT = PowerPuff.Common.Settings;

namespace PowerPuff.Common.Tests.Settings.SettingsRepository
{
    [Subject(typeof(SUT.MenuSettingsRepository))]
    public class When_register_a_menu
    {
        private Establish context = () =>
        {
            _subject = new SUT.MenuSettingsRepository();
        };

        private Because of = () => _subject.RegisterMenu("some title", "someViewId");

        private It should_return_that_menu_when_calling_find_all = () =>
        {
            var menus = _subject.FindAll();

            menus.ShouldNotBeNull();
            menus.Count().ShouldEqual(1);
            menus.First().Title.ShouldEqual("some title");
            menus.First().SettingContentViewId.ShouldEqual("someViewId");
        };

        private static SUT.MenuSettingsRepository _subject;
    }
}
