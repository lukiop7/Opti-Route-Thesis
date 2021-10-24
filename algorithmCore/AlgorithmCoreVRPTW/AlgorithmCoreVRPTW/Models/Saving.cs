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
        public double DistanceBetween { get; set; }

        public void CalculateSaving(Depot depot)
        {
            DistanceBetween = A.CalculateDistanceBetween(B);
            Value = A.CalculateDistanceBetween(depot) + B.CalculateDistanceBetween(depot) - DistanceBetween;
        }
    }
}
