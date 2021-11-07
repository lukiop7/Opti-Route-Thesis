using System;
using System.Collections.Generic;

namespace AlgorithmCoreVRPTW.Models
{
    public class Customer
    {
        public static Customer Parse(string input)
        {
            var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Customer()
            {
                Id = Int32.Parse(parts[0]),
                X = Int32.Parse(parts[1]),
                Y = Int32.Parse(parts[2]),
                Demand = Int32.Parse(parts[3]),
                ReadyTime = Int32.Parse(parts[4]),
                DueDate = Int32.Parse(parts[5]),
                ServiceTime = Int32.Parse(parts[6])
            };
        }

        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Demand { get; set; }
        public int ReadyTime { get; set; }
        public int DueDate { get; set; }
        public int ServiceTime { get; set; }
        public double DepotDistanceTo { get; set; }
        public double DepotTimeTo { get; set; }
        public double DepotDistanceFrom { get; set; }
        public double DepotTimeFrom { get; set; }

        public double CalculateDistanceBetween(List<List<double>> distances, Customer destination)
        {
            return distances[this.Id][destination.Id];
        }

        public double CalculateDistanceBetween(List<List<double>> distances, Depot destination)
        {
            // depot
            return distances[this.Id][0];
        }

        public double CalculateTimeBetween(List<List<double>> durations, Customer destination)
        {
            return durations[this.Id][destination.Id];
        }

        public double CalculateTimeBetween(List<List<double>> durations, Depot destination)
        {
            // depot
            return durations[this.Id][0];
        }

        public void CalculateDepotTimesAndDistances(List<List<double>> distances, List<List<double>> durations, Depot destination)
        {
            this.DepotDistanceTo = distances[this.Id][0];
            this.DepotDistanceFrom = distances[0][this.Id];
            this.DepotTimeTo = durations[this.Id][0];
            this.DepotTimeFrom = durations[0][this.Id];
        }

        public double CalculateDistanceBetween(Customer destination)
        {
            return Math.Sqrt((Math.Pow(this.X - destination.X, 2) + Math.Pow(this.Y - destination.Y, 2)));
        }

        public double CalculateDistanceBetween(Depot destination)
        {
            return Math.Sqrt((Math.Pow(this.X - destination.X, 2) + Math.Pow(this.Y - destination.Y, 2)));
        }
    }
}