using Microsoft.EntityFrameworkCore;
using OptiRoute.Modules.Main.Domain.Interfaces;
using OptiRoute.Shared.Data.Models;
using OptiRoute.Shared.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Modules.Main.Infrastructure.Repositories
{
    public class TestRepository : BaseCrudRepository<Test, d74iruuie0tpfkContext>, ITestRepository
    {
        public TestRepository(d74iruuie0tpfkContext context) : base(context)
        {

        }

        public async Task<Test> GetTask()
        {
            return await DbSet.FirstOrDefaultAsync();
        }

        public async Task<Test> AddTestAsync(Test newTest)
        {
            await base.AddAsync(newTest);
            await Context.SaveChangesAsync();
            return await Context.Tests.FirstOrDefaultAsync(t => t.Name == newTest.Name);           
        }
    }
}
