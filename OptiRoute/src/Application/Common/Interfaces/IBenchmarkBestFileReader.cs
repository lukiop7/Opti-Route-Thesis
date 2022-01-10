using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IBenchmarkBestFileReader
    {
        List<List<int>> ReadBenchmarkBestFile(string content);
    }
}
