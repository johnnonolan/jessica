using Jessica.Extensions;
using NUnit.Framework;

namespace Jessica.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void With_WithPlaceholderAndStringArg_ShouldReturnCorrectString()
        {
            Assert.That("Hello, {0}".With("world!"), Is.EqualTo("Hello, world!"));
        }

        [Test]
        public void With_WithPlaceholderAndIntArg_ShouldReturnCorrectString()
        {
            Assert.That("10 x 10 = {0}".With(100), Is.EqualTo("10 x 10 = 100"));
        }

        [Test]
        public void With_WithNoPlaceholder_ShouldReturnOriginalString()
        {
            Assert.That("Foobar!".With(), Is.EqualTo("Foobar!"));
        }
    }
}
