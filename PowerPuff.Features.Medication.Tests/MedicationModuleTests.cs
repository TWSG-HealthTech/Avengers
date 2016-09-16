using Autofac;
using Machine.Specifications;
using Moq;
using PowerPuff.Common;
using PowerPuff.Features.Medication;
using PowerPuff.Features.Medication.ViewModels;
using PowerPuff.Features.Medication.Views;
using Prism.Regions;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests
{
    public class MedicationModuleTests
    {
        [Subject(typeof(MedicationModule))]
        class When_initialize_medication_module
        {
            private static MedicationModule _subject;
            private static Mock<IRegionManager> _regionManagerMock;
            private static IContainer _container;

            private Establish context = () =>
            {
                _regionManagerMock = new Mock<IRegionManager>();
                var containerBuilder = new ContainerBuilder();
                _container = containerBuilder.Build();
                _subject = new MedicationModule(_regionManagerMock.Object, _container);
            };

            Because of = () => _subject.Initialize();

            It should_register_view_with_main_button_region = () =>
                _regionManagerMock.Verify(manager => manager.RegisterViewWithRegion(RegionNames.MainButtonsRegion, typeof(MedicationMainButtonView)));
        }
    }
}