﻿using System.Collections.Generic;
using System.Linq;
using Jessica.Extensions;
using NUnit.Framework;

namespace Jessica.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        public void ForEach_WithCollectionOf0_ShouldIterate0Times()
        {
            var collection = new List<int>();
            var iterations = 0;

            collection.AsEnumerable().ForEach(num => iterations++);

            Assert.That(iterations, Is.EqualTo(0));
        }

        [Test]
        public void ForEach_WithCollectionOf1_ShouldIterate1Time()
        {
            var collection = new List<int> { 1 };
            var iterations = 0;

            collection.AsEnumerable().ForEach(num => iterations++);

            Assert.That(iterations, Is.EqualTo(1));
        }

        [Test]
        public void ForEach_WithCollectionOf10_ShouldIterate10Times()
        {
            var collection = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var iterations = 0;

            collection.AsEnumerable().ForEach(num => iterations++);

            Assert.That(iterations, Is.EqualTo(10));
        }
    }
}
