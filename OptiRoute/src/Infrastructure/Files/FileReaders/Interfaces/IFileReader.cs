using AlgorithmCoreVRPTW.Models;

namespace AlgorithmCoreVRPTW.FileReaders.Interfaces
{
    public interface IFileReader
    {
        Problem ReadBenchmarkFile(string filePath);
    }
}