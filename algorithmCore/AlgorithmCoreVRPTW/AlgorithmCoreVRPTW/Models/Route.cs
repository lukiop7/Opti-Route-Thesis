using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmCoreVRPTW.Models
{
   public class Route
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public double Distance { get; set; } = 0;
        public Vehicle Vehicle { get; set; }
        public Depot Depot { get; set; }
        public void AddCustomer(Customer customer)
        {
            double distance;
            if(Customers.Count==0)
                distance = customer.CalculateDistanceBetween(this.Depot);
            else
                distance = customer.CalculateDistanceBetween(this.Customers.Last());


            this.Distance += distance;
            this.Vehicle.CurrentLoad += customer.Demand;
            this.Vehicle.CurrentTime += distance + customer.ServiceTime;
            this.Customers.Add(customer);
        }
    }
}
