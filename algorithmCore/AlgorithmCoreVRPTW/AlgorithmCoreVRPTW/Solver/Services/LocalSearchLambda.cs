using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class LocalSearchLambda : IImprovement
    {
        private readonly int Lambda = 2;

        public Solution Improve(Solution currentSolution)
        {
            var routeList = currentSolution.Routes;
            int iterationsCounter = 0;
            int iterationsLimit = 50;
            int repetitionsCounter = 0;
            int repetitionsLimit = 5;
            double previousDistance = 0;
            while (iterationsCounter < iterationsLimit && repetitionsCounter < repetitionsLimit)
            {
                double currentDistance = LocalSearch(routeList);
                repetitionsCounter = CheckRepetition(repetitionsCounter, previousDistance, currentDistance);
                iterationsLimit = CheckImprovement(iterationsLimit, previousDistance, currentDistance);
                iterationsCounter++;
            }
            return new Solution() { Feasible = true, Depot = currentSolution.Depot, Routes = routeList };
        }

        private int CheckImprovement(int iterationsLimit, double previousDistance, double currentDistance)
        {
            return previousDistance - currentDistance > 0 ? iterationsLimit + 5 : iterationsLimit;
        }

        private int CheckRepetition(int repetitionsCounter, double previousDistance, double currentDistance)
        {
            return currentDistance == previousDistance ? ++repetitionsCounter : 0;
        }

        private double LocalSearch(List<Route> routeList)
        {
            double globalMaxDif = 0;
            int globalFirstRouteIndex = 0;
            int globalSecondRouteIndex = 0;
            Route globalMinCostFirstRoute = null;
            Route globalMinCostSecondRoute = null;

            for (int i = 0; i < routeList.Count; i++)
            {
                Route firstRoute = routeList[i];
                for (int j = i + 1; j < routeList.Count; j++)
                {
                    Route secondRoute = routeList[j];
                    Route localMinCostFirstRoute = null;
                    Route localMinCostSecondRoute = null;
                    double oldDistance = firstRoute.TotalDistance +
                                            secondRoute.TotalDistance;
                    double newDistance = Interchange(oldDistance, firstRoute, secondRoute, ref localMinCostFirstRoute, ref localMinCostSecondRoute, Lambda);

                    if (oldDistance - newDistance > globalMaxDif)
                    {
                        globalMaxDif = oldDistance - newDistance;
                        globalFirstRouteIndex = i;
                        globalSecondRouteIndex = j;
                        globalMinCostFirstRoute = localMinCostFirstRoute;
                        globalMinCostSecondRoute = localMinCostSecondRoute;
                    }
                }
            }
            if (globalMaxDif != 0)
            {
                routeList.RemoveAt(globalFirstRouteIndex);
                if (globalMinCostFirstRoute.Customers.Count != 0)
                    routeList.Insert(globalFirstRouteIndex, globalMinCostFirstRoute);
                routeList.RemoveAt(globalSecondRouteIndex);
                if (globalMinCostSecondRoute.Customers.Count != 0)
                    routeList.Insert(globalSecondRouteIndex, globalMinCostSecondRoute);
            }
            return Math.Round(routeList.Sum(x => x.TotalDistance), 2);
        }

        //interchanging customer between 2 given routes using all operators (0,1) (1,0) (2,0) (0,2) (1,2) (2,1) (1,1) (2,2)
        //accepting only mincost solution across all operators
        public double Interchange(double oldDistance, Route firstRoute, Route secondRoute, ref Route minCostFirstRoute, ref Route minCostSecondRoute, int lamda)
        {
            double minDistance = oldDistance;
            minCostFirstRoute = firstRoute.Clone();
            minCostSecondRoute = secondRoute.Clone();

            for (int i = 0; i <= lamda; i++)
            {
                for (int j = 0; j <= lamda; j++)
                {
                    Route firstRouteCopy = firstRoute.Clone();
                    Route secondRouteCopy = secondRoute.Clone();
                    bool feasible = PerformInterchange(firstRouteCopy, secondRouteCopy, i, j);
                    double newDistance = firstRouteCopy.TotalDistance + secondRouteCopy.TotalDistance;
                    if (newDistance < minDistance)
                    {
                        minDistance = newDistance;
                        minCostFirstRoute = firstRouteCopy;
                        minCostSecondRoute = secondRouteCopy;
                    }
                    else
                    {
                        firstRouteCopy = null;
                        secondRouteCopy = null;
                    }
                }
            }
            return minDistance;
        }

        //performing interchange between 2 given routers using given operator
        public bool PerformInterchange(Route firstRoute, Route secondRoute, int operator1, int operator2)
        {
            double minDistance = firstRoute.TotalDistance +
                                  secondRoute.TotalDistance;
            bool feasible = true;
            Random random = new Random();

            while (operator1 != 0 && feasible)
            {
                if (firstRoute.Customers.Count - 2 < 1)
                {
                    feasible = false;
                    break;
                }
                int rand1 = random.Next(1, firstRoute.Customers.Count - 1);
                Customer firstRouteCustomer = firstRoute.Customers[rand1];
                firstRoute.DeleteCustomer(firstRouteCustomer);

                for (int secondRouteCustomerIndex = 0; secondRouteCustomerIndex <= secondRoute.Customers.Count; secondRouteCustomerIndex++)
                {
                    secondRoute.AddCustomer(firstRouteCustomer, secondRouteCustomerIndex);
                    feasible = secondRoute.IsFeasible();
                    if (feasible)
                    {
                        break;
                    }
                    else
                    {
                        secondRoute.DeleteCustomer(firstRouteCustomer);
                    }
                }
                if (!feasible)
                {
                    firstRoute.AddCustomer(firstRouteCustomer, rand1);
                }
                operator1--;
            }

            while (operator2 != 0 && feasible)
            {
                if (secondRoute.Customers.Count - 2 < 1)
                {
                    feasible = false;
                    break;
                }
                int rand2 = random.Next(1, secondRoute.Customers.Count - 1);
                Customer secondRouteCustomer = secondRoute.Customers[rand2];
                secondRoute.DeleteCustomer(secondRouteCustomer);

                for (int firstRouteCustomerIndex = 0; firstRouteCustomerIndex <= firstRoute.Customers.Count; firstRouteCustomerIndex++)
                {
                    firstRoute.AddCustomer(secondRouteCustomer, firstRouteCustomerIndex);
                    feasible = firstRoute.IsFeasible();
                    if (feasible)
                    {
                        break;
                    }
                    else
                    {
                        firstRoute.DeleteCustomer(secondRouteCustomer);
                    }
                }
                if (!feasible)
                {
                    secondRoute.AddCustomer(secondRouteCustomer, rand2);
                }

                operator2--;
            }
            return feasible;
        }
    }
}