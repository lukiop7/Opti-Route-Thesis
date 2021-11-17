using System.Collections.Generic;

namespace AlgorithmCoreVRPTW.Models
{
    public class Problem
    {
        public int Vehicles { get; set; }
        public int Capacity { get; set; }
        public Depot Depot { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<List<double>> Distances { get; set; } = new List<List<double>>();
        public List<List<double>> Durations { get; set; } = new List<List<double>>();
    }
}