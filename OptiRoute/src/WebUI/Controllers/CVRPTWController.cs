using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiRoute.Application.CVRPTW.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptiRoute.WebUI.Controllers
{
    public class CVRPTWController : ApiControllerBase
    {
        private readonly ISolver solver;
        private readonly IMapper mapper;

        public CVRPTWController(ISolver _solver, IMapper mapper)
        {
            this.solver = _solver;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SolutionViewModel>> GetSolution(ProblemViewModel problem)
        {
            Problem problemMapped = new Problem()
            {
                Capacity = problem.Capacity,
                Depot = mapper.Map<DepotViewModel,Depot>(problem.Depot),
                Customers = mapper.Map<List<CustomerViewModel>,List<Customer>>(problem.Customers),
                Distances = problem.Distances,
                Durations = problem.Durations,
                Vehicles = problem.Vehicles
            };
            var solution = this.solver.Solve(problemMapped);
            List<RouteViewModel> routeViewModels = solution.Routes.Select(x => new RouteViewModel()
            {
                Customers = x.Customers.Select(y => y.Id).ToList()
            }).ToList();
            SolutionViewModel returnedSolution = new SolutionViewModel()
            {
                Distance = solution.Distance,
                Feasible = solution.Feasible,
                Routes = routeViewModels
            };
            return returnedSolution;
        }
    }
}
