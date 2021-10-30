using System.Collections.Generic;

namespace AlgorithmCoreVRPTW.Models
{
    public class Problem
    {
        public int Vehicles { get; set; }
        public int Capacity { get; set; }
        public Depot Depot { get; set; }
        public List<Customer> Customers { get; } = new List<Customer>();
    }
}