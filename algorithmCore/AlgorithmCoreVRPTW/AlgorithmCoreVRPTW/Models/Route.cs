using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreVRPTW.Models
{
    public class Route
    {
        private double _distance = 0;
        private double _time = 0;
        public int Id { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<List<double>> Distances { get; set; } = new List<List<double>>();
        public List<List<double>> Durations { get; set; } = new List<List<double>>();

        public double TotalDistance
        {
            get
            {
                return _distance + DistanceToDepot + DistanceFromDepot;
            }
        }
        public double TotalTime
        {
            get
            {
                return _time + TimeToDepot + TimeFromDepot;
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

        public double CustomersTime
        {
            get
            {
                return _time;
            }
            set
            {
                this._time = value;
            }
        }

        public double DistanceToDepot
        {
            get
            {
                if (this.Customers.Count == 0)
                    return 0;

                return this.Customers.Last().DepotDistanceTo;
            }
        }

        public double DistanceFromDepot
        {
            get
            {
                if (this.Customers.Count == 0)
                    return 0;

                return this.Customers.First().DepotDistanceFrom;
            }
        }

        public double TimeToDepot
        {
            get
            {
                if (this.Customers.Count == 0)
                    return 0;

                return this.Customers.Last().DepotTimeTo;
            }
        }

        public double TimeFromDepot
        {
            get
            {
                if (this.Customers.Count == 0)
                    return 0;

                return this.Customers.First().DepotDistanceFrom;
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
                CustomersTime = this.CustomersTime,
                Depot = this.Depot,
                Vehicle = this.Vehicle.Clone(),
                Distances=this.Distances,
                Durations=this.Durations
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
                CustomersDistance += this.Customers.Last().CalculateDistanceBetween(this.Distances, customer);
                CustomersTime += this.Customers.Last().CalculateTimeBetween(this.Durations, customer);
                CustomersTime += customer.ServiceTime;
                //CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers.Last());
                //CustomersTime += customer.CalculateTimeBetween(this.Durations, this.Customers.Last());
            }
            this.Customers.Add(customer);

            this.Vehicle.CurrentLoad += customer.Demand;
            this.Vehicle.CurrentTime = this.TotalTime;
        }

        public void AddCustomer(Customer customer, int index)
        {
            if (Customers.Count > 0 && index < Customers.Count)
            {
                var indexCustomer = Customers[index];
                if (IsInterior(indexCustomer) || (index == Customers.Count - 1 && Customers.Count > 1))
                {
                    CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers[index]);
                    CustomersDistance += this.Customers[index - 1].CalculateDistanceBetween(this.Distances, customer);

                    CustomersTime += customer.CalculateDistanceBetween(this.Durations, this.Customers[index]);
                    CustomersTime += this.Customers[index - 1].CalculateTimeBetween(this.Durations, customer);
                    CustomersTime += customer.ServiceTime;
                    //CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers[index]);
                    //CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers[index - 1]);
                }
                else
                {
                    CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers[index]);
                    CustomersTime += customer.CalculateTimeBetween(this.Durations, this.Customers[index]);
                    CustomersTime += customer.ServiceTime;
                }
                this.Customers.Insert(index, customer);
                this.Vehicle.CurrentLoad += customer.Demand;
                this.Vehicle.CurrentTime = this.TotalTime;
            }
            else
                AddCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            var customerIndex = this.Customers.IndexOf(customer);

            double distanceDifference = 0;
            double previousTotal = this.TotalDistance;

            double timeDifference = 0;
            double previousTime = this.TotalTime;

            if (Customers.Count == 1)
            {
                distanceDifference = 0;
                timeDifference = 0;
            }
            else if (IsInterior(customer))
            {

                distanceDifference = customer.CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);
                distanceDifference += this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, customer);
                distanceDifference -= this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);

                timeDifference = customer.CalculateTimeBetween(this.Durations, this.Customers[customerIndex + 1]);
                timeDifference += this.Customers[customerIndex - 1].CalculateTimeBetween(this.Durations, customer);
                timeDifference -= this.Customers[customerIndex - 1].CalculateTimeBetween(this.Durations, this.Customers[customerIndex + 1]);
                //distanceDifference = customer.CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);
                //distanceDifference += customer.CalculateDistanceBetween(this.Distances, this.Customers[customerIndex - 1]);
                //distanceDifference -= this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);
            }
            else
            {
                if (customerIndex == 0)
                {
                    distanceDifference = customer.CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1] );
                    timeDifference = customer.CalculateTimeBetween(this.Durations, this.Customers[customerIndex + 1] );
                }
                else
                {
                    distanceDifference = this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, customer);
                    timeDifference = this.Customers[customerIndex - 1].CalculateTimeBetween(this.Durations, customer);
                }
            }

            this.CustomersDistance -= distanceDifference;
            this.CustomersTime -= timeDifference + customer.ServiceTime;
            Customers.Remove(customer);

            this.Vehicle.CurrentLoad -= customer.Demand;
            this.Vehicle.CurrentTime = this.TotalTime;
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

            return index != 0 && index != count - 1;
        }

        public bool IsFeasible()
        {
            if (CheckCapacityConstraints(this.Customers, this.Vehicle.Capacity))
                return CheckTimeConstraints(this.Customers, this.Depot, this.Durations);

            return false;
        }

        public static bool CheckCapacityConstraints(List<Customer> customers, int vehicleCapacity)
        {
            return customers.Sum(x => x.Demand) <= vehicleCapacity;
        }

        public static bool CheckTimeConstraints(List<Customer> customers, Depot depot, List<List<double>> durations)
        {
            double arrivalTime = 0;
            Customer previousCustomer = null;
            arrivalTime += customers.FirstOrDefault().DepotTimeFrom;

            foreach (Customer customer in customers)
            {
                if (previousCustomer != null)
                {
                    arrivalTime += previousCustomer.ServiceTime;
                    arrivalTime += previousCustomer.CalculateTimeBetween(durations, customer);
                }
                if (arrivalTime < customer.ReadyTime)
                {
                    arrivalTime = customer.ReadyTime;
                }
                if (arrivalTime > customer.DueDate)
                {
                    return false;
                }
                previousCustomer = customer;
            }
            arrivalTime += customers.LastOrDefault().ServiceTime;
            // czy tu nie trzeba uwzglednic tego jeszcze
            arrivalTime += customers.LastOrDefault().DepotTimeTo;

            return arrivalTime <= depot.DueDate;
        }
    }
}