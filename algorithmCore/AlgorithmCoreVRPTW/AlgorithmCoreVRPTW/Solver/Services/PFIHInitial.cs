using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class PFIHInitial : IMethod
    {
        public Solution Solve(Problem problem)
        {
            List<Customer> unroutedCustomers = new List<Customer>();
            List<Route> routes = new List<Route>();
            unroutedCustomers.AddRange(problem.Customers);
            CalculateDepotDistancesAndTimes(ref unroutedCustomers, problem.Depot, problem.Distances, problem.Durations);
            Construct(problem, unroutedCustomers, routes);
            bool Feasible = routes.Count <= problem.Vehicles;

            return new Solution() { Feasible = Feasible, Depot = problem.Depot, Routes = routes };
        }

        private void Construct(Problem problem, List<Customer> unroutedCustomers, List<Route> routes)
        {
            int counter = 0;
            while (unroutedCustomers.Count > 0)
            {
                Route currentRouteCustomerList = new Route() { Id = counter, Vehicle = new Vehicle(counter, problem.Capacity, 0, 0), Depot = problem.Depot, Distances = problem.Distances,
                Durations = problem.Durations};
                Customer seedCustomer = FindSeedCustomer(unroutedCustomers);
                currentRouteCustomerList.AddCustomer(seedCustomer);
                unroutedCustomers.Remove(seedCustomer);

                InsertCustomers(unroutedCustomers, currentRouteCustomerList);
                routes.Add(currentRouteCustomerList);

                counter++;
            }
        }

        private void InsertCustomers(List<Customer> unroutedCustomers, Route currentRouteCustomerList)
        {
            bool placementPossible = true;
            while (placementPossible)
            {
                double minCost = Double.MaxValue;
                int insertionIndex = 0;
                Customer selectedCustomer = null;
                GetBestInsertionPlace(unroutedCustomers, currentRouteCustomerList, ref minCost, ref insertionIndex, ref selectedCustomer);
                if (minCost != Double.MaxValue)
                {
                    currentRouteCustomerList.AddCustomer(selectedCustomer, insertionIndex);
                    unroutedCustomers.Remove(selectedCustomer);
                }
                else
                {
                    placementPossible = false;
                }
            }
        }

        private void GetBestInsertionPlace(List<Customer> unroutedCustomers, Route currentRouteCustomerList, ref double minCost, ref int insertionIndex, ref Customer selectedCustomer)
        {
            foreach (Customer unroutedCustomer in unroutedCustomers)
            {
                for (int currentRouteCustomerIndex = 0; currentRouteCustomerIndex <= currentRouteCustomerList.Customers.Count; currentRouteCustomerIndex++)
                {
                    double cost = CostOfInsertionInRoute(currentRouteCustomerList.Clone(), currentRouteCustomerIndex, unroutedCustomer);
                    if (cost < minCost)
                    {
                        minCost = cost;
                        insertionIndex = currentRouteCustomerIndex;
                        selectedCustomer = unroutedCustomer;
                    }
                }
            }
        }

        private void CalculateDepotDistancesAndTimes(ref List<Customer> customers, Depot depot, List<List<double>> distances, List<List<double>> durations)
        {
            foreach (var customer in customers)
            {
                customer.CalculateDepotTimesAndDistances(distances, durations, depot);
            }
            customers = customers.OrderByDescending(x => x.DepotDistanceFrom).ToList();
        }

        //finds seed customer for new route from given unrouted customers
        private Customer FindSeedCustomer(List<Customer> unroutedCustomerList)
        {
            double minCost = Double.MaxValue;
            Customer seedCustomer = null;

            foreach (Customer customer in unroutedCustomerList)
            {
                double distanceFromDepo = customer.DepotDistanceFrom;
                double timeFromDepo = customer.DepotTimeFrom;
                double cost = CostOfSeedCustomer(distanceFromDepo, timeFromDepo, customer.ReadyTime);
                if (cost < minCost)
                {
                    minCost = cost;
                    seedCustomer = customer;
                }
            }
            return seedCustomer;
        }

        //calculates cost of customer for selectioning it as seed customer
        private double CostOfSeedCustomer(double disFromDepo, double timeFromDepo, int latestDeadline)
        {
            double alpha = 0.4;
            double beta = 0.4;
            double gamma = 0.2;
            return alpha * disFromDepo + beta * timeFromDepo + gamma * latestDeadline;
        }

        //calculates cost of insertion for given customer in given vehicle route at specific index
        private double CostOfInsertionInRoute(Route route, int index, Customer customer)
        {
            route.AddCustomer(customer, index);
            bool feasible = route.IsFeasible();
            if (feasible)
            {
                double newTotalDistance = route.TotalDistance;
                double theta = 0.01 * newTotalDistance;
                double routeTime = route.Vehicle.CurrentTime;
                route.DeleteCustomer(customer);

                return newTotalDistance + theta * routeTime;
            }

            route.DeleteCustomer(customer);
            return Double.MaxValue;
        }
    }
}