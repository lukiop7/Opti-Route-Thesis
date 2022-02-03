using OptiRoute.Domain.Entities;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IBenchmarkInstanceFileReader
    {
        Problem ReadBenchmarkFile(string content);
    }
}