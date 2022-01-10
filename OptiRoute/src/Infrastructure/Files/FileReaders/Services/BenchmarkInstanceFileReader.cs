using OptiRoute.Application.Common.Exceptions;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Domain.Entities;
using OptiRoute.Infrastructure.Files.FileReaders.BenchmarkTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace OptiRoute.Infrastructure.FileReaders.Services
{
    public class BenchmarkInstanceFileReader : IBenchmarkInstanceFileReader
    {
        private string _errorTemplate = "Line: {0} Error: {1}";
        private Dictionary<int, Action<Problem, string>> _handlersDictionary;

        public BenchmarkInstanceFileReader()
        {
            _handlersDictionary = new Dictionary<int, Action<Problem, string>>()
            {
                {4, ParseVehicle},
                {9, ParseDepot},
                {10, ParseCustomer},
            };
        }

        public Problem ReadBenchmarkFile(string content)
        {
            var dataLines = content.Split("\r\n").ToList();

            if (dataLines.Count < BenchmarkInstanceTemplate.MinimumNumberOfLines)
            {
                throw new ValidationException(new KeyValuePair<string, string[]>("File", new string[]
                { "The number of lines in the file is less than specified in the documentation." }));
            }

            Problem benchmarkProblem = new Problem();

            ProcessData(dataLines, benchmarkProblem);
            CalculateDurationsAndDistances(benchmarkProblem);

            return benchmarkProblem;
        }

        private void ProcessData(List<string> dataLines, Problem problem)
        {
            dataLines = dataLines.Take(dataLines.FindLastIndex(x => !string.IsNullOrEmpty(x)) + 1).ToList();
            for (int i = 0; i < dataLines.Count; i++)
            {
                int index = i <= 10 ? i : 10;
                if (ValidateLine(index, dataLines[i]))
                {
                    if (_handlersDictionary.ContainsKey(index))
                        _handlersDictionary[index].Invoke(problem, dataLines[i]);
                }
                else
                {
                    string message = string.Format(_errorTemplate, i, "Data does not correspond to the format given in the documentation");
                    throw new ValidationException(new KeyValuePair<string, string[]>("File", new string[] { message }));
                }
            }
        }


        private bool ValidateLine(int index, string line)
        {
            return Regex.Match(line, BenchmarkInstanceTemplate.FromBenchmarkTemplate(index), RegexOptions.IgnoreCase).Success;
        }

        private void ParseDepot(Problem problem, string data)
        {
            problem.Depot = Depot.Parse(data);
        }

        private void ParseVehicle(Problem problem, string data)
        {
            var dataSplit = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            problem.Vehicles = Int32.Parse(dataSplit[0]);
            problem.Capacity = Int32.Parse(dataSplit[1]);
        }

        private void ParseCustomer(Problem problem, string data)
        {
            problem.Customers.Add(Customer.Parse(data));
        }
        private void CalculateDurationsAndDistances(Problem benchmarkProblem)
        {
            List<List<double>> distances = new List<List<double>>();
            List<List<double>> durations = new List<List<double>>();

            List<double> depotDistances = new List<double>();
            List<double> depotDurations = new List<double>();
            depotDistances.Add(0);
            depotDurations.Add(0);
            for (int i = 0; i < benchmarkProblem.Customers.Count; i++)
            {
                depotDistances.Add(benchmarkProblem.Customers[i].CalculateDistanceBetween(benchmarkProblem.Depot));
                depotDurations.Add(benchmarkProblem.Customers[i].CalculateDistanceBetween(benchmarkProblem.Depot));
            }
            distances.Add(depotDistances);
            durations.Add(depotDurations);
            foreach (var customer in benchmarkProblem.Customers)
            {
                List<double> customerDistances = new List<double>();
                List<double> customerDurations = new List<double>();
                customerDistances.Add(customer.CalculateDistanceBetween(benchmarkProblem.Depot));
                customerDurations.Add(customer.CalculateDistanceBetween(benchmarkProblem.Depot));
                for (int i = 0; i < benchmarkProblem.Customers.Count; i++)
                {
                    if (benchmarkProblem.Customers[i].Id == customer.Id)
                    {
                        customerDistances.Add(0);
                        customerDurations.Add(0);
                    }
                    else
                    {
                        customerDistances.Add(customer.CalculateDistanceBetween(benchmarkProblem.Customers[i]));
                        customerDurations.Add(customer.CalculateDistanceBetween(benchmarkProblem.Customers[i]));
                    }
                }
                distances.Add(customerDistances);
                durations.Add(customerDurations);
            }
            benchmarkProblem.Distances = distances;
            benchmarkProblem.Durations = durations;
        }
    }
}