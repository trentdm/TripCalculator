using Moq;
using NUnit.Framework;
using TripCalculator.Controllers;
using TripCalculator.Models.TripExpenses;
using TripCalculator.Services;

namespace TripCalculator.Tests.Controllers
{
    [TestFixture]
    public class TripExpensesControllerTests
    {
        private TripExpensesController _controller;
        private Mock<ITripExpensesService> _tripExpensesService;
        private Mock<ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            _tripExpensesService = new Mock<ITripExpensesService>();
            _logger = new Mock<ILogger>();
            _controller = new TripExpensesController(_tripExpensesService.Object, _logger.Object);
        }

        [Test]
        public void TestPostReturnsPurchases()
        {
            var purchases = new TripMemberExpensesCollection();
            var settlements = new TripSettlementCollection();
            _tripExpensesService.Setup(s => s.GetSettlements(purchases))
                .Returns(settlements);

            var result = _controller.Post(purchases);
            Assert.That(result.Settlements, Is.EqualTo(settlements));
        }

        [Test]
        public void TestPostReturnsSettlements()
        {
            var purchases = new TripMemberExpensesCollection();
            var settlements = new TripSettlementCollection();
            _tripExpensesService.Setup(s => s.GetSettlements(purchases))
                .Returns(settlements);

            var result = _controller.Post(purchases);
            Assert.That(result.TripMemberExpenses, Is.EqualTo(purchases));
        }
    }
}
