using System.Collections.Generic;

namespace OptiRoute.Shared.SolutionDrawer.Models
{
    public class DrawSolutionRequestDto
    {
        public List<(int x, int y)> Points { get; set; }
        public List<int> Route { get; set; }
        public string Path { get; set; }
    }
}