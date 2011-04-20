using System.Collections.Generic;
using Jessica.Extensions;
using Machine.Specifications;

namespace Jessica.Specs.Extensions
{
    public class when_iterating_over_an_empty_collection
    {
        Establish context = () =>
            _collection = new List<int>();

        Because of = () =>
            _collection.ForEach(num => _iterations++);

        It should_iterate_zero_times = () =>
            _iterations.ShouldEqual(0);

        static IEnumerable<int> _collection;
        static int _iterations;
    }

    public class when_iterating_over_a_collection_of_one_item
    {
        Establish context = () =>
            _collection = new List<int> { 1 };

        Because of = () =>
            _collection.ForEach(num => _iterations++);

        It should_iterate_one_time = () =>
            _iterations.ShouldEqual(1);

        static IEnumerable<int> _collection;
        static int _iterations;
    }

    public class when_iterating_over_a_collection_of_ten_items
    {
        Establish context = () =>
            _collection = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        Because of = () =>
            _collection.ForEach(num => _iterations++);

        It should_iterate_ten_times = () =>
            _iterations.ShouldEqual(10);

        static List<int> _collection;
        static int _iterations;
    }
}
