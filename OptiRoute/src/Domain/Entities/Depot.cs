using System;
using System.Collections.Generic;

namespace OptiRoute.Domain.Entities
{
    public class Depot
    {
        public int DbId { get; set; }
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int DueDate { get; set; }
        public Solution Solution { get; set; }
        public List<Route> Routes { get; set; } = new List<Route>();
        public static Depot Parse(string input)
        {
            var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Depot()
            {
                Id = 0,
                X = Int32.Parse(parts[1]),
                Y = Int32.Parse(parts[2]),
                DueDate = Int32.Parse(parts[5]),
            };
        }
    }
}