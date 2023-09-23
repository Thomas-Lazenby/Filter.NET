using Xunit;
using Filter;
using System.Threading.Tasks;
using System.Collections.Generic; // Required for List<T>

namespace Tests
{
    public class Async
    {
        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task DefaultProperty_SetAndGet_ReturnsExpectedValueAsync(IFilter<int> filter)
        {
            // Include
            await Task.Run(() => filter.Default = FilterType.Include);
            Assert.Equal(FilterType.Include, await Task.Run(() => filter.Default));

            // Exclude
            await Task.Run(() => filter.Default = FilterType.Exclude);
            Assert.Equal(FilterType.Exclude, await Task.Run(() => filter.Default));
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task Include_MultipleItems_SetsItemsAsIncludedAsync(IFilter<int> filter)
        {
            int item1 = 1, item2 = 2;

            await Task.Run(() => filter.Include(item1, item2));

            Assert.True(await Task.Run(() => filter.ShouldInclude(item1)));
            Assert.True(await Task.Run(() => filter.ShouldInclude(item2)));
        }

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task Exclude_MultipleItems_SetsItemsAsExcludedAsync(IFilter<int> filter)
        {
            int item1 = 1, item2 = 2;

            await Task.Run(() => filter.Exclude(item1, item2));

            Assert.False(await Task.Run(() => filter.ShouldInclude(item1)));
            Assert.False(await Task.Run(() => filter.ShouldInclude(item2)));
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

            // Spin up multiple tasks to perform operations on the filter.
            var tasks = new List<Task>
            {
                Task.Run(() => filter.Include(item)),
                Task.Run(() => filter.SetAsDefault(item)),
                Task.Run(() =>
                    {
                        // Ensure that, after concurrent operations, the item adheres to the default filter setting.
                        var result = filter.ShouldInclude(item) ? FilterType.Include : FilterType.Exclude;
                        Assert.Equal(filter.Default, result);
                    })
            };

            await Task.WhenAll(tasks);
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

        [Theory]
        [ClassData(typeof(ConcurrentFilterTestData))]
        public async Task CloneAsync_CreatesIndependentAndSameTypeCopyAsync(IFilter<int> filter)
        {
            int item = 1;

            await Task.Run(() => filter.Include(item));

            var clonedFilter = await Task.Run(() => (IFilter<int>)filter.Clone());

            Assert.IsType(filter.GetType(), clonedFilter);

            Assert.True(await Task.Run(() => filter.ShouldInclude(item)));
            Assert.True(await Task.Run(() => clonedFilter.ShouldInclude(item)));

            await Task.Run(() => clonedFilter.Exclude(item));

            Assert.True(await Task.Run(() => filter.ShouldInclude(item)));
            Assert.False(await Task.Run(() => clonedFilter.ShouldInclude(item)));
        }

    }
}
