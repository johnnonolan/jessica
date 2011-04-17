using System;
using System.Linq;
using System.Web.Routing;
using Jessica.Filters;
using NUnit.Framework;

namespace Jessica.Tests.Filters
{
    [TestFixture]
    public class AfterFiltersTests
    {
        AfterFilters _filters;

        [SetUp]
        public void SetUp()
        {
            _filters = new AfterFilters();
        }

        [Test]
        public void AddFilterToEnd_WithTwoFilters_ShouldAddToEndOfList()
        {
            Action<RequestContext> filter1 = context => { };
            Action<RequestContext> filter2 = context => { };

            _filters.AddFilterToEnd(filter1);
            _filters.AddFilterToEnd(filter2);

            Assert.That(_filters.Filters.Count, Is.EqualTo(2));
            Assert.That(filter2, Is.SameAs(_filters.Filters.Last()));
        }

        [Test]
        public void AddFilterToStart_WithTwoFilters_ShouldAddToStartOfList()
        {
            Action<RequestContext> filter1 = context => { };
            Action<RequestContext> filter2 = context => { };

            _filters.AddFilterToStart(filter1);
            _filters.AddFilterToStart(filter2);

            Assert.That(_filters.Filters.Count, Is.EqualTo(2));
            Assert.That(filter2, Is.SameAs(_filters.Filters.First()));            
        }

        [Test]
        public void InsertFilterAtIndex_WithThreeFilters_ShouldInsertAtCorrectIndexes()
        {
            Action<RequestContext> filter1 = context => { };
            Action<RequestContext> filter2 = context => { };
            Action<RequestContext> filter3 = context => { };

            _filters.AddFilterToEnd(filter1);
            _filters.AddFilterToEnd(filter3);
            _filters.InsertFilterAtIndex(1, filter2);

            Assert.That(filter1, Is.SameAs((_filters.Filters.ElementAt(0))));
            Assert.That(filter2, Is.SameAs((_filters.Filters.ElementAt(1))));
            Assert.That(filter3, Is.SameAs((_filters.Filters.ElementAt(2))));
        }

        [Test]
        public void PlusEquals_WithFilter_ShouldAddToEndOfList()
        {
            Action<RequestContext> filter1 = context => { };
            Action<RequestContext> filter2 = context => { };

            _filters.AddFilterToEnd(filter2);
            _filters += filter1;

            Assert.That(filter1, Is.SameAs(_filters.Filters.Last()));
        }

        [Test]
        public void Invoke_WithTwoFilters_ShouldCallAllItems()
        {
            var filter1Called = false;
            var filter2Called = false;
            Action<RequestContext> filter1 = context => { filter1Called = true; };
            Action<RequestContext> filter2 = context => { filter2Called = true; };

            _filters += filter1;
            _filters += filter2;
            _filters.Invoke(null);

            Assert.That(filter1Called, Is.True);
            Assert.That(filter2Called, Is.True);
        }
    }
}
