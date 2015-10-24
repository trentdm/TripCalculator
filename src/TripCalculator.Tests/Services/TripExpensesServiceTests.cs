using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TripCalculator.Models.TripExpenses;
using TripCalculator.Services;

namespace TripCalculator.Tests.Services
{
    [TestFixture]
    public class TripExpensesServiceTests
    {
        private ITripExpensesService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TripExpensesService();
        }

        [Test]
        public void TestGetSettlementsOnePersonOneSettlement()
        {
            var memberExpenses = GetOneMemberExpenseCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlements = result.Settlements.ToList();

            Assert.That(settlements.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestGetSettlementsTwoPersonOneSettlement()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlements = result.Settlements.ToList();

            Assert.That(settlements.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestGetSettlementsTwoPersonPayer()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.Payer.Name, Is.EqualTo("Amber"));
        }

        [Test]
        public void TestGetSettlementsTwoPersonPayee()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.Payee.Name, Is.EqualTo("Brandon"));
        }

        [Test]
        public void TestGetSettlementsTwoPersonSettlementAmount()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.Amount, Is.EqualTo(22.45));
        }

        private TripMemberExpensesCollection GetOneMemberExpenseCollection()
        {
            var purchases = new TripMemberExpensesCollection
            {
                TripMemberExpenses = new List<TripMemberExpenses>
                {
                    new TripMemberExpenses
                    {
                        Member = new TripMember {Name = "Amber"},
                        Expenses = new[] {0M, 1.01M, 1.12M, 2.23M, 3.34M, 5.45M, 8.56M}
                    }
                }
            };
            return purchases;
        }

        private TripMemberExpensesCollection GetTwoMemberExpensesCollection()
        {
            var purchases = new TripMemberExpensesCollection
            {
                TripMemberExpenses = new List<TripMemberExpenses>
                {
                    new TripMemberExpenses
                    {
                        Member = new TripMember {Name = "Amber"},
                        Expenses = new[] {1.01M, 1.12M, 2.23M, 3.34M, 5.45M, 8.56M}
                    },
                    new TripMemberExpenses
                    {
                        Member = new TripMember {Name = "Brandon"},
                        Expenses = new[] {2.21M, 3.33M, 6.43M, 9.67M, 15.98M, 28.99M}
                    }
                }
            };
            return purchases;
        }
    }
}
