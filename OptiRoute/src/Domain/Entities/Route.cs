using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreVRPTW.Models
{
    public class Route
    {
        public int Id { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<List<double>> Distances { get; set; } = new List<List<double>>();
        public List<List<double>> Durations { get; set; } = new List<List<double>>();
        public Vehicle Vehicle { get; set; }
        public Depot Depot { get; set; }

        private double _distance = 0;
        public double TotalDistance
        {
            get
            {
                return _distance + DistanceToDepot + DistanceFromDepot;
            }
        }
        private double _time = 0;
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

        public bool ValidateDistance()
        {
            double dist = 0;
            for (int i = 0; i < Customers.Count - 1; i++)
            {
                dist += Customers[i].CalculateDistanceBetween(this.Distances, Customers[i + 1]);
            }
            return System.Math.Round(dist - CustomersDistance, 13) <= 0;
        }

        public bool ValidateTime()
        {
            double arrivalTime = 0;
            Customer previousCustomer = null;
            foreach (Customer customer in Customers)
            {
                if (previousCustomer != null)
                {
                    arrivalTime += previousCustomer.ServiceTime;
                    arrivalTime += previousCustomer.CalculateTimeBetween(Durations, customer);
                }
                previousCustomer = customer;
            }
            arrivalTime += Customers.LastOrDefault().ServiceTime;
            return System.Math.Round(arrivalTime - this.CustomersTime, 13) <= 0;
        }

        public void AddCustomer(Customer customer)
        {
            if (Customers.Count > 0)
            {
                CustomersDistance += this.Customers.Last().CalculateDistanceBetween(this.Distances, customer);
                CustomersTime += this.Customers.Last().CalculateTimeBetween(this.Durations, customer);
            }
            this.Customers.Add(customer);

            CustomersTime += customer.ServiceTime;
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
                }
                else
                {
                    CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers[index]);
                    CustomersTime += customer.CalculateTimeBetween(this.Durations, this.Customers[index]);
                }
                this.Customers.Insert(index, customer);
                CustomersTime += customer.ServiceTime;
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
            if (CheckCapacityConstraints())
                return CheckTimeConstraints();

            return false;
        }

        public bool CheckCapacityConstraints()
        {
            return this.Customers.Sum(x => x.Demand) <= this.Vehicle.Capacity;
        }

        public bool CheckTimeConstraints()
        {
            double arrivalTime = 0;
            Customer previousCustomer = null;
            arrivalTime += Customers.FirstOrDefault().DepotTimeFrom;
            foreach (Customer customer in Customers)
            {
                if (previousCustomer != null)
                {
                    arrivalTime += previousCustomer.ServiceTime;
                    arrivalTime += previousCustomer.CalculateTimeBetween(Durations, customer);
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
            arrivalTime += Customers.LastOrDefault().ServiceTime;
            arrivalTime += Customers.LastOrDefault().DepotTimeTo;

            this.Vehicle.CurrentTime = arrivalTime;

            return arrivalTime <= Depot.DueDate;
        }
    }
}