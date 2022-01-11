using System.Collections.Generic;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IBenchmarkBestFileReader
    {
        List<List<int>> ReadBenchmarkBestFile(string content);
    }
}