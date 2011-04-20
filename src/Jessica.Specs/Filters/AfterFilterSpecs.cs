using System;
using System.Linq;
using System.Web.Routing;
using Jessica.Filters;
using Machine.Specifications;

namespace Jessica.Specs.Filters
{
    public class when_adding_two_after_filters_to_the_end
    {
        Establish context = () =>
            _filters = new AfterFilters();

        Because of = () =>
        {
            _filters.AddFilterToEnd(_filter1);
            _filters.AddFilterToEnd(_filter2);
        };

        It should_contain_two_filters = () =>
            _filters.Filters.Count.ShouldEqual(2);

        It should_contain_filter1_as_first_filter = () =>
            _filters.Filters.First().ShouldEqual(_filter1);

        It should_contain_filter2_as_last_filter = () =>
            _filters.Filters.Last().ShouldEqual(_filter2);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
        static AfterFilters _filters;
    }

    public class when_adding_two_after_filters_to_the_start
    {
        Establish context = () =>
            _filters = new AfterFilters();

        Because of = () =>
        {
            _filters.AddFilterToStart(_filter1);
            _filters.AddFilterToStart(_filter2);
        };

        It should_contain_two_filters = () =>
            _filters.Filters.Count.ShouldEqual(2);

        It should_contain_filter1_as_last_filter = () =>
            _filters.Filters.Last().ShouldEqual(_filter1);

        It should_contain_filter2_as_first_filter = () =>
            _filters.Filters.First().ShouldEqual(_filter2);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
        static AfterFilters _filters;
    }

    public class when_inserting_an_after_filter_at_index
    {
        Establish context = () =>
            _filters = new AfterFilters();

        Because of = () =>
        {
            _filters.AddFilterToEnd(_filter1);
            _filters.AddFilterToEnd(_filter3);
            _filters.InsertFilterAtIndex(1, _filter2);
        };

        It should_contain_three_filters = () =>
            _filters.Filters.Count.ShouldEqual(3);

        It should_contain_filter1_at_index_zero = () =>
            _filters.Filters.ElementAt(0).ShouldEqual(_filter1);

        It should_contain_filter2_at_index_one = () =>
            _filters.Filters.ElementAt(1).ShouldEqual(_filter2);

        It should_contain_filter3_at_index_two = () =>
            _filters.Filters.ElementAt(2).ShouldEqual(_filter3);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
        static Action<RequestContext> _filter3 = context => { };
        static AfterFilters _filters;
    }

    public class when_concatenating_an_after_filter
    {
        Establish context = () =>
            _filters = new AfterFilters();

        Because of = () =>
        {
            _filters.AddFilterToEnd(_filter2);
            _filters += _filter1;
        };

        It should_contain_two_filters = () =>
            _filters.Filters.Count.ShouldEqual(2);

        It should_contain_filter1_as_last_filter = () =>
            _filters.Filters.Last().ShouldEqual(_filter1);

        It should_contain_filter2_as_first_filter = () =>
            _filters.Filters.First().ShouldEqual(_filter2);

        static Action<RequestContext> _filter1 = context => { };
        static Action<RequestContext> _filter2 = context => { };
        static AfterFilters _filters;
    }

    public class when_invoking_after_filters_containing_two_filters
    {
        Establish context = () =>
            _filters = new AfterFilters();

        Because of = () =>
        {
            _filter1Called = false;
            _filter2Called = false;

            _filters += _filter1;
            _filters += _filter2;
            _filters.Invoke(null);
        };

        It should_invoke_the_first_filter = () =>
            _filter1Called.ShouldBeTrue();

        It should_invoke_the_second_filter = () =>
            _filter2Called.ShouldBeTrue();

        static bool _filter1Called;
        static bool _filter2Called;
        static Action<RequestContext> _filter1 = context => { _filter1Called = true; };
        static Action<RequestContext> _filter2 = context => { _filter2Called = true; };
        static AfterFilters _filters;
    }
}
