using OptiRoute.Shared.Abstractions.Repositories;
using OptiRoute.Shared.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Domain.Interfaces
{
    public interface ITestRepository
    {
        Task<Test> GetTask();
        Task<Test> AddTestAsync(Test newTest);
    }
}
