using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class ClarkeWrightInitial : IMethod
    {

        public Solution Solve(Problem problem)
        {
            var customers = problem.Customers;
            List<Route> routes = CreateInitialRoutes(problem);
            List<Saving> savings = GenerateSavings(problem, customers);
            MergeSavings(problem, savings, routes);


            return new Solution() { Feasible = true, Depot = problem.Depot, Routes = routes};
        }

        private void MergeSavings(Problem problem, List<Saving> savings, List<Route> routes)
        {
            foreach(Saving saving in savings)
            {
                var routeCustomerA = routes.FirstOrDefault(x => x.Customers.Any(c => c.Id == saving.A.Id));
                var routeCustomerB = routes.FirstOrDefault(x => x.Customers.Any(c => c.Id == saving.B.Id));

                if (ValidateSaving(routeCustomerA, routeCustomerB, saving))
                {
                    MergeRoutes(routeCustomerA, routeCustomerB, routes, saving);
                }
            }
        }

        private void MergeRoutes(Route routeCustomerA, Route routeCustomerB, List<Route> routes, Saving saving)
        {
            routeCustomerA.MergeRoutes(routeCustomerB, saving.DistanceBetween);
            routes.Remove(routeCustomerB);
        }

        private bool ValidateSaving(Route routeCustomerA, Route routeCustomerB, Saving saving)
        {
            if (ValidateCustomers(routeCustomerA, routeCustomerB, saving))
                return ValidateConstraints(routeCustomerA, routeCustomerB, saving);

           return false;
        }

        private bool ValidateConstraints(Route routeCustomerA, Route routeCustomerB, Saving saving)
        {
           if((routeCustomerA.Vehicle.CurrentLoad + routeCustomerB.Vehicle.CurrentLoad) < routeCustomerA.Vehicle.Capacity)
            {
                if (((routeCustomerA.Vehicle.CurrentTime - routeCustomerA.DistanceToDepot) + (routeCustomerB.Vehicle.CurrentTime - routeCustomerB.DistanceFromDepot)
                    + saving.DistanceBetween) <= routeCustomerA.Depot.DueDate)
                    return true;
            }
            return false;
        }

        private bool ValidateCustomers(Route routeCustomerA, Route routeCustomerB, Saving saving)
        {
            if(routeCustomerA.Id != routeCustomerB.Id)
            {
                var indexA = routeCustomerA.Customers.IndexOf(saving.A);
                var indexB = routeCustomerB.Customers.IndexOf(saving.B);                
                if (!(IsInterior(indexA, routeCustomerA.Customers.Count) && IsInterior(indexB, routeCustomerB.Customers.Count)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInterior(int index, int count)
        {
            if (count < 2)
                return false;

            if (index == 0 || index == count - 1)
                return false;

            return true;
        }

        private List<Saving> GenerateSavings(Problem problem, List<Customer> customers)
        {
            List<Saving> savings = new List<Saving>();

            for (int i = 0; i < customers.Count; i++)
            {
                for (int j = 0; j < customers.Count; j++)
                {
                    if (i >= j)
                        continue;

                    var saving = new Saving()
                    {
                        A = customers[i],
                        B = customers[j]
                    };
                    saving.CalculateSaving(problem.Depot);
                    savings.Add(saving);
                }
            }
            savings.OrderByDescending(x => x.Value);
            return savings;
        }

        private List<Route> CreateInitialRoutes(Problem problem)
        {
            List<Route> routes = new List<Route>();
            for(int i = 0; i<problem.Customers.Count; i++)
            {
                var route = new Route() {Id=i, Vehicle = new Vehicle(i, problem.Capacity, 0, 0), Depot = problem.Depot };
                route.AddCustomer(problem.Customers[i]);
                routes.Add(route);
            }
            return routes;
        }

    }
}
