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

### Filter

```csharp
// Creating a new Filter instance with a default setting of Include
var filter = new Filter<int> { Default = FilterType.Include };

// Including and Excluding specific items
filter.Include(5);
filter.Exclude(10);

// Testing the filter
bool isFiveIncluded = filter.ShouldInclude(5);  // true
bool isTenIncluded = filter.ShouldInclude(10); // false

// Clearing all override options and reverting to the default behavior
filter.Clear();
bool isDefaultBehaviourRestored = filter.ShouldInclude(5);  // true (Default)

// Cloning the filter
var newFilter = (Filter<int>)filter.Clone();
```

### ConcurrentFilter

```csharp
// Creating a new ConcurrentFilter instance with a default setting of Include
var concurrentFilter = new ConcurrentFilter<int> { Default = FilterType.Include };

// Including and Excluding specific items concurrently
// (Assuming thread-safe operations are implemented)
concurrentFilter.Include(5);
concurrentFilter.Exclude(10);

// Testing the concurrentFilter
bool isFiveIncludedConcurrently = concurrentFilter.ShouldInclude(5);  // true
bool isTenIncludedConcurrently = concurrentFilter.ShouldInclude(10); // false
```

# Installation

TODO

# Contribution

Contributions are welcome to the project. Feel free to open an issue or create a pull request.

# License

This project is licensed under the MIT License. See the LICENSE file for details.
