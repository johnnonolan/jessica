using System.Collections.Generic;
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

        [Test]
        public void UrlEncode_WithNormalString_ShouldReturnOriginalString()
        {
            Assert.That("Hello".UrlEncode(), Is.EqualTo("Hello"));
        }

        [Test]
        public void UrlEncode_WithEncodableString_ShouldReturnEncodedString()
        {
            Assert.That("Hello, world!".UrlEncode(), Is.EqualTo("Hello%2c+world!"));
        }

        [Test]
        public void Join_WithPlaceholderAndEmptyCollection_ShouldReturnEmptyString()
        {
            Assert.That("&".Join(new List<int>()), Is.EqualTo(string.Empty));
        }

        [Test]
        public void Join_WithPlaceholderAndCollection_ShouldReturnCorrectString()
        {
            Assert.That("&".Join(new List<int> { 1, 2, 3 }), Is.EqualTo("1&2&3"));
        }
    }
}
