using Xunit;
using Filter;  // Assuming Filter namespace contains the Filter<int> class definition

namespace Tests
{
    public class CombinedFilterPairTestData : TheoryData<IFilter<int>, IFilter<int>>
    {
        public CombinedFilterPairTestData()
        {
            var filters = new List<IFilter<int>>
            {
                new ConcurrentFilter<int>(),
                new Filter<int>()
            };

            // Combinations
            foreach (var filter1 in filters)
            {
                foreach (var filter2 in filters)
                {
                    Add(filter1, filter2);
                }
            }
        }
    }
}
