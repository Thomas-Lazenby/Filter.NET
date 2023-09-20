﻿using Filter;

namespace Tests
{
    public class FilterTestData : TheoryData<IFilter<int>>
    {
        public FilterTestData()
        {
            Add(new Filter<int>());
            Add(new ConcurrentFilter<int>());
        }
    }

}
