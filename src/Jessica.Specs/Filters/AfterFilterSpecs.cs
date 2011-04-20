using System;
using System.Linq;
using System.Web.Routing;
using Jessica.Filters;
using Machine.Specifications;

namespace Jessica.Specs.Filters
{
    public class after_filter_list
    {
        Establish context = () =>
            Filters = new AfterFilters();

        protected static AfterFilters Filters;
    }

    public class when_adding_two_after_filters_to_the_end : after_filter_list
    {
        Because of = () =>
        {
            Filters.AddFilterToEnd(_filter1);
            Filters.AddFilterToEnd(_filter2);
        };

        It should_contain_two_filters = () =>
            Filters.Filters.Count.ShouldEqual(2);

        It should_contain_filter1_as_first_filter = () =>
            Filters.Filters.First().ShouldEqual(_filter1);

        It should_contain_filter2_as_last_filter = () =>
            Filters.Filters.Last().ShouldEqual(_filter2);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
    }

    public class when_adding_two_after_filters_to_the_start : after_filter_list
    {
        Because of = () =>
        {
            Filters.AddFilterToStart(_filter1);
            Filters.AddFilterToStart(_filter2);
        };

        It should_contain_two_filters = () =>
            Filters.Filters.Count.ShouldEqual(2);

        It should_contain_filter1_as_last_filter = () =>
            Filters.Filters.Last().ShouldEqual(_filter1);

        It should_contain_filter2_as_first_filter = () =>
            Filters.Filters.First().ShouldEqual(_filter2);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
    }

    public class when_inserting_an_after_filter_at_index : after_filter_list
    {
        Because of = () =>
        {
            Filters.AddFilterToEnd(_filter1);
            Filters.AddFilterToEnd(_filter3);
            Filters.InsertFilterAtIndex(1, _filter2);
        };

        It should_contain_three_filters = () =>
            Filters.Filters.Count.ShouldEqual(3);

        It should_contain_filter1_at_index_zero = () =>
            Filters.Filters.ElementAt(0).ShouldEqual(_filter1);

        It should_contain_filter2_at_index_one = () =>
            Filters.Filters.ElementAt(1).ShouldEqual(_filter2);

        It should_contain_filter3_at_index_two = () =>
            Filters.Filters.ElementAt(2).ShouldEqual(_filter3);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
        static Action<RequestContext> _filter3 = context => { };
    }

    public class when_concatenating_an_after_filter : after_filter_list
    {
        Because of = () =>
        {
            Filters.AddFilterToEnd(_filter2);
            Filters += _filter1;
        };

        It should_contain_two_filters = () =>
            Filters.Filters.Count.ShouldEqual(2);

        It should_contain_filter1_as_last_filter = () =>
            Filters.Filters.Last().ShouldEqual(_filter1);

        It should_contain_filter2_as_first_filter = () =>
            Filters.Filters.First().ShouldEqual(_filter2);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
    }

    public class when_invoking_after_filters_containing_two_filters : after_filter_list
    {
        Because of = () =>
        {
            _filter1Called = false;
            _filter2Called = false;

            Filters += _filter1;
            Filters += _filter2;
            Filters.Invoke(null);
        };

        It should_invoke_the_first_filter = () =>
            _filter1Called.ShouldBeTrue();

        It should_invoke_the_second_filter = () =>
            _filter2Called.ShouldBeTrue();

        static bool _filter1Called;
        static bool _filter2Called;
        static Action<RequestContext> _filter1 = context => { _filter1Called = true; };
        static Action<RequestContext> _filter2 = context => { _filter2Called = true; };
    }
}
