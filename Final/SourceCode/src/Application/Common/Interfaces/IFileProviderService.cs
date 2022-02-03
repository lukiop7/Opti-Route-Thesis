using OptiRoute.Application.Common.Enums;
using System.IO;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IFileProviderService
    {
        FileInfo[] GetFiles(SolomonFiles fileType);

        FileInfo GetFile(string name, SolomonFiles fileType);
    }
}