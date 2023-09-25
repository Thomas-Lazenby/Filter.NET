using Xunit;
using Filter;

namespace Tests
{
    public class NonAsync
    {

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void DefaultProperty_SetAndGet_ReturnsExpectedValue(IFilter<int> filter)
        {
            // Include
            filter.Default = FilterType.Include;
            Assert.Equal(FilterType.Include, filter.Default);

            // Exclude
            filter.Default = FilterType.Exclude;
            Assert.Equal(FilterType.Exclude, filter.Default);
        }

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
        public void FiltersAreEqual_WhenBothAreNew(IFilter<int> filter1, IFilter<int> filter2)
        {
            Assert.Equal(filter1, filter2);
        }

    }
}
