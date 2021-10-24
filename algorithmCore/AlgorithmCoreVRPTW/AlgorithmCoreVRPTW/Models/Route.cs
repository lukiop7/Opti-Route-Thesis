using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmCoreVRPTW.Models
{
   public class Route
    {
        public int Id { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public double Distance { get; set; } = 0;
        public double DistanceToDepot 
        {
            get 
            {
                return this.Customers.Last().CalculateDistanceBetween(this.Depot);
            }
        }
        public double DistanceFromDepot
        {
            get
            {
                return this.Customers.First().CalculateDistanceBetween(this.Depot);
            }
        }
        public Vehicle Vehicle { get; set; }
        public Depot Depot { get; set; }
        public void AddCustomer(Customer customer)
        {
            double distance;
            double lastDistanceToDepot;
            double distanceToDepot;
            if (Customers.Count == 0)
            {
                distanceToDepot = customer.CalculateDistanceBetween(this.Depot);
                distance = 2 * distanceToDepot;
            }
            else
            {
                distanceToDepot = customer.CalculateDistanceBetween(this.Depot);
                distance = customer.CalculateDistanceBetween(this.Customers.Last());
                lastDistanceToDepot = this.Customers.Last().CalculateDistanceBetween(this.Depot);

                distance += distanceToDepot - lastDistanceToDepot;
            }
            this.Customers.Add(customer);

            this.Distance += distance;
            this.Vehicle.CurrentLoad += customer.Demand;
            this.Vehicle.CurrentTime += distance + customer.ServiceTime;
        }

        public void MergeRoutes(Route routeToMerge, double distanceBetween)
        {
            this.Distance = (this.Distance - DistanceToDepot) + (routeToMerge.Distance - routeToMerge.DistanceFromDepot) + distanceBetween;
            this.Vehicle.CurrentTime = (this.Vehicle.CurrentTime - DistanceToDepot) + (routeToMerge.Vehicle.CurrentTime - routeToMerge.DistanceFromDepot) + distanceBetween;
            this.Vehicle.CurrentLoad += routeToMerge.Vehicle.CurrentLoad;

            this.Customers.AddRange(routeToMerge.Customers);
        }

    }
}
