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
        public void TestGetSettlementsOnePersonSettlementCount()
        {
            var memberExpenses = GetOneMemberExpenseCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlements = result.Settlements.ToList();

            Assert.That(settlements.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestGetSettlementsTwoPersonSettlementCount()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlements = result.Settlements.ToList();

            Assert.That(settlements.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestGetSettlementsTwoPersonSender()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.SenderName, Is.EqualTo("Amber"));
        }

        [Test]
        public void TestGetSettlementsTwoPersonReceiver()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.ReceiverName, Is.EqualTo("Brandon"));
        }

        [Test]
        public void TestGetSettlementsTwoPersonSettlementAmount()
        {
            var memberExpenses = GetTwoMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.Amount, Is.EqualTo(22.45));
        }

        [Test]
        public void TestGetSettlementsThreePersonSettlementCount()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlements = result.Settlements.ToList();

            Assert.That(settlements.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestGetSettlementsThreePersonFirstSettlementSender()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.SenderName, Is.EqualTo("Catherine"));
        }

        [Test]
        public void TestGetSettlementsThreePersonFirstSettlementReceiver()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.ReceiverName, Is.EqualTo("Brandon"));
        }

        [Test]
        public void TestGetSettlementsThreePersonFirstSettlementAmount()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(0);

            Assert.That(settlement.Amount, Is.EqualTo(102.09M));
        }

        [Test]
        public void TestGetSettlementsThreePersonSecondSettlementSender()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(1);

            Assert.That(settlement.SenderName, Is.EqualTo("Amber"));
        }

        [Test]
        public void TestGetSettlementsThreePersonSecondSettlementReceiver()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(1);

            Assert.That(settlement.ReceiverName, Is.EqualTo("Brandon"));
        }

        [Test]
        public void TestGetSettlementsThreePersonSecondSettlementAmount()
        {
            var memberExpenses = GetThreeMemberExpensesCollection();

            var result = _service.GetSettlements(memberExpenses);
            var settlement = result.Settlements.ElementAt(1);

            Assert.That(settlement.Amount, Is.EqualTo(16.97M));
        }

        private TripMemberCollection GetOneMemberExpenseCollection()
        {
            var purchases = new TripMemberCollection
            {
                TripMembers = new List<TripMember>
                {
                    new TripMember
                    {
                        Name = "Amber",
                        Expenses = new[] {0M, 1.01M, 1.12M, 2.23M, 3.34M, 5.45M, 8.56M}
                    }
                }
            };
            return purchases;
        }

        private TripMemberCollection GetTwoMemberExpensesCollection()
        {
            var purchases = new TripMemberCollection
            {
                TripMembers = new List<TripMember>
                {
                    new TripMember
                    {
                        Name = "Amber",
                        Expenses = new[] {1.01M, 1.12M, 2.23M, 3.34M, 5.45M, 8.56M}
                    },
                    new TripMember
                    {
                        Name = "Brandon",
                        Expenses = new[] {2.21M, 3.33M, 6.43M, 9.67M, 15.98M, 28.99M}
                    }
                }
            };
            return purchases;
        }

        private TripMemberCollection GetThreeMemberExpensesCollection()
        {
            var purchases = new TripMemberCollection
            {
                TripMembers = new List<TripMember>
                {
                    new TripMember
                    {
                        Name = "Amber",
                        Expenses = new[] { 1.25M, 1.50M, 5.67M, 98.41M }
                    },
                    new TripMember
                    {
                        Name = "Brandon",
                        Expenses = new[] { 49.96M, 87.12M, 105.78M }
                    },
                    new TripMember
                    {
                        Name = "Catherine",
                        Expenses = new[] { 1.01M, 1.12M, 2.23M, 3.34M, 5.45M, 8.56M }
                    }
                }
            };
            return purchases;
        }
    }
}
