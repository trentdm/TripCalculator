using NUnit.Framework;
using TripCalculator.Controllers;

namespace TripCalculator.Tests
{
    [TestFixture]
    public class TripExpensesControllerTests
    {
        private TripExpensesController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TripExpensesController();
        }
    }
}
