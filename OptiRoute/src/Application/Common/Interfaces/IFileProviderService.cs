using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IFileProviderService
    {
        FileInfo[] GetFiles();
    }
}
