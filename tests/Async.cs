using Xunit;
using Filter;
using System.Threading.Tasks;

namespace Tests
{
    public class Async
    {
        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task DefaultProperty_SetAndGet_ReturnsExpectedValueAsync(IFilter<int> filter)
        {
            // Include
            filter.Default = FilterType.Include;
            Assert.Equal(FilterType.Include, await Task.Run(() => filter.Default));

            // Exclude
            filter.Default = FilterType.Exclude;
            Assert.Equal(FilterType.Exclude, await Task.Run(() => filter.Default));
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task Include_Item_SetsItemAsIncludedAsync(IFilter<int> filter)
        {
            int item = 1;

            await Task.Run(() => filter.Include(item));

            Assert.True(await Task.Run(() => filter.ShouldInclude(item)));
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task Exclude_Item_SetsItemAsExcludedAsync(IFilter<int> filter)
        {
            int item = 1;

            await Task.Run(() => filter.Exclude(item));

            Assert.False(await Task.Run(() => filter.ShouldInclude(item)));
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task ShouldInclude_Item_ReturnsExpectedResultAsync(IFilter<int> filter)
        {
            int item = 1;
            await Task.Run(() => filter.Include(item));

            bool result = await Task.Run(() => filter.ShouldInclude(item));

            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task SetAsDefault_Item_RemovesOverrideAsync(IFilter<int> filter)
        {
            int item = 1;
            await Task.Run(() => filter.Include(item));

            bool result = await Task.Run(() => filter.SetAsDefault(item));

            Assert.True(result);
            Assert.Equal(filter.Default, await Task.Run(() => filter.ShouldInclude(item)) ? FilterType.Include : FilterType.Exclude);
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task Clear_ResetsOverridesAsync(IFilter<int> filter)
        {
            int item = 1;
            await Task.Run(() => filter.Include(item));

            await Task.Run(() => filter.Clear());

            Assert.Equal(filter.Default, await Task.Run(() => filter.ShouldInclude(item)) ? FilterType.Include : FilterType.Exclude);
        }
    }
}
