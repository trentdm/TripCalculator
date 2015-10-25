using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TripCalculator.Services;

namespace TripCalculator.Tests.Services
{
    [TestFixture]
    public class LoggerTests
    {
        private ILogger _logger;
        private StringWriter _writer;

        [SetUp]
        public void Setup()
        {
            _logger = new Logger();
            _writer = new StringWriter();
            Trace.Listeners.Add(new DelimitedListTraceListener(_writer));
        }

        [TearDown]
        public void Teardown()
        {
            _writer.Dispose();
        }

        [Test]
        public void TestLogInfo()
        {
            _logger.LogInfo("Foo {0}", "Bar");
            var result =_writer.ToString();

            Assert.That(result, Is.EqualTo("Foo Bar\r\n"));
        }

        [Test]
        public void TestLogWarning()
        {
            _logger.LogWarning("Foo {0}", "Bar");
            var result = _writer.ToString();

            Assert.That(result, Is.EqualTo("Foo Bar\r\n"));
        }

        [Test]
        public void TestLogDebugPrepends()
        {
            _logger.LogDebug("Foo {0}", "Bar");
            var result = _writer.ToString();

            Assert.That(result, Is.EqualTo("Foo Bar\r\n"));
        }

        [Test]
        public void TestLogDebugAppendsArgs()
        {
            _logger.LogDebug("Foo {0}", "Bar");
            var result = _writer.ToString();
            
            Assert.That(result, Is.StringEnding("Foo Bar\r\n"));
        }

        [Test]
        public void TestLogExceptionPrepends()
        {
            var exception = new IOException("io");

            _logger.LogException(exception, "Foo {0}", "Bar");
            var result = _writer.ToString();

            Assert.That(result, Is.StringStarting("Exception: IOException, io\r\n"));
        }

        [Test]
        public void TestLogExceptionAppendsArgs()
        {
            var exception = new IOException("io");

            _logger.LogException(exception, "Foo {0}", "Bar");
            var result = _writer.ToString();

            Assert.That(result, Is.StringEnding("Foo Bar\r\n"));
        }
    }
}
