// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ReflectionConsole;

BenchmarkRunner.Run<ReflectionBenchmarks>();

var value = ReflectionUsage.GetDefaultValue();
Console.WriteLine(value);
