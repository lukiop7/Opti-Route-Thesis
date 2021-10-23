using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.Models
{
    public class Saving
    {
        public Customer A { get; set; }
        public Customer B { get; set; }
        public double Value { get; set; }

        public void CalculateSaving(Depot depot)
        {
            Value = A.CalculateDistanceBetween(depot) + B.CalculateDistanceBetween(depot) - A.CalculateDistanceBetween(B);
        }
    }
}
