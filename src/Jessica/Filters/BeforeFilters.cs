using System;
using System.Collections.Generic;
using System.Web.Routing;
using Jessica.Responses;

namespace Jessica.Filters
{
    public class BeforeFilters
    {
        public List<Func<RequestContext, Response>> Filters { get; set; }

        public BeforeFilters()
        {
            Filters = new List<Func<RequestContext, Response>>();
        }

        public Response Invoke(RequestContext context)
        {
            Response response = null;

            using (var enumerator = Filters.GetEnumerator())
            {
                while (response == null && enumerator.MoveNext())
                {
                    if (enumerator.Current != null)
                    {
                        response = enumerator.Current.Invoke(context);
                    }
                }
            }

            return response;
        }

        public static BeforeFilters operator +(BeforeFilters filters, Func<RequestContext, Response> filter)
        {
            filters.AddFilterToEnd(filter);
            return filters;
        }

        public void AddFilterToStart(Func<RequestContext, Response> filter)
        {
            InsertFilterAtIndex(0, filter);
        }

        public void AddFilterToEnd(Func<RequestContext, Response> filter)
        {
            Filters.Add(filter);
        }

        public void InsertFilterAtIndex(int index, Func<RequestContext, Response> filter)
        {
            Filters.Insert(index, filter);
        }
    }
}
