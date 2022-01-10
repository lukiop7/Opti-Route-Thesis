using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IBenchmarkInstanceFileReader
    {
        Problem ReadBenchmarkFile(string content);
    }
}
