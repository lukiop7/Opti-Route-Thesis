using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

            bool Feasible = routes.Count <= problem.Vehicles;

            return new Solution() { Feasible = Feasible, Depot = problem.Depot, Routes = routes };
        }

        private void MergeSavings(Problem problem, List<Saving> savings, List<Route> routes)
        {
            foreach (Saving saving in savings)
            {
                var routeCustomerA = routes.FirstOrDefault(x => x.Customers.Any(c => c.Id == saving.A.Id));
                var routeCustomerB = routes.FirstOrDefault(x => x.Customers.Any(c => c.Id == saving.B.Id));

                if (ValidateSaving(routeCustomerA, routeCustomerB, saving))
                {
                    Route routeCustomerACopy = routeCustomerA.Clone();
                    Route routeCustomerBCopy = routeCustomerB.Clone();
                    bool feasible = TestMergeRoutes(routeCustomerACopy, routeCustomerBCopy, saving);
                    if (feasible)
                    {
                        MergeRoutes(routeCustomerA, routeCustomerB, routes, saving);
                    }
                }
            }
        }

        private bool TestMergeRoutes(Route routeCustomerA, Route routeCustomerB, Saving saving)
        {
            routeCustomerA.MergeRoutes(routeCustomerB, saving.DistanceBetween);
            return routeCustomerA.IsFeasible();
        }

        private void MergeRoutes(Route routeCustomerA, Route routeCustomerB, List<Route> routes, Saving saving)
        {
            routeCustomerA.MergeRoutes(routeCustomerB, saving.DistanceBetween);
            routes.Remove(routeCustomerB);
        }

        private bool ValidateSaving(Route routeCustomerA, Route routeCustomerB, Saving saving)
        {
            if (ValidateCustomers(routeCustomerA, routeCustomerB, saving))
            {
                return true;
            }

            return false;
        }

        private bool ValidateCustomers(Route routeCustomerA, Route routeCustomerB, Saving saving)
        {
            if (routeCustomerA.Id != routeCustomerB.Id)
            {
                if (!routeCustomerA.IsInterior(saving.A) && !routeCustomerB.IsInterior(saving.B))
                {
                    if (routeCustomerA.Customers.Count == 1 && routeCustomerB.Customers.Count == 1)
                    {
                        return true;
                    }

                    if ((routeCustomerA.Customers.IndexOf(saving.A) != 0 && routeCustomerB.Customers.IndexOf(saving.B) != routeCustomerB.Customers.Count))
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }

        private List<Saving> GenerateSavings(Problem problem, List<Customer> customers)
        {
            List<Saving> savings = new List<Saving>();

            for (int i = 0; i < customers.Count; i++)
            {
                for (int j = 0; j < customers.Count; j++)
                {
                    if (i == j)
                        continue;

                    // wersja z unikalnymi savingami
                    //if (i >= j)
                    //    continue;

                    var saving = new Saving()
                    {
                        A = customers[i],
                        B = customers[j]
                    };
                    saving.CalculateSaving(problem.Depot);
                    savings.Add(saving);
                }
            }
            savings = savings.OrderByDescending(x => x.Value).ToList();
            return savings;
        }

        private List<Route> CreateInitialRoutes(Problem problem)
        {
            List<Route> routes = new List<Route>();
            for (int i = 0; i < problem.Customers.Count; i++)
            {
                var route = new Route() { Id = i, Vehicle = new Vehicle(i, problem.Capacity, 0, 0), Depot = problem.Depot };
                route.AddCustomer(problem.Customers[i]);
                routes.Add(route);
            }
            return routes;
        }
    }
}