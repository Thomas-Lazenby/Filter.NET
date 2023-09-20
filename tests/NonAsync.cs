using Xunit;
using Filter;
using System.Threading.Tasks;

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
        public void Exclude_Item_SetsItemAsExcluded(IFilter<int> filter)
        {
            int item = 1;

            filter.Exclude(item);

            Assert.False(filter.ShouldInclude(item));
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

            bool result = filter.SetAsDefault(item);

            Assert.True(result);
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

    }

}
