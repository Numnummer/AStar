using System.Drawing;

namespace AStar
{
    public static class PathFinder
    {
        public static int GetDistance(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public static Point[] GetNeighbours<T>(T[,] landscape, Point current)
        {
            var x = current.X;
            var y = current.Y;
            var points = new Point[8]
            {
                new Point(x+1,y),
                new Point(x-1,y),
                new Point(x,y+1),
                new Point(x,y-1),
                new Point(x+1,y+1),
                new Point(x+1,y-1),
                new Point(x-1,y+1),
                new Point(x-1,y-1)
            };
            return points.Where(point => IsNotOutOfRange<T>(point, landscape)).ToArray();
        }

        public static bool IsNotOutOfRange<T>(Point point, T[,] arr)
        {
            try
            {
                var temp = arr[point.Y, point.X];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            return true;
        }

        public static LinkedList<Point> AStar(Cell[,] landscape, Point start, Point end)
        {
            LinkedList<Point> path = new LinkedList<Point>();
            Point current = start;
            int costPluser;
            landscape[start.Y, start.X].Cost = 0;
            var frontier = new PriorityQueue<Point, int>();

            while (current != end)
            {
                var neighbours = GetNeighbours(landscape, current);
                var currentCell = landscape[current.Y, current.X];
                //(Point, int) minimum = (new Point(), int.MaxValue);

                foreach (var neighbour in neighbours)
                {
                    var currentNeighbourCell = landscape[neighbour.Y, neighbour.X];
                    if (currentNeighbourCell.IsVisited)
                    {
                        continue;
                    }
                    switch (currentNeighbourCell.Surface)
                    {
                        case Surface.ground:
                            costPluser = 1;
                            break;
                        case Surface.rock:
                            costPluser = 2;
                            break;
                        case Surface.spikes:
                            costPluser = 3;
                            break;
                        default:
                            costPluser = 1;
                            break;
                    }
                    costPluser += GetDistance(neighbour, end);
                    if (currentNeighbourCell.Cost > (currentCell.Cost + costPluser))
                    {
                        currentNeighbourCell.From = current;
                        currentNeighbourCell.Cost = currentCell.Cost + costPluser;
                    }
                    frontier.Enqueue(neighbour, currentNeighbourCell.Cost);
                    currentNeighbourCell.IsVisited = true;
                }
                //currentCell.IsVisited = true;
                current = frontier.Dequeue();
            }

            while (current != start)
            {
                path.AddFirst(current);
                current = landscape[current.Y, current.X].From;
            }

            return path;
        }
    }
}