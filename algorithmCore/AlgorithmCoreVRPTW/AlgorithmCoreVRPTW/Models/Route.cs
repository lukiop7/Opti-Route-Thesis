using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreVRPTW.Models
{
    public class Route
    {
        private double _distance = 0;
        public int Id { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public double TotalDistance
        {
            get
            {
                return _distance + DistanceToDepot + DistanceFromDepot;
            }
        }

        public double CustomersDistance
        {
            get
            {
                return _distance;
            }
            set
            {
                this._distance = value;
            }
        }

        public double DistanceToDepot
        {
            get
            {
                if (this.Customers.Count == 0)
                    return 0;

                return this.Customers.Last().CalculateDistanceBetween(this.Depot);
            }
        }

        public double DistanceFromDepot
        {
            get
            {
                if (this.Customers.Count == 0)
                    return 0;

                return this.Customers.First().CalculateDistanceBetween(this.Depot);
            }
        }

        public Vehicle Vehicle { get; set; }
        public Depot Depot { get; set; }

        public Route Clone()
        {
            return new Route
            {
                Id = this.Id,
                Customers = new List<Customer>(this.Customers),
                CustomersDistance = this.CustomersDistance,
                Depot = this.Depot,
                Vehicle = this.Vehicle.Clone()
            };
        }

        //public bool ValidateDistance()
        //{
        //    double dist = 0;
        //    for(int i=0; i< Customers.Count-1; i++)
        //    {
        //        dist += Customers[i].CalculateDistanceBetween(Customers[i+1]);
        //    }
        //    return System.Math.Round(dist - CustomersDistance, 14) <= 0;
        //}

        public void AddCustomer(Customer customer)
        {
            if (Customers.Count > 0)
            {
                CustomersDistance += customer.CalculateDistanceBetween(this.Customers.Last());
            }
            this.Customers.Add(customer);

            this.Vehicle.CurrentLoad += customer.Demand;
            this.Vehicle.CurrentTime = this.TotalDistance + customer.ServiceTime;
        }

        public void AddCustomer(Customer customer, int index)
        {
            if (Customers.Count > 0)
            {
                var indexCustomer = Customers[index];
                if (IsInterior(indexCustomer) || index == Customers.Count - 1)
                {
                    CustomersDistance += customer.CalculateDistanceBetween(this.Customers[index]);
                    CustomersDistance += customer.CalculateDistanceBetween(this.Customers[index - 1]);
                }
                else
                {
                    CustomersDistance += customer.CalculateDistanceBetween(this.Customers[index]);
                }
                this.Customers.Insert(index, customer);
                this.Vehicle.CurrentLoad += customer.Demand;
                this.Vehicle.CurrentTime = this.TotalDistance + customer.ServiceTime;
            }
            else
                AddCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            var customerIndex = this.Customers.IndexOf(customer);

            double distanceDifference = 0;
            double previousTotal = this.TotalDistance;

            if (Customers.Count == 1)
            {
                distanceDifference = 0;
            }
            else if (IsInterior(customer))
            {
                distanceDifference = customer.CalculateDistanceBetween(this.Customers[customerIndex + 1]);
                distanceDifference += customer.CalculateDistanceBetween(this.Customers[customerIndex - 1]);
                distanceDifference -= this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Customers[customerIndex + 1]);
            }
            else
            {
                distanceDifference = customer.CalculateDistanceBetween(customerIndex == 0 ? this.Customers[customerIndex + 1] : this.Customers[customerIndex - 1]);
            }

            this.CustomersDistance -= distanceDifference;
            Customers.Remove(customer);

            this.Vehicle.CurrentLoad -= customer.Demand;
            this.Vehicle.CurrentTime -= (previousTotal - this.TotalDistance + customer.ServiceTime);
        }

        public void MergeRoutes(Route routeToMerge, double distanceBetween)
        {
            this.CustomersDistance = this.CustomersDistance + routeToMerge.CustomersDistance + distanceBetween;
            this.Vehicle.CurrentTime = (this.Vehicle.CurrentTime - DistanceToDepot) + (routeToMerge.Vehicle.CurrentTime - routeToMerge.DistanceFromDepot) + distanceBetween;
            this.Vehicle.CurrentLoad += routeToMerge.Vehicle.CurrentLoad;

            this.Customers.AddRange(routeToMerge.Customers);
        }

        public bool IsInterior(Customer customer)
        {
            var index = Customers.IndexOf(customer);
            var count = this.Customers.Count;

            if (count < 2)
                return false;

            if (index == 0 || index == count - 1)
                return false;

            return true;
        }

        public bool IsFeasible()
        {
            if (this.Vehicle.CurrentLoad <= this.Vehicle.Capacity)
            {
                if (this.Vehicle.CurrentTime <= this.Depot.DueDate)
                    return true;
            }
            return false;
        }
    }
}