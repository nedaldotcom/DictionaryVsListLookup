## Dictionary vs. List Lookup Benchmark

This benchmark compares the performance of two common lookup strategies in C#:

- `List.FirstOrDefault()` – linear search through a list  
- `Dictionary.TryGetValue()` – hash-based key lookup

These techniques are frequently used in real-world scenarios such as enriching API response data, mapping configurations, or merging data from different sources.

### Use Case

Suppose you're building an application that displays a list of users, and each user needs to be associated with additional metadata like role information or display preferences (e.g. theme color, avatar). This metadata is stored in a predefined configuration list.

To associate each user with their corresponding metadata during runtime, you can either:
- Loop through the list for every user (`List.FirstOrDefault`)
- Use a dictionary to instantly look up metadata by user ID (`Dictionary.TryGetValue`)

This benchmark demonstrates the significant performance difference between the two approaches, helping you choose the optimal structure when scaling up to thousands of users.

---

## Benchmark Results

| Method           | Mean         | Error      | StdDev     | Gen0   | Gen1   | Allocated |
|------------------|-------------:|-----------:|-----------:|-------:|-------:|----------:|
| ListLookup       | 2,771.182 µs | 21.3055 µs | 18.8868 µs | 7.8125 |      - | 102.15 KB |
| DictionaryLookup |     5.272 µs |  0.0809 µs |  0.0717 µs | 1.3199 | 0.0381 |  16.21 KB |

**Result:** `Dictionary` is over **500x faster** and significantly more memory-efficient than list-based lookup.

---

## Project Highlights

- Written in C# using [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
- Shows practical, real-world use case
- Helps reinforce efficient data access patterns in large-scale systems
