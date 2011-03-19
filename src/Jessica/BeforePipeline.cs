using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Jessica
{
    public class BeforePipeline
    {
        private List<Action<RequestContext>> _pipeline;

        public BeforePipeline()
        {
            _pipeline = new List<Action<RequestContext>>();
        }

        public void Invoke(RequestContext context)
        {
            _pipeline.ForEach(
                action => action.Invoke(context));
        }

        public static implicit operator BeforePipeline(Action<RequestContext> action)
        {
            var pipeline = new BeforePipeline();
            pipeline.AddItemToEndOfPipeline(action);
            return pipeline;
        }

        public static BeforePipeline operator +(BeforePipeline pipeline, Action<RequestContext> action)
        {
            pipeline.AddItemToEndOfPipeline(action);
            return pipeline;
        }

        public static BeforePipeline operator +(BeforePipeline pipelineToAddTo, BeforePipeline pipelineToAdd)
        {
            pipelineToAddTo._pipeline.AddRange(pipelineToAdd._pipeline);
            return pipelineToAddTo;
        }

        public void AddItemToStartOfPipeline(Action<RequestContext> action)
        {
            InsertItemAtPipelineIndex(0, action);
        }

        public void AddItemToEndOfPipeline(Action<RequestContext> action)
        {
            _pipeline.Add(action);
        }

        public void InsertItemAtPipelineIndex(int index, Action<RequestContext> action)
        {
            _pipeline.Insert(index, action);
        }
    }
}
