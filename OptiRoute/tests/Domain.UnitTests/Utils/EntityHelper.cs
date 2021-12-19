using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Domain.UnitTests.Utils
{
    public static class EntityHelper
    {

        public static List<List<double>> GetDistances()
        {
            return new List<List<double>>()
            {
                new List<double>(){0,1,2,3,4},
                new List<double>(){1,0,2,3,4},
                new List<double>(){2,2,0,4,4},
                new List<double>(){3,3,4,0,4},
                new List<double>(){4,4,4,4,0},
            };
        }

        public static List<List<double>> GetDurations()
        {
            return new List<List<double>>()
            {
                new List<double>(){0,1,2,3,4},
                new List<double>(){1,0,2,3,4},
                new List<double>(){2,2,0,4,4},
                new List<double>(){3,3,4,0,4},
                new List<double>(){4,4,4,4,0},
            };
        }

        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < 4; i++)
            {
                customers.Add(GetCustomer(i + 1, i, i, i + 1, (i) * 10, (i + 1) * 10, 1));
            }
            return customers;
        }

        public static Customer GetCustomer(int id, int x, int y, int demand, int readyTime, int dueDate, int serviceTime)
        {
            return new Customer()
            {
                Id = id,
                X = x,
                Y = y,
                Demand = demand,
                ReadyTime = readyTime,
                DueDate = dueDate,
                ServiceTime = serviceTime
            };
        }

        public static Depot GetDepot(int id, int x, int y, int dueDate)
        {
            return new Depot()
            {
                Id = id,
                X = x,
                Y = y,
                DueDate = dueDate
            };
        }

        public static Route GetRoute(int id, int capacity)
        {
            return new Route()
            {
                Id = id,
                Vehicle = new Vehicle(id, capacity, 0, 0)
            };
        }

        public static Route GetRouteCase(int id, int capacity, int dueDate)
        {
            Route route = new Route()
            {
                Id = id,
                Vehicle = new Vehicle(id, capacity, 0, 0)
            };

            Depot depot = EntityHelper.GetDepot(0, 0, 0, dueDate);
            route.Depot = depot;

            route.Distances = EntityHelper.GetDistances();
            route.Durations = EntityHelper.GetDurations();
            return route;
        }

        public static IEnumerable<List<Customer>> PrepareCustomerCases()
        {
            yield return GetCustomers().Take(1).ToList();
            yield return GetCustomers().Take(2).ToList();
            yield return GetCustomers().Take(3).ToList();
            yield return GetCustomers();
        }
    }
}
