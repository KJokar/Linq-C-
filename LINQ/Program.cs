using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqNotebook
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string City { get; set; } = "";
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int MajorId { get; set; }
    }

    class Major
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            DemoDeferredVsImmediate();
            Wait();

            DemoFiltering();
            Wait();

            DemoSorting();
            Wait();

            DemoProjection();
            Wait();

            DemoGrouping();
            Wait();

            DemoJoin();
            Wait();

            DemoAggregation();
            Wait();

            DemoQuantifiers();
            Wait();

            DemoGeneration();
            Wait();

            DemoSetOperators();
            Wait();

            DemoPartitioning();
            Wait();

            DemoElementOperators();
            Wait();

            DemoEquality();
            Wait();

            DemoConversion();
            Wait();

            DemoConcatenation();
            Wait();

            Console.WriteLine("End of LINQ Notebook ✅");
            Console.ReadLine();
        }

        static void Wait()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        // 1) Deferred vs Immediate Execution
        static void DemoDeferredVsImmediate()
        {
            Console.WriteLine("== Deferred vs Immediate Execution ==");

            var numbers = new List<int> { 1, 2, 3 };

            var deferredQuery = numbers.Where(x => x > 1); // Deferred
            var immediateResult = numbers.Where(x => x > 1).ToList(); // Immediate

            numbers.Add(4); // Modify data source

            Console.WriteLine("Deferred (evaluated later):");
            foreach (var n in deferredQuery)
                Console.WriteLine(n); // includes 4

            Console.WriteLine("\nImmediate (evaluated now):");
            foreach (var n in immediateResult)
                Console.WriteLine(n); // excludes 4
        }

        // 2) Filtering (Where)
        static void DemoFiltering()
        {
            Console.WriteLine("== Filtering (Where) ==");

            var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

            var evens = numbers.Where(n => n % 2 == 0);

            Console.WriteLine("Even numbers:");
            foreach (var n in evens)
                Console.WriteLine(n);

            var bigOnes =
                from n in numbers
                where n > 3
                select n;

            Console.WriteLine("\nNumbers > 3 (Query Syntax):");
            foreach (var n in bigOnes)
                Console.WriteLine(n);
        }

        // 3) Sorting (OrderBy / ThenBy)
        static void DemoSorting()
        {
            Console.WriteLine("== Sorting (OrderBy / ThenBy) ==");

            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Ali",  Age = 25, City = "Tehran" },
                new Person { Id = 2, Name = "Sara", Age = 19, City = "Shiraz" },
                new Person { Id = 3, Name = "Reza", Age = 30, City = "Tehran" },
                new Person { Id = 4, Name = "Niloofar", Age = 22, City = "Tabriz" },
            };

            var ordered = people
                .OrderBy(p => p.City)
                .ThenBy(p => p.Age);

            foreach (var p in ordered)
                Console.WriteLine($"{p.City,-8} | {p.Name,-10} | Age: {p.Age}");
        }

        // 4) Projection (Select / SelectMany)
        static void DemoProjection()
        {
            Console.WriteLine("== Projection (Select / SelectMany) ==");

            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Ali",  Age = 25, City = "Tehran" },
                new Person { Id = 2, Name = "Sara", Age = 19, City = "Shiraz" },
                new Person { Id = 3, Name = "Reza", Age = 30, City = "Tehran" },
            };

            var names = people.Select(p => p.Name);

            Console.WriteLine("Names only (Select):");
            foreach (var name in names)
                Console.WriteLine(name);

            var info = people.Select(p => new
            {
                Full = $"{p.Name} ({p.Age})",
                p.City
            });

            Console.WriteLine("\nAnonymous Projection:");
            foreach (var x in info)
                Console.WriteLine($"{x.Full} - {x.City}");

            var classes = new[]
            {
                new { ClassName = "A", Students = new[] { "Ali", "Sara" } },
                new { ClassName = "B", Students = new[] { "Reza", "Niloofar" } }
            };

            var allStudents = classes.SelectMany(c => c.Students);

            Console.WriteLine("\nSelectMany (All students):");
            foreach (var s in allStudents)
                Console.WriteLine(s);
        }

        // 5) Grouping (GroupBy)
        static void DemoGrouping()
        {
            Console.WriteLine("== Grouping (GroupBy) ==");

            var people = new List<Person>
            {
                new Person { Id = 1, Name = "Ali",  Age = 25, City = "Tehran" },
                new Person { Id = 2, Name = "Sara", Age = 19, City = "Shiraz" },
                new Person { Id = 3, Name = "Reza", Age = 30, City = "Tehran" },
                new Person { Id = 4, Name = "Niloofar", Age = 22, City = "Shiraz" },
            };

            var groups = people.GroupBy(p => p.City);

            foreach (var g in groups)
            {
                Console.WriteLine($"City: {g.Key} (Count: {g.Count()})");
                foreach (var p in g)
                    Console.WriteLine($"  - {p.Name} ({p.Age})");
            }
        }

        // 6) Join
        static void DemoJoin()
        {
            Console.WriteLine("== Join ==");

            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Ali",  MajorId = 1 },
                new Student { Id = 2, Name = "Sara", MajorId = 2 },
                new Student { Id = 3, Name = "Reza", MajorId = 1 },
            };

            var majors = new List<Major>
            {
                new Major { Id = 1, Title = "Computer Science" },
                new Major { Id = 2, Title = "Mathematics" }
            };

            var query = students.Join(
                majors,
                s => s.MajorId,
                m => m.Id,
                (s, m) => new { s.Name, Major = m.Title });

            foreach (var item in query)
                Console.WriteLine($"{item.Name} - {item.Major}");
        }

        // 7) Aggregation
        static void DemoAggregation()
        {
            Console.WriteLine("== Aggregation (Count / Sum / Average / Min / Max / Aggregate) ==");

            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            Console.WriteLine($"Count: {numbers.Count()}");
            Console.WriteLine($"Sum: {numbers.Sum()}");
            Console.WriteLine($"Average: {numbers.Average()}");
            Console.WriteLine($"Min: {numbers.Min()}");
            Console.WriteLine($"Max: {numbers.Max()}");

            int product = numbers.Aggregate((total, next) => total * next);
            Console.WriteLine($"Product (Aggregate): {product}");
        }

        // 8) Quantifiers
        static void DemoQuantifiers()
        {
            Console.WriteLine("== Quantifiers (Any / All / Contains) ==");

            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            Console.WriteLine($"Any? {numbers.Any()}");
            Console.WriteLine($"Any > 3? {numbers.Any(n => n > 3)}");
            Console.WriteLine($"All > 0? {numbers.All(n => n > 0)}");
            Console.WriteLine($"Contains(3)? {numbers.Contains(3)}");
        }

        // 9) Generation
        static void DemoGeneration()
        {
            Console.WriteLine("== Generation (Range / Repeat / Empty) ==");

            var range = Enumerable.Range(1, 5);
            Console.WriteLine("Range 1..5: " + string.Join(", ", range));

            var repeat = Enumerable.Repeat("C#", 3);
            Console.WriteLine("Repeat \"C#\" 3 times: " + string.Join(", ", repeat));

            var empty = Enumerable.Empty<int>();
            Console.WriteLine($"Empty<int>() Count: {empty.Count()}");
        }

        // 10) Set Operators
        static void DemoSetOperators()
        {
            Console.WriteLine("== Set Operators (Distinct / Union / Intersect / Except) ==");

            var a = new[] { 1, 2, 2, 3, 4 };
            var b = new[] { 3, 4, 5, 6 };

            Console.WriteLine("Distinct(a): " + string.Join(", ", a.Distinct()));
            Console.WriteLine("Union: " + string.Join(", ", a.Union(b)));
            Console.WriteLine("Intersect: " + string.Join(", ", a.Intersect(b)));
            Console.WriteLine("Except (a - b): " + string.Join(", ", a.Except(b)));
        }

        // 11) Partitioning
        static void DemoPartitioning()
        {
            Console.WriteLine("== Partitioning (Take / Skip / TakeWhile / SkipWhile) ==");

            var numbers = new[] { 1, 2, 3, 0, 4, 5 };

            Console.WriteLine("Take(3): " + string.Join(", ", numbers.Take(3)));
            Console.WriteLine("Skip(2): " + string.Join(", ", numbers.Skip(2)));
            Console.WriteLine("TakeWhile(n > 0): " + string.Join(", ", numbers.TakeWhile(n => n > 0)));
            Console.WriteLine("SkipWhile(n < 3): " + string.Join(", ", numbers.SkipWhile(n => n < 3)));
        }

        // 12) Element Operators
        static void DemoElementOperators()
        {
            Console.WriteLine("== Element Operators ==");

            var numbers = new List<int> { 10, 20, 30, 40 };

            Console.WriteLine($"First: {numbers.First()}");
            Console.WriteLine($"First > 15: {numbers.First(n => n > 15)}");
            Console.WriteLine($"Last: {numbers.Last()}");

            var empty = new List<int>();
            Console.WriteLine($"FirstOrDefault (empty): {empty.FirstOrDefault()}");

            var singleList = new List<int> { 42 };
            Console.WriteLine($"Single: {singleList.Single()}");

            Console.WriteLine($"ElementAt(2): {numbers.ElementAt(2)}");
        }

        // 13) Equality
        static void DemoEquality()
        {
            Console.WriteLine("== Equality (SequenceEqual) ==");

            var a = new[] { 1, 2, 3 };
            var b = new[] { 1, 2, 3 };
            var c = new[] { 3, 2, 1 };

            Console.WriteLine($"a vs b: {a.SequenceEqual(b)}"); // true
            Console.WriteLine($"a vs c: {a.SequenceEqual(c)}"); // false
        }

        // 14) Conversion
        static void DemoConversion()
        {
            Console.WriteLine("== Conversion (ToList / ToArray / ToDictionary / Cast / OfType) ==");

            var numbers = Enumerable.Range(1, 5);

            var list = numbers.ToList();
            var array = numbers.ToArray();

            Console.WriteLine("List: " + string.Join(", ", list));
            Console.WriteLine("Array: " + string.Join(", ", array));

            var people = new[]
            {
                new Person { Id = 1, Name = "Ali", Age = 20 },
                new Person { Id = 2, Name = "Sara", Age = 25 }
            };

            var dict = people.ToDictionary(p => p.Id, p => p.Name);
            Console.WriteLine("\nToDictionary (Id -> Name):");
            foreach (var kv in dict)
                Console.WriteLine($"{kv.Key} => {kv.Value}");

            object[] mixed = { 1, "Ali", 2.5, 3, "Sara" };

            var ints = mixed.OfType<int>();
            Console.WriteLine("\nOfType<int> from mixed:");
            Console.WriteLine(string.Join(", ", ints));

            IEnumerable<object> objs = new object[] { 1, 2, 3 };
            var casted = objs.Cast<int>();
            Console.WriteLine("\nCast<int>:");
            Console.WriteLine(string.Join(", ", casted));
        }

        // 15) Concatenation
        static void DemoConcatenation()
        {
            Console.WriteLine("== Concatenation (Concat / Append / Prepend) ==");

            var a = new[] { 1, 2, 3 };
            var b = new[] { 4, 5 };

            var concat = a.Concat(b);
            Console.WriteLine("Concat: " + string.Join(", ", concat));

            var appended = a.Append(99);
            Console.WriteLine("Append 99: " + string.Join(", ", appended));

            var prepended = a.Prepend(0);
            Console.WriteLine("Prepend 0: " + string.Join(", ", prepended));
        }
    }
}
