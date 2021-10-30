using System;

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
        public double DepotDistance { get; set; }

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