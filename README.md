![Logo](https://socialify.git.ci/Thomas-Lazenby/Filter.NET/image?font=Inter&forks=1&issues=1&logo=https%3A%2F%2Fsvgur.com%2Fi%2Fxmj.svg&name=1&pattern=Signal&pulls=1&theme=Light)

## Overview

Filter.NET is a versatile and performant library designed to facilitate advanced filtering operations on collections of items in .NET. It provides a simple yet powerful way to define inclusion and exclusion rules, making it a handy tool for a wide range of applications such as data analysis, information retrieval, and many others.

## Features

- **Generic Filtering**: Easily create filters for collections of any data type that implements `IEquatable<T>`.
- **Fluent Interface**: Intuitive methods like `Include`, `Exclude`, and `ShouldInclude` allow for the fluent construction of filter criteria.
- **High Performance**: Optimized for performance, ensuring fast filtering even on large datasets.
- **Asynchronous Support**: The library offers asynchronous filtering capabilities, enabling non-blocking operations which are especially beneficial in IO-bound scenarios.
- **Extensible**: Open for extension, you can create your custom filters by implementing the `IFilter<T>` interface.

## Basic Usage

### Basics

```csharp
var filter = new Filter<int>(); // All values are filtered as included unless changed by Default.

filter.Include(2, 4, 6, 8)
    .Exclude(1, 3, 5, 7);

bool isFiveIncluded = filter.IsIncluded(5); // false
bool isTenIncluded = filter.IsIncluded(10); // true, Default is Included by default if not set.

bool isFiveExcluded = filter.IsExcluded(5); // true
bool isTendExcluded = filter.IsExcluded(10); // false, Default is Included by default if not set.
```

### Operations
```csharp
var filter = new Filter<int>();
var filter2 = new ConcurrentFilter<int>();

// Clearing all override options and reverting to the default behavior
filter.Clear();

// Checking if equivalent
bool equivalent = filter.Equals(filter2); // true.
```

### Explicity
```csharp
var filter = new Filter<int>()
    .Include(2, 3, 5)
    .Exclude(4);

bool containsExplicitIncluded1 = filter.AnyExplicitIncluded(4, 9); // false
bool containsExplicitIncluded2 = filter.AnyExplicitIncluded(5, 231); // true

bool containsExplicitExcluded1 = filter.AnyExplicitExcluded(4, 9); // true
bool containsExplicitExcluded2 = filter.AnyExplicitExcluded(5, 231); // false



bool containsAtAllInIncludedOrExcludedExplicitly = filter.ContainsExplicitly(2); // true;

IEnumerable<int> explicitIncludedItems = filter.ExplicitIncludedItems; // list of explicitly included items.
IEnumerable<int> explicitExcludedItems = filter.ExplicitExcludedItems; // list of explicitly excluded items.
```

# Installation

TODO

# Contribution

Contributions are welcome to the project. Feel free to open an issue or create a pull request.

# License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/Thomas-Lazenby/Filter.NET/blob/main/LICENSE) file for details.
