using Filter;

namespace Tests
{
    public class ConcurrentFilterTestData : TheoryData<IFilter<int>>
    {
        public ConcurrentFilterTestData()
        {
            Add(new ConcurrentFilter<int>());
        }
    }

}
