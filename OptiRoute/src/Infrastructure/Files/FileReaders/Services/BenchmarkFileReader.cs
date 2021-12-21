using AlgorithmCoreVRPTW.FileReaders.Interfaces;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmCoreVRPTW.FileReaders.Services
{
    public class BenchmarkFileReader : IFileReader
    {
        public Problem ReadBenchmarkFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var data = File.ReadAllText(filePath);
                bool isValid = ValidateDataFormat(data);
                if (isValid)
                {
                    var dataLines = data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                    return ParseInputData(dataLines);
                }
                throw new InvalidDataException();
            }

            throw new FileNotFoundException();
        }

        public Problem ReadBenchmark(string content)
        {
            bool isValid = ValidateDataFormat(content);
            if (isValid)
            {
                var dataLines = content.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
                var problem = ParseInputData(dataLines);
                CalculateDurationsAndDistances(problem);
                return problem;
                }
            throw new InvalidDataException();
        }


        private bool ValidateDataFormat(string data)
        {
            string pattern = @"(.+(\n|\r|\r\n).*(\n|\r|\r\n)(VEHICLE)(\n|\r|\r\n)(NUMBER\s+CAPACITY)(\n|\r|\r\n)(\s+\d+\s+\d+)(\n|\r|\r\n)(\n|\r|\r\n)(CUSTOMER)(\n|\r|\r\n)
(CUST NO\.\s+XCOORD\.\s+YCOORD\.\s+DEMAND\s+READY\s+TIME\s+DUE\s+DATE\s+SERVICE\s+TIME)(\n|\r|\r\n)\s+(\n|\r|\r\n)
((\s+\d+){7}((\n|\r|\r\n)|$)){2,})";
            //return Regex.Match(data, pattern).Success;
            return true;
        }

        private Problem ParseInputData(List<string> dataLines)
        {
            // tu to trzeba dobrze sprawdzic bo sie wywalaja randomowe ilosci linijek
            var vehicleLine = dataLines[3].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string depotLine = dataLines[7];
            var customersLines = dataLines.Skip(8);

            Depot depot = Depot.Parse(depotLine);
            Problem problem = new Problem()
            {
                Depot = depot,
                Vehicles = Int32.Parse(vehicleLine[0]),
                Capacity = Int32.Parse(vehicleLine[1]),
            };

            foreach (var customer in customersLines)
            {
                problem.Customers.Add(Customer.Parse(customer));
            }

            return problem;
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