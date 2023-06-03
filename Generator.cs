using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public static class Generator
    {
        public static Cell[,] GenerateLandscape(Size size)
        {
            var landcape = new Cell[size.Height, size.Width];
            var random = new Random();
            for (int i = 0; i < size.Height; i++)
            {
                for (int j = 0; j < size.Width; j++)
                {
                    switch (random.Next(3))
                    {
                        case 0:
                            landcape[i, j] = new Cell(Surface.ground);
                            break;
                        case 1:
                            landcape[i, j] = new Cell(Surface.rock);
                            break;
                        case 2:
                            landcape[i, j] = new Cell(Surface.spikes);
                            break;
                        default:
                            break;
                    }
                    landcape[i, j].Cost = int.MaxValue;
                }
            }
            return landcape;
        }
    }
}
