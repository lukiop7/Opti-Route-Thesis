using System;

namespace OptiRoute.Infrastructure.Files.FileReaders.BenchmarkTemplate
{
    public class BenchmarkInstanceTemplate
    {
        public string FromBenchmarkTemplate(int line) => line switch
        {
            0 => @"^\s*\S+\s*$",
            1 => @"^\s*$",
            2 => @"^\s*VEHICLE\s*$",
            3 => @"^\s*NUMBER\s+CAPACITY\s*$",
            4 => @"^\s*\d+\s+\d+\s*$",
            5 => @"^\s*$",
            6 => @"^\s*CUSTOMER$\s*",
            7 => @"^\s*CUST\s+NO.\s+XCOORD.\s+YCOORD.\s+DEMAND\s+READY TIME\s+DUE\s+DATE\s+SERVICE\s+TIME\s*$",
            8 => @"^\s*$",
            9 => @"^\s*\d+\s+\d+\s+\d+\s+\d+\s+\d+\s+\d+\s+\d+\s*$",
            10 => @"^\s*\d+\s+\d+\s+\d+\s+\d+\s+\d+\s+\d+\s+\d+\s*$",
            _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(line))
        };

        public int MinimumNumberOfLines;

        public BenchmarkInstanceTemplate()
        {
            MinimumNumberOfLines = 11;
        }
    }
}