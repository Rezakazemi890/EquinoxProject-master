using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Equinox.Core.Test.TestUtils;
[Collection("Non-Parallel Collection")]
[CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
    IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
        foreach (TTestCase testCase in testCases)
        {
            int priority = testCase.TestMethod.Method
                .GetCustomAttributes(assemblyName)
                .FirstOrDefault()
                ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

            GetOrCreate(sortedMethods, priority).Add(testCase);
        }

        foreach (TTestCase testCase in
            sortedMethods.Keys.SelectMany(
                priority => sortedMethods[priority].OrderBy(
                    testCase => testCase.TestMethod.Method.Name)))
        {
            yield return testCase;
        }
    }

    private static TValue GetOrCreate<TKey, TValue>(
        IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : struct
        where TValue : new() =>
        dictionary.TryGetValue(key, out TValue result)
            ? result
            : (dictionary[key] = new TValue());
    // public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    // {
    //     var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

    //     foreach (TTestCase testCase in testCases)
    //     {
    //         int priority = 0;

    //         foreach (IAttributeInfo attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(TestPriorityAttribute).AssemblyQualifiedName)))
    //             priority = attr.GetNamedArgument<int>("Priority");

    //         GetOrCreate(sortedMethods, priority).Add(testCase);
    //     }

    //     foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
    //     {
    //         list.Sort((x, y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
    //         foreach (TTestCase testCase in list)
    //             yield return testCase;
    //     }
    // }

    // static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
    // {
    //     TValue result;

    //     if (dictionary.TryGetValue(key, out result)) return result;

    //     result = new TValue();
    //     dictionary[key] = result;

    //     return result;
    // }
}