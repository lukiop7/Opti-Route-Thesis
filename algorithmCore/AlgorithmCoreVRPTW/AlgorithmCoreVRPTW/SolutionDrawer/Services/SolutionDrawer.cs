using AlgorithmCoreVRPTW.Common;
using AlgorithmCoreVRPTW.Models;
using OptiRoute.Shared.SolutionDrawer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace OptiRoute.Shared.SolutionDrawer
{
    public class SolutionDrawer : ISolutionDrawer
    {
        private readonly int Radius;
        private readonly NavigationList<Brush> RouteBrushes;
        private readonly int Height;
        private readonly int Width;
        public SolutionDrawer() 
        {
            Radius = 3;
            Height = 1000;
            Width = 1000;
            RouteBrushes = new NavigationList<Brush>() {
                    Brushes.Black,
                    Brushes.Blue,
                    Brushes.Green,
                    Brushes.Yellow,
                    Brushes.Purple,
                    Brushes.Pink
                    };
        }
        public DrawSolutionResponseDto DrawSolution(Solution solution, string path)
        {
            path += DateTime.Now.ToString("yyyyMMddHHmmss") +".png";
            var routes = solution.Routes;
            var depot = solution.Depot;
            int offset = 100;
            int scale = 4;
            try
            {
                using (Bitmap bmp = new Bitmap(Width,Height))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Rectangle ImageSize = new Rectangle(0, 0, Width, Height);
                    g.FillRectangle(Brushes.White, ImageSize);

                    Pen linePen = new Pen(RouteBrushes.Current);
                    Pen pointsPen = new Pen(Brushes.Red);
                    Pen depotPen = new Pen(Brushes.Orange);

                    g.FillCircle(Brushes.Orange, (depot.X + offset) * scale, (depot.Y + offset) * scale, Radius);
                    g.DrawCircle(depotPen, (depot.X + offset) * scale, (depot.Y + offset) * scale, Radius);

                    foreach (var route in routes)
                    { 
                        var customers = route.Customers;
                        for (int i = 0; i < customers.Count; i++)
                        {
                            g.FillCircle(Brushes.Red, (customers[i].X+ offset) * scale, (customers[i].Y + offset) * scale, Radius);
                            g.DrawCircle(pointsPen, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, Radius);

                            if(i ==0)
                                g.DrawLine(linePen,  (depot.X + offset) * scale, (depot.Y + offset) * scale, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale);

                            if (i == customers.Count - 1)
                                g.DrawLine(linePen, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, (depot.X + offset) * scale, (depot.Y + offset) * scale);
                            else
                                g.DrawLine(linePen, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, (customers[i + 1].X + offset) * scale, (customers[i + 1].Y + offset) * scale);

                        }
                        linePen.Brush = RouteBrushes.MoveNext;
                    }

                    bmp.Save(path, ImageFormat.Png);
                }
                return new DrawSolutionResponseDto { SuccessfullyDrawn=true};
            }
            catch 
            {
                return new DrawSolutionResponseDto { SuccessfullyDrawn = false };
            }
        }
    }
}
