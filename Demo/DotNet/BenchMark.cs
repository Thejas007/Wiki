using System.Collections.Generic;
using System.Linq;

namespace BenchMark
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestData1>();
            var summary2 = BenchmarkRunner.Run<TestData2>();
        }


    }

    [MemoryDiagnoser()]
    public class TestData1
    {
        public IEnumerable<Data> Getdata()
        {
            var list = new List<Data>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Data() { Id = i, Value = i.ToString() });
            }
            
            return list;
        }

        [Benchmark]
        public void TestImplicitList()
        {
            var data = Getdata();
            var a = (List<Data>)data;

            for (int i = 0; i < 1000; i++)
            {
                a.Add(new Data() { Id = i, Value = i.ToString() });
            }

            foreach (var values in a)
            {
                Console.Write(values.Id);
            }
        }

    }

    [MemoryDiagnoser()]
    public class TestData2
    {
        public IList<Data> GetList()
        {
            var list = new List<Data>();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(new Data() { Id = i, Value = i.ToString() });
            }

            return list;
        }

        [Benchmark]
        public void TestToList()
        {
            var data = GetList();
            var a = data.ToList();

            for (int i = 0; i < 1000; i++)
            {
                a.Add(new Data() { Id = i, Value = i.ToString() });
            }

            foreach (var values in a)
            {
                Console.Write(values.Id);
            }
        }
    }


    public class Data
    {

        public int Id { get; set; }

        public string Value { get; set; }
    }
}
