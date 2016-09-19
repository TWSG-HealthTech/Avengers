using System;
using System.Collections.Generic;
using Machine.Specifications;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.Tests.Services
{
    [Subject(typeof(Prescription))]
    public class PrescriptionTests
    {
        class When_filter_drug_orders
        {
            private static Prescription _subject;
            private static DrugOrder _drugOne;
            private static DrugOrder _drugTwo;
            private static DrugOrder _drugThree;
            private static DrugOrder _drugFour;
            private static IEnumerable<DrugOrder> _result;

            Establish context = () =>
            {
                _drugOne = new DrugOrder()
                {
                    DrugName = "One",
                    StartDate = new DateTime(2016, 9, 10),
                    EndDate = new DateTime(2016, 9, 15),
                    Frequency = "1"
                };
                _drugTwo = new DrugOrder()
                {
                    DrugName = "Two",
                    StartDate = new DateTime(2016, 9, 10),
                    EndDate = new DateTime(2016, 9, 15),
                    Frequency = "2"
                };
                _drugThree = new DrugOrder()
                {
                    DrugName = "Four",
                    StartDate = new DateTime(2016, 9, 5),
                    EndDate = new DateTime(2016, 9, 12),
                    Frequency = "1"
                };
                _subject = new Prescription
                {
                    DrugOrders = new List<DrugOrder>() { _drugOne, _drugTwo, _drugThree }
                };
            };

            Because of =
                () => _result = _subject.GetDrugOrdersForDateTimeWithFrequencies(new DateTime(2016, 9, 9, 12, 0, 0), new[] {"1"});

            It should_only_return_drugs_within_schedule =
                () => _result.ShouldContainOnly(new[] {_drugThree});
        }

    }
}
