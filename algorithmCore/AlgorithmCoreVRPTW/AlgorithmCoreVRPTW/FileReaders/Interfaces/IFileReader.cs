using AlgorithmCoreVRPTW.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.FileReaders.Interfaces
{
   public interface IFileReader
    {
        Problem ReadBenchmarkFile(string filePath);
    }
}
