using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Jessica
{
    public class BeforePipeline
    {
        public List<Action<RequestContext>> Pipeline { get; set; }

        public BeforePipeline()
        {
            Pipeline = new List<Action<RequestContext>>();
        }

        public void Invoke(RequestContext context)
        {
            Pipeline.ForEach(action => action.Invoke(context));
        }

        public static BeforePipeline operator +(BeforePipeline pipeline, Action<RequestContext> action)
        {
            pipeline.AddItemToEndOfPipeline(action);
            return pipeline;
        }

        public void AddItemToStartOfPipeline(Action<RequestContext> action)
        {
            InsertItemAtPipelineIndex(0, action);
        }

        public void AddItemToEndOfPipeline(Action<RequestContext> action)
        {
            Pipeline.Add(action);
        }

        public void InsertItemAtPipelineIndex(int index, Action<RequestContext> action)
        {
            Pipeline.Insert(index, action);
        }
    }
}
