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
        public void Include_Item_SetsItemAsIncluded(IFilter<int> filter)
        {
            int item = 1;

            filter.Include(item);

            Assert.True(filter.ShouldInclude(item));
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void Include_MultipleItems_SetsItemsAsIncluded(IFilter<int> filter)
        {
            int item1 = 1, item2 = 2;

            filter.Include(item1, item2);

            Assert.True(filter.ShouldInclude(item1));
            Assert.True(filter.ShouldInclude(item2));
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void Exclude_Item_SetsItemAsExcluded(IFilter<int> filter)
        {
            int item = 1;

            filter.Exclude(item);

            Assert.False(filter.ShouldInclude(item));
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void Exclude_MultipleItems_SetsItemsAsExcluded(IFilter<int> filter)
        {
            int item1 = 1, item2 = 2;

            filter.Exclude(item1, item2);

            Assert.False(filter.ShouldInclude(item1));
            Assert.False(filter.ShouldInclude(item2));
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void ShouldInclude_Item_ReturnsExpectedResult(IFilter<int> filter)
        {
            int item = 1;
            filter.Include(item);

            bool result = filter.ShouldInclude(item);

            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void SetAsDefault_Item_RemovesOverride(IFilter<int> filter)
        {
            int item = 1;
            filter.Include(item);
            filter.SetAsDefault(item);

            Assert.Equal(filter.Default, filter.ShouldInclude(item) ? FilterType.Include : FilterType.Exclude);
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void Clear_ResetsOverrides(IFilter<int> filter)
        {
            int item = 1;
            filter.Include(item);

            filter.Clear();

            Assert.Equal(filter.Default, filter.ShouldInclude(item) ? FilterType.Include : FilterType.Exclude);
        }

        [Theory]
        [ClassData(typeof(FilterTestData))]
        public void Clone_CreatesIndependentAndSameTypeCopy(IFilter<int> filter)
        {
            int item = 1;

            filter.Include(item);

            var clonedFilter = (IFilter<int>)filter.Clone();

            Assert.IsType(filter.GetType(), clonedFilter);

            Assert.True(filter.ShouldInclude(item));
            Assert.True(clonedFilter.ShouldInclude(item));

            clonedFilter.Exclude(item);

            Assert.True(filter.ShouldInclude(item));
            Assert.False(clonedFilter.ShouldInclude(item));
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
