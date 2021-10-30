using AlgorithmCoreVRPTW.Common;
using AlgorithmCoreVRPTW.Models;
using OptiRoute.Shared.SolutionDrawer.Models;
using System;
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
        private readonly int ColorsOffset;

        public SolutionDrawer()
        {
            Radius = 3;
            Height = 1000;
            Width = 1000;
            ColorsOffset = 49;
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
            System.IO.Directory.CreateDirectory(path);
            path += DateTime.Now.ToString("yyyyMMddHHmmss")+"_NEW" + ".png";
            var routes = solution.Routes;
            var depot = solution.Depot;
            int offset = 100;
            int scale = 4;
            try
            {
                using (Bitmap bmp = new Bitmap(Width, Height))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Rectangle ImageSize = new Rectangle(0, 0, Width, Height);
                    g.FillRectangle(Brushes.White, ImageSize);

                    Pen pointsPen = new Pen(Brushes.Red);
                    Pen depotPen = new Pen(Brushes.Orange);

                    g.FillCircle(Brushes.Orange, (depot.X + offset) * scale, (depot.Y + offset) * scale, Radius);
                    g.DrawCircle(depotPen, (depot.X + offset) * scale, (depot.Y + offset) * scale, Radius);

                    for (int r = 0; r < routes.Count; r++)
                    {
                        Pen linePen = new Pen(Color.FromKnownColor((KnownColor)(r + ColorsOffset)));

                        var customers = routes[r].Customers;
                        for (int i = 0; i < customers.Count; i++)
                        {
                            g.FillCircle(linePen.Brush, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, Radius);
                            g.DrawCircle(linePen, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, Radius);

                            if (i == 0)
                                g.DrawLine(linePen, (depot.X + offset) * scale, (depot.Y + offset) * scale, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale);

                            if (i == customers.Count - 1)
                                g.DrawLine(linePen, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, (depot.X + offset) * scale, (depot.Y + offset) * scale);
                            else
                                g.DrawLine(linePen, (customers[i].X + offset) * scale, (customers[i].Y + offset) * scale, (customers[i + 1].X + offset) * scale, (customers[i + 1].Y + offset) * scale);
                        }
                    }
                    WriteDistance(g, solution);

                    bmp.Save(path, ImageFormat.Png);
                }
                return new DrawSolutionResponseDto { SuccessfullyDrawn = true };
            }
            catch
            {
                return new DrawSolutionResponseDto { SuccessfullyDrawn = false };
            }
        }

        private void WriteDistance(Graphics g, Solution solution)
        {
            Font font = new Font("Arial", 20, FontStyle.Italic, GraphicsUnit.Pixel);
            Color color = Color.Black;
            Point atpoint = new Point(Width / 2, Height / 5);
            SolidBrush brush = new SolidBrush(color);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString($"Distance: {Math.Round(solution.Distance, 2)}, Vehicles: {solution.Routes.Count}", font, brush, atpoint, sf);
        }
    }
}