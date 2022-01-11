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
    public class CustomersTests
    {
        [Test]
        public void ShouldParseCustomerFromString()
        {
            Customer customer = Customer.Parse("    1      45         68         10        912        967         90  ");

            customer.Should().NotBeNull();
            customer.Id.Should().Be(1);
            customer.X.Should().Be(45);
            customer.Y.Should().Be(68);
            customer.Demand.Should().Be(10);
            customer.ReadyTime.Should().Be(912);
            customer.DueDate.Should().Be(967);
            customer.ServiceTime.Should().Be(90);
        }

        [Test, TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.GetCustomers))]
        public void ShouldCalculateValidDistanceBetweenCustomers(Customer customer)
        {
            List<List<double>> distances = EntityHelper.GetDistances();

            List<Customer> customers = EntityHelper.GetCustomers();

            foreach(var listCustomer in customers)
            {
                Assert.AreEqual(distances[customer.Id][listCustomer.Id], customer.CalculateDistanceBetween(distances, listCustomer));
            }
        }

        [Test, TestCaseSource(typeof(EntityHelper), nameof(EntityHelper.GetCustomers))]
        public void ShouldCalculateValidTimeBetweenCustomers(Customer customer)
        {
            List<List<double>> durations = EntityHelper.GetDurations();

            List<Customer> customers = EntityHelper.GetCustomers();

            foreach (var listCustomer in customers)
            {
                Assert.AreEqual(durations[customer.Id][listCustomer.Id], customer.CalculateTimeBetween(durations, listCustomer));
            }
        }

        [Test, TestCaseSource(typeof(EntityHelper),nameof(EntityHelper.GetCustomers))]
        public void ShouldSetValidDepotProperties(Customer customer)
        {
            List<List<double>> distances = EntityHelper.GetDistances();
            List<List<double>> durations = EntityHelper.GetDurations();

            customer.CalculateDepotTimesAndDistances(distances, durations);

            customer.DepotDistanceTo.Should().Be(distances[customer.Id][0]);
            customer.DepotDistanceFrom.Should().Be(distances[0][customer.Id]);
            customer.DepotTimeTo.Should().Be(durations[customer.Id][0]);
            customer.DepotTimeFrom.Should().Be(durations[0][customer.Id]);
        }
    }
}
