using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Cell
    {
        public Surface Surface { get; set; }
        public Point From { get; set; }
        public int Cost { get; set; }

        public bool IsVisited { get; set; }

        public Cell(Surface surface)
        {
            Surface = surface;
            From = new Point();
            Cost = int.MaxValue;
            IsVisited = false;
        }
    }
}
