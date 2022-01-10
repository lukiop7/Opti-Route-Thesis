using OptiRoute.Application.Common.Exceptions;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Infrastructure.Files.FileReaders.BenchmarkTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptiRoute.Infrastructure.Files.FileReaders.Services
{
    public class BenchmarkBestFileReader : IBenchmarkBestFileReader
    {
        private string _errorTemplate = "Line: {0} Error: {1}";

        public List<List<int>> ReadBenchmarkBestFile(string content)
        {
            var dataLines = content.Split("\r\n").ToList();

            if (dataLines.Count < BenchmarkBestTemplate.MinimumNumberOfLines)
            {
                throw new ValidationException(new KeyValuePair<string, string[]>("File", new string[]
                { "The number of lines in the file is less than specified in the documentation." }));
            }

            return ProcessData(dataLines);
        }

        private List<List<int>> ProcessData(List<string> dataLines)
        {
            var routeLines = dataLines.Skip(5).ToList();
            routeLines = routeLines.Take(routeLines.FindLastIndex(x => !string.IsNullOrEmpty(x)) + 1).ToList();
            List<List<int>> routes = new List<List<int>>();
            for (int i = 0; i < routeLines.Count(); i++)
            {
                if (!ValidateLine(i, routeLines[i]))
                {
                    string message = string.Format(_errorTemplate, i, "Data does not correspond to the format given in the documentation");
                    throw new ValidationException(new KeyValuePair<string, string[]>("File", new string[] { message }));
                }

                routes.Add(ParseRoute(routeLines[i]));
            }

            return routes;
        }

        private bool ValidateLine(int index, string line)
        {
            string pattern = @"^\s*Route\s*\d+\s*:(\s*\d+)+\s*$";
            return Regex.Match(line, pattern, RegexOptions.IgnoreCase).Success;
        }

        private List<int> ParseRoute(string line)
        {
            var splitted = line.Split(new char[] { ':' });
            var customers = splitted[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            return customers.Select(x => int.Parse(x)).ToList();
        }
    }
}