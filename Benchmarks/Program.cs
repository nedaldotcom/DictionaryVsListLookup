using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<LookupBenchmark>();
        }
    }

    [MemoryDiagnoser]
    public class LookupBenchmark
    {
        private const int ItemCount = 10000;
        private const int LookupCount = 1000;
        private List<Item> items;
        private List<int> queries;
        private Dictionary<int, string> itemDictionary;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            items = Enumerable.Range(0, ItemCount)
                .Select(i => new Item { Id = i, Value = $"Value_{i}" })
                .ToList();

            queries = Enumerable.Range(0, LookupCount)
                .Select(_ => random.Next(0, ItemCount))
                .ToList();

            itemDictionary = items.ToDictionary(item => item.Id, item => item.Value);
        }

        [Benchmark]
        public List<string> ListLookup()
        {
            var results = new List<string>();
            foreach (var id in queries)
            {
                var match = items.FirstOrDefault(item => item.Id == id);
                if (match != null)
                    results.Add(match.Value);
            }
            return results;
        }

        [Benchmark]
        public List<string> DictionaryLookup()
        {
            var results = new List<string>();
            foreach (var id in queries)
            {
                if (itemDictionary.TryGetValue(id, out var value))
                    results.Add(value);
            }
            return results;
        }

        public class Item
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
    }
}