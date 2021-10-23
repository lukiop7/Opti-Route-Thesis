using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class ClarkeWrightInitial : IInitial
    {

        public Solution Solve(Problem problem)
        {
            var customers = problem.Customers;
            List<Route> routes = CreateInitialRoutes(problem);
            List<Saving> savings = new List<Saving>();

            for(int i=0; i < customers.Count; i++)
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



            return null;
        }
        private List<Route> CreateInitialRoutes(Problem problem)
        {
            List<Route> routes = new List<Route>();
            for(int i = 0; i<problem.Customers.Count; i++)
            {
                var route = new Route() { Vehicle = new Vehicle(i, problem.Capacity, 0, 0), Depot = problem.Depot };
                route.AddCustomer(problem.Customers[i]);
                routes.Add(route);
            }
            return routes;
        }

    }
}
