using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ReflectionConsole;

[MemoryDiagnoser, Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ReflectionBenchmarks
{
    [Benchmark]
    public string GetSimple() => ReflectionUsage.GetSimple();

    [Benchmark]
    public string GetDefaultValue() => ReflectionUsage.GetDefaultValue();

    [Benchmark]
    public string TraditionalReflection() => ReflectionUsage.TraditionalReflection();

    [Benchmark]
    public string OptimizedTraditionalReflection() => ReflectionUsage.OptimizedTraditionalReflection();

    [Benchmark]
    public string CompiledDelegate() => ReflectionUsage.CompiledDelegate();

    [Benchmark]
    public string Emitted_IL_Version() => ReflectionUsage.Emitted_IL_Version();
}