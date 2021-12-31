using System.Collections.Generic;
using System.Linq;

namespace OptiRoute.Domain.Entities
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
        private double _waitingTime = 0;
        private Dictionary<int, double> _waitingTimeDictionary = new Dictionary<int, double>();

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
                return _time + _waitingTime + TimeToDepot + TimeFromDepot;
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

        public double WaitingTime
        {
            get
            {
                return _waitingTime;
            }
            set
            {
                this._waitingTime = value;
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

                return this.Customers.First().DepotTimeFrom;
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
                Distances = this.Distances,
                Durations = this.Durations,
                _waitingTimeDictionary = this._waitingTimeDictionary,
                WaitingTime = this.WaitingTime
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

            if (TimeFromDepot + CustomersTime + WaitingTime < customer.ReadyTime)
            {
                double waitingTime = customer.ReadyTime - (TimeFromDepot + CustomersTime + WaitingTime);
                this._waitingTime += waitingTime;
                this._waitingTimeDictionary.Add(customer.Id, waitingTime);
            }

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
                }
                else
                {
                    CustomersDistance += customer.CalculateDistanceBetween(this.Distances, this.Customers[index]);
                }
                this.Customers.Insert(index, customer);
                this.Vehicle.CurrentLoad += customer.Demand;
                CalculateTime();
            }
            else
                AddCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            var customerIndex = this.Customers.IndexOf(customer);

            double distanceDifference = 0;
            double previousTotal = this.TotalDistance;

            double previousTime = this.TotalTime;

            if (Customers.Count == 1)
            {
                distanceDifference = 0;
            }
            else if (IsInterior(customer))
            {

                distanceDifference = customer.CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);
                distanceDifference += this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, customer);
                distanceDifference -= this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);
            }
            else
            {
                if (customerIndex == 0)
                {
                    distanceDifference = customer.CalculateDistanceBetween(this.Distances, this.Customers[customerIndex + 1]);
                }
                else
                {
                    distanceDifference = this.Customers[customerIndex - 1].CalculateDistanceBetween(this.Distances, customer);
                }
            }

            this.CustomersDistance -= distanceDifference;
            Customers.Remove(customer);

            this.Vehicle.CurrentLoad -= customer.Demand;
            CalculateTime();
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

            return arrivalTime <= Depot.DueDate;
        }

        public void CalculateTime()
        {

            _waitingTimeDictionary.Clear();
            double arrivalTime = 0;
            double customersTime = 0;
            double totalWaiting = 0;

            if (Customers.Count != 0)
            {
                Customer previousCustomer = null;
                arrivalTime += Customers.FirstOrDefault().DepotTimeFrom;
                foreach (Customer customer in Customers)
                {
                    if (previousCustomer != null)
                    {
                        arrivalTime += previousCustomer.ServiceTime;
                        arrivalTime += previousCustomer.CalculateTimeBetween(Durations, customer);
                        customersTime += previousCustomer.ServiceTime;
                        customersTime += previousCustomer.CalculateTimeBetween(Durations, customer);
                    }
                    if (arrivalTime < customer.ReadyTime)
                    {
                        double waitingTime = customer.ReadyTime - (arrivalTime);

                        totalWaiting += waitingTime;
                        arrivalTime = customer.ReadyTime;

                        _waitingTimeDictionary.Add(customer.Id, waitingTime);
                    }
                    previousCustomer = customer;
                }
                arrivalTime += Customers.LastOrDefault().ServiceTime;
                customersTime += Customers.LastOrDefault().ServiceTime;
                arrivalTime += Customers.LastOrDefault().DepotTimeTo;
            }

            this.CustomersTime = customersTime;
            this.WaitingTime = totalWaiting;
            this.Vehicle.CurrentTime = TotalTime;
        }
    }
}