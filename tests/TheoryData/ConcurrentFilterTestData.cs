using Filter;

namespace Filter.Tests
{
    public class ConcurrentFilterTestData : TheoryData<IFilter<int>>
    {
        public ConcurrentFilterTestData()
        {
            Add(new ConcurrentFilter<int>());
        }
    }

}
