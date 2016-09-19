using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Moq;
using PowerPuff.Common.Gateway;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.Services;
using It = Machine.Specifications.It;

namespace PowerPuff.Features.Medication.Tests.Services
{
    [Subject(typeof(PrescriptionService))]
    public class PrescriptionServiceTests
    {
        private static PrescriptionService _subject;
        private static Mock<IServerGateway> _gatewayMock;
        private static Mock<IPrescriptionCache> _cacheMock;
        private static List<DrugOrder> _dummyDrugOrders;
        private static string _patientId;
        private static Prescription _result;

        private Establish context = () =>
        {
            _gatewayMock = new Mock<IServerGateway>();
            _cacheMock = new Mock<IPrescriptionCache>();
            _subject = new PrescriptionService(_gatewayMock.Object, _cacheMock.Object);
            _patientId = "patientId";
        };

        private class When_cache_has_no_data
        {
            Establish context = () =>
            {
                _dummyDrugOrders = new List<DrugOrder>() {new DrugOrder()};
                _gatewayMock.Setup(gw => gw.GetAsync<List<DrugOrder>>(Moq.It.IsAny<string>()))
                    .Returns(Task.FromResult(_dummyDrugOrders));
                _cacheMock.Setup(c => c.ExistPrescription(Moq.It.IsAny<string>())).Returns(false);
            };

            Because of = () => _result = _subject.GetPrescriptionAsync(_patientId).Result;

            It should_get_prescription_from_server =
                () => _result.DrugOrders.ShouldContainOnly(_dummyDrugOrders);

            It should_store_prescription_in_local_cache =
                () => _cacheMock.Verify(c => c.StorePrescription(_patientId, _result));
        }

        private class When_cache_has_data
        {
            private static Prescription _prescription;

            Establish context = () =>
            {
                _prescription = new Prescription();
                _cacheMock.Setup(c => c.ExistPrescription(Moq.It.IsAny<string>())).Returns(true);
                _cacheMock.Setup(c => c.RetrievePrescription(Moq.It.IsAny<string>()))
                    .Returns(Task.FromResult(_prescription));
            };

            Because of = () => _result = _subject.GetPrescriptionAsync(_patientId).Result;

            It should_get_precription_from_cache = () => _result.ShouldEqual(_prescription);
        }
    }
}