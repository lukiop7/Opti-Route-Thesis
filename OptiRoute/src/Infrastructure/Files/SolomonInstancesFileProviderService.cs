using Microsoft.AspNetCore.Hosting;
using OptiRoute.Application.Common.Enums;
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

        public FileInfo GetFile(string name, SolomonFiles fileType)
        {
            var files = GetFiles(fileType);

            var file = files.FirstOrDefault(x => x.Name.Equals(name));

            if (file == null)
                throw new FileNotFoundException(name);

            return file;
        }

        public FileInfo[] GetFiles(SolomonFiles fileType)
        {

            DirectoryInfo directory = new DirectoryInfo(Path.Combine(
                _environment.WebRootPath, GetPath(fileType)));

            if (!directory.Exists)
                throw new DirectoryNotFoundException();

            return directory.GetFiles("*.txt");
        }

        private string GetPath(SolomonFiles fileType) =>
            fileType switch
            {
                SolomonFiles.Instance => "solomonInstances",
                SolomonFiles.Best => "solomonBests",
                _ => throw new ArgumentException()
            };      
    }
}
