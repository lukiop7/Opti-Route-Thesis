using Microsoft.AspNetCore.Hosting;
using OptiRoute.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Infrastructure.Files
{
    public class SolomonInstancesFileProviderService : IFileProviderService
    {
        private IWebHostEnvironment _environment;

        public SolomonInstancesFileProviderService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public FileInfo[] GetFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(
                _environment.WebRootPath, "solomonInstances"));

            if (!directory.Exists)
                throw new DirectoryNotFoundException();

            return directory.GetFiles("*.txt");
        }
    }
}
