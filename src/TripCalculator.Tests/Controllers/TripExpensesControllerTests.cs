using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
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
            var tripMembers = new TripMemberCollection {TripMembers = new List<TripMember>()};
            var settlements = new TripSettlementCollection();
            _tripExpensesService.Setup(s => s.GetSettlements(tripMembers))
                .Returns(settlements);
            
            var result = _controller.Post(tripMembers);
            Assert.That(result.Data, Is.EqualTo(settlements));
        }

        [Test]
        public void TestPostReturnsSettlements()
        {
            var tripMembers = new TripMemberCollection { TripMembers = new List<TripMember>() };
            var settlements = new TripSettlementCollection();
            _tripExpensesService.Setup(s => s.GetSettlements(tripMembers))
                .Returns(settlements);

            var result = _controller.Post(tripMembers);
            Assert.That(result.Query, Is.EqualTo(tripMembers));
        }

        [Test]
        public void TestPostDeserialization()
        {
            var value = new TripMemberCollection
            {
                TripMembers = new List<TripMember>()
            };
            
            var json = new JsonMediaTypeFormatter();
            var jsonStr = Serialize(json, value);
            
            var result = Deserialize<TripMemberCollection>(json, jsonStr);

            Assert.That(result.TripMembers, Is.Not.EqualTo(null));
        }

        [Test]
        public void TestPostResponseSerialization()
        {
            var value = new TripExpensesResponse
            {
                Query = new TripMemberCollection(),
                Data = new TripSettlementCollection()
            };

            var json = new JsonMediaTypeFormatter();

            var result = Serialize(json, value);

            Assert.That(result, Is.StringContaining("TripMembers"));
        }

        private string Serialize<T>(MediaTypeFormatter formatter, T value)
        {
            var stream = new MemoryStream();
            var content = new StreamContent(stream);
            formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
            stream.Position = 0;
            return content.ReadAsStringAsync().Result;
        }

        private T Deserialize<T>(MediaTypeFormatter formatter, string str) where T : class
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return formatter.ReadFromStreamAsync(typeof(T), stream, null, null).Result as T;
        }
    }
}
