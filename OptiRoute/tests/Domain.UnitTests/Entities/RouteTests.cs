using FluentAssertions;
using NUnit.Framework;
using OptiRoute.Domain.Entities;
using OptiRoute.Domain.UnitTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Domain.UnitTests.Entities
{
   public class RouteTests
    {
        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldChangeRouteDistance(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5,100);

            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }


            double customersDistance = GetValidCustomerDistance(route);      
            route.CustomersDistance.Should().Be(customersDistance);

            customersDistance += route.DistanceToDepot + route.DistanceFromDepot;
            route.TotalDistance.Should().Be(customersDistance);
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldChangeRouteDistanceAfterRemoving(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.DeleteCustomer(customers.Last());

            double customersDistance = GetValidCustomerDistance(route);
            route.CustomersDistance.Should().Be(customersDistance);

            customersDistance += route.DistanceToDepot + route.DistanceFromDepot;
            route.TotalDistance.Should().Be(customersDistance);
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldChangeRouteTime(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }


            double customersTime = GetValidCustomerTime(route);
            route.CustomersTime.Should().Be(customersTime);

            customersTime += route.TimeFromDepot + route.TimeToDepot;
            route.TotalTime.Should().Be(customersTime);
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldChangeRouteTimeAfterRemoving(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.DeleteCustomer(customers.Last());


            double customersTime = GetValidCustomerTime(route);
            route.CustomersTime.Should().Be(customersTime);

            customersTime += route.TimeFromDepot + route.TimeToDepot;
            route.TotalTime.Should().Be(customersTime);
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldIncludeWaitingTime(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.CustomersTime.Should().Be(GetValidCustomerTime(route));
            route.TotalTime.Should().Be(GetValidTimeWithWaiting(route));
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldIncludeWaitingTimeAfterRemoving(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }
            route.DeleteCustomer(customers.Last());

            route.CustomersTime.Should().Be(GetValidCustomerTime(route));
            route.TotalTime.Should().Be(GetValidTimeWithWaiting(route));
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldChangeLoadOfTheVehicle(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.Vehicle.CurrentLoad.Should().Be(customers.Sum(x => x.Demand));
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldNotChangeLoadOfTheVehicle(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
                route.DeleteCustomer(customer);
            }

            route.Vehicle.CurrentLoad.Should().Be(0);
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldChangeTimeOfTheVehicle(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.Vehicle.CurrentTime.Should().Be(route.TotalTime);
            route.Vehicle.CurrentTime.Should().NotBe(GetValidCustomerTime(route));
        }

        [Test]
        [TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.PrepareCustomerCases))]
        public void ShouldNotChangeTimeOfTheVehicle(List<Customer> customers)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
                route.DeleteCustomer(customer);
            }

            route.Vehicle.CurrentLoad.Should().Be(0);
        }

        [Test]       
        public void ShouldReturnInterior()
        {
            Route route = EntityHelper.GetRouteCase(0, 5,100);


            route.Customers.Add(EntityHelper.GetCustomer(1, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(2, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(3, 1, 1, 2, 1, 1, 1));

            route.IsInterior(route.Customers[1]).Should().BeTrue();
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        public void ShouldNotReturnInterior(int index)
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            route.Customers.Add(EntityHelper.GetCustomer(1, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(2, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(3, 1, 1, 2, 1, 1, 1));

            route.IsInterior(route.Customers[index]).Should().BeFalse();
        }

        [Test]
        public void ShouldNotReturnInteriorWhenTwoCustomers()
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            route.Customers.Add(EntityHelper.GetCustomer(1, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(2, 1, 1, 2, 1, 1, 1));

            route.IsInterior(route.Customers[0]).Should().BeFalse();
            route.IsInterior(route.Customers[1]).Should().BeFalse();
        }

        [Test]
        public void ShouldReturnDeepCopy()
        {
            Route original = EntityHelper.GetRouteCase(0, 5, 100);


            Route cloned = original.Clone();

            cloned.Should().BeEquivalentTo(original);
            Assert.AreNotSame(original, cloned);
        }

        [Test]
        public void ShouldNotExceedCapacity()
        {
            Route route = EntityHelper.GetRouteCase(0, 6, 100);


            route.Customers.Add(EntityHelper.GetCustomer(1, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(2, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(3, 1, 1, 2, 1, 1, 1));

            route.CheckCapacityConstraints().Should().BeTrue();
        }

        [Test]
        public void ShouldExceedCapacity()
        {
            Route route = EntityHelper.GetRouteCase(0, 5, 100);


            route.Customers.Add(EntityHelper.GetCustomer(1, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(2, 1, 1, 2, 1, 1, 1));
            route.Customers.Add(EntityHelper.GetCustomer(3, 1, 1, 2, 1, 1, 1));

            route.CheckCapacityConstraints().Should().BeFalse();
        }


        [Test]
        public void ShouldBeFeasible()
        {
            Route route = EntityHelper.GetRouteCase(0, 10, 100);

            var customers = EntityHelper.GetCustomers();

            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.IsFeasible().Should().BeTrue();
        }


        [Test]
        public void ShouldNotBeFeasibleDepotDueDate()
        {
            Route route = EntityHelper.GetRouteCase(0, 10, 3);


            var customers = EntityHelper.GetCustomers();

            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(route.Distances, route.Durations);
                route.AddCustomer(customer);
            }

            route.IsFeasible().Should().BeFalse();
        }


        public double GetValidCustomerTime(Route route)
        {
            if (route.Customers.Count == 0) return 0;

            double arrivalTime = 0;
            Customer previousCustomer = null;
            foreach (Customer customer in route.Customers)
            {
                if (previousCustomer != null)
                {
                    arrivalTime += previousCustomer.ServiceTime;
                    arrivalTime += previousCustomer.CalculateTimeBetween(route.Durations, customer);
                }
                previousCustomer = customer;
            }
            arrivalTime += route.Customers.LastOrDefault().ServiceTime;
            return arrivalTime;
        }

        public double GetValidCustomerDistance(Route route)
        {
                double dist = 0;
                for (int i = 0; i < route.Customers.Count - 1; i++)
                {
                    dist += route.Customers[i].CalculateDistanceBetween(route.Distances, route.Customers[i + 1]);
                }
            return dist;
        }

        public double GetValidTimeWithWaiting(Route route)
        {
            if (route.Customers.Count == 0) return 0;

            double arrivalTime = 0;
            Customer previousCustomer = null;
            arrivalTime += route.Customers.FirstOrDefault().DepotTimeFrom;
            foreach (Customer customer in route.Customers)
            {
                if (previousCustomer != null)
                {
                    arrivalTime += previousCustomer.ServiceTime;
                    arrivalTime += previousCustomer.CalculateTimeBetween(route.Durations, customer);
                }
                if (arrivalTime < customer.ReadyTime)
                {
                    arrivalTime = customer.ReadyTime;
                }
                if (arrivalTime > customer.DueDate)
                {
                    return 0;
                }
                previousCustomer = customer;
            }
            arrivalTime += route.Customers.LastOrDefault().ServiceTime;
            arrivalTime += route.Customers.LastOrDefault().DepotTimeTo;

            return arrivalTime;
        }
    }
}
