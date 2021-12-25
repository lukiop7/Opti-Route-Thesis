
using OptiRoute.Domain.Entities;

namespace AlgorithmCoreVRPTW.FileReaders.Interfaces
{
    public interface IFileReader
    {
        Problem ReadBenchmarkFile(string filePath);
        Problem ReadBenchmark(string content);
    }
}