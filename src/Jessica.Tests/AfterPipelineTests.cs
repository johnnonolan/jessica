using System;
using System.Linq;
using System.Web.Routing;
using NUnit.Framework;

namespace Jessica.Tests
{
    [TestFixture]
    public class AfterPipelineTests
    {
        AfterPipeline _pipeline;

        [SetUp]
        public void SetUp()
        {
            _pipeline = new AfterPipeline();
        }

        [Test]
        public void AddItemToEndOfPipeline_WithTwoItems_ShouldAddToEndOfList()
        {
            Action<RequestContext> action1 = context => { };
            Action<RequestContext> action2 = context => { };

            _pipeline.AddItemToEndOfPipeline(action1);
            _pipeline.AddItemToEndOfPipeline(action2);

            Assert.That(_pipeline.Pipeline.Count, Is.EqualTo(2));
            Assert.That(action2, Is.SameAs(_pipeline.Pipeline.Last()));
        }

        [Test]
        public void AddItemToEndOfPipeline_WithTwoItems_ShouldAddToStartOfList()
        {
            Action<RequestContext> action1 = context => { };
            Action<RequestContext> action2 = context => { };

            _pipeline.AddItemToStartOfPipeline(action1);
            _pipeline.AddItemToStartOfPipeline(action2);

            Assert.That(_pipeline.Pipeline.Count, Is.EqualTo(2));
            Assert.That(action2, Is.SameAs(_pipeline.Pipeline.First()));            
        }

        [Test]
        public void InsertItemAtPipelineIndex_WithThreeItems_ShouldInsertAtCorrectIndexes()
        {
            Action<RequestContext> action1 = context => { };
            Action<RequestContext> action2 = context => { };
            Action<RequestContext> action3 = context => { };

            _pipeline.AddItemToEndOfPipeline(action1);
            _pipeline.AddItemToEndOfPipeline(action3);
            _pipeline.InsertItemAtPipelineIndex(1, action2);

            Assert.That(action1, Is.SameAs((_pipeline.Pipeline.ElementAt(0))));
            Assert.That(action2, Is.SameAs((_pipeline.Pipeline.ElementAt(1))));
            Assert.That(action3, Is.SameAs((_pipeline.Pipeline.ElementAt(2))));
        }

        [Test]
        public void PlusEquals_WithItem_ShouldAddItemToEndOfList()
        {
            Action<RequestContext> action1 = context => { };
            Action<RequestContext> action2 = context => { };

            _pipeline.AddItemToEndOfPipeline(action2);
            _pipeline += action1;

            Assert.That(action1, Is.SameAs(_pipeline.Pipeline.Last()));
        }

        [Test]
        public void Invoke_WithTwoItems_ShouldCallAllItems()
        {
            var action1Called = false;
            var action2Called = false;
            Action<RequestContext> action1 = context => { action1Called = true; };
            Action<RequestContext> action2 = context => { action2Called = true; };

            _pipeline += action1;
            _pipeline += action2;
            _pipeline.Invoke(null);

            Assert.That(action1Called, Is.True);
            Assert.That(action2Called, Is.True);
        }
    }
}
