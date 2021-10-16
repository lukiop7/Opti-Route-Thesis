using OptiRoute.Shared.SolutionDrawer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace OptiRoute.Shared.SolutionDrawer
{
    public class SolutionDrawer : ISolutionDrawer
    {
        public DrawSolutionResponseDto DrawSolution(List<(int x, int y)> points, List<int> route, string path)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(600, 600))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    int radius = 2;
                    Rectangle ImageSize = new Rectangle(0, 0, 600, 600);
                    g.FillRectangle(Brushes.White, ImageSize);

                    Pen linesPen = new Pen(Brushes.Black);
                    Pen pointsPen = new Pen(Brushes.Red);
                    foreach (var point in points)
                    {
                        g.FillCircle(Brushes.Red, point.x, point.y, radius);
                        g.DrawCircle(pointsPen, point.x, point.y, radius);
                    }
                    for (int i = 0; i < route.Count; i++)
                    {
                        if(i!=route.Count-1)
                        g.DrawLine(linesPen, points[i].x, points[i].y, points[i + 1].x, points[i + 1].y);
                        else
                            g.DrawLine(linesPen, points[i].x, points[i].y, points[0].x, points[0].y);
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
