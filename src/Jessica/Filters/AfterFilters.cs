using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace Jessica.Filters
{
    public class AfterFilters
    {
        public AfterFilters()
        {
            Filters = new List<Action<RequestContext>>();
        }

        public List<Action<RequestContext>> Filters { get; set; }

        public static AfterFilters operator +(AfterFilters filters, Action<RequestContext> filter)
        {
            filters.AddFilterToEnd(filter);
            return filters;
        }

        public void AddFilterToEnd(Action<RequestContext> filter)
        {
            Filters.Add(filter);
        }

        public void AddFilterToStart(Action<RequestContext> filter)
        {
            InsertFilterAtIndex(0, filter);
        }

        public void InsertFilterAtIndex(int index, Action<RequestContext> filter)
        {
            Filters.Insert(index, filter);
        }

        public void Invoke(RequestContext context)
        {
            Filters.ForEach(filter => filter.Invoke(context));
        }
    }
}
