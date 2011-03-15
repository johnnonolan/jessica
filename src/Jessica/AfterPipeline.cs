using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Jessica
{
    public class AfterPipeline
    {
        private readonly List<Action<RequestContext>> _pipeline;

        public AfterPipeline()
        {
            _pipeline = new List<Action<RequestContext>>();
        }

        public void Invoke(RequestContext context)
        {
            _pipeline.ForEach(
                action => action.Invoke(context));
        }

        public static implicit operator AfterPipeline(Action<RequestContext> action)
        {
            var pipeline = new AfterPipeline();
            pipeline.AddItemToEndOfPipeline(action);
            return pipeline;
        }

        public static AfterPipeline operator +(AfterPipeline pipeline, Action<RequestContext> action)
        {
            pipeline.AddItemToEndOfPipeline(action);
            return pipeline;
        }

        public static AfterPipeline operator +(AfterPipeline pipelineToAddTo, AfterPipeline piplineToAdd)
        {
            pipelineToAddTo._pipeline.AddRange(piplineToAdd._pipeline);
            return pipelineToAddTo;
        }

        public virtual void AddItemToStartOfPipeline(Action<RequestContext> action)
        {
            InsertItemAtPipelineIndex(0, action);
        }

        public virtual void AddItemToEndOfPipeline(Action<RequestContext> action)
        {
            _pipeline.Add(action);
        }

        public virtual void InsertItemAtPipelineIndex(int index, Action<RequestContext> action)
        {
            _pipeline.Insert(index, action);
        }
    }
}
