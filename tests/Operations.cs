using Xunit;
using Filter;

namespace Filter.Tests
{
    public class FilterOperations
    {


        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void ChainTest(IFilter<int> filter)
        {
            Assert.IsType(filter.Include(1).GetType(), filter);
            Assert.IsType(filter.Include(1,2).GetType(), filter);

            Assert.IsType(filter.Exclude(1).GetType(), filter);
            Assert.IsType(filter.Exclude(1, 2).GetType(), filter);

            Assert.IsType(filter.Clear().GetType(), filter);
            Assert.IsType(filter.SetAsDefault(1).GetType(), filter);

        }

        [Theory]
        [ClassData(typeof(CombinedFilterPairTestData))]
        public void Equal(IFilter<int> filter1, IFilter<int> filter2)
        {
            Assert.Equal(filter1, filter2);

            static void addValuesToFilter(IFilter<int> filter)
            {
                filter.Include(2, 3, 4, 5)
                    .Exclude(8, 9, 10);
            }

            addValuesToFilter(filter1);
            addValuesToFilter(filter2);

            Assert.Equal(filter1, filter2);

            filter1.Clear();
            filter2.Clear();

            Assert.Equal(filter1, filter2);
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void CloneTest(IFilter<int> filter)
        {
            // NOTE: If the equal test is wrong then this will be wrong as we're using the equals function here.

            // Make sure doesn't throw any exceptions when cloning normal one.
            var cloneFilter1 = filter.Clone();

            Assert.Equal(filter, cloneFilter1);

            filter.Include(2, 1, 2, 4, 5)
                .Exclude(6, 7, 8, 9, 10);

            var cloneFilter2 = filter.Clone();

            Assert.Equal(filter, cloneFilter2);
        }

    }
}
