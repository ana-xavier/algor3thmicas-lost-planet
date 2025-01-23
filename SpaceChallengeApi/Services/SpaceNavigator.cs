namespace SpaceChallengeApi.Services
{
    public class SpaceNavigator
    {
        private readonly char[,] map;
        private readonly (int X, int Y) start;
        private readonly (int X, int Y) finish;
        private readonly int lines;
        private readonly int columns;

        public SpaceNavigator(char[,] map)
        {
            this.map = map;
            lines = map.GetLength(0);
            columns = map.GetLength(1);

            start = FindPosition('F');
            finish = FindPosition('P');
        }

        public List<(int X, int Y)> FindShortestPath()
        {
            var openSet = new PriorityQueue<(int X, int Y), int>();
            var openSetHashSet = new HashSet<(int X, int Y)>();
            var cameFrom = new Dictionary<(int X, int Y), (int X, int Y)>();
            var gScore = new Dictionary<(int X, int Y), int>();
            var fScore = new Dictionary<(int X, int Y), int>();

            openSet.Enqueue(start, 0);
            openSetHashSet.Add(start);
            gScore[start] = 0;
            fScore[start] = Heuristic(start, finish);

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();
                openSetHashSet.Remove(current);

                if (current == finish)
                    return ReconstructPath(cameFrom, current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    int tentativeGScore = gScore[current] + 1;

                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, finish);

                        if (!openSetHashSet.Contains(neighbor))
                        {
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                            openSetHashSet.Add(neighbor);
                        }
                    }
                }
            }

            return new List<(int X, int Y)>();
        }

        private (int X, int Y) FindPosition(char target)
        {
            for (int x = 0; x < lines; x++)
                for (int y = 0; y < columns; y++)
                    if (map[x, y] == target)
                        return (x, y);

            throw new Exception($"Target '{target}' not found on the map.");
        }

        private int Heuristic((int X, int Y) a, (int X, int Y) b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private List<(int X, int Y)> GetNeighbors((int X, int Y) position)
        {
            var neighbors = new List<(int X, int Y)>();
            var directions = new (int X, int Y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

            foreach (var (dx, dy) in directions)
            {
                int newX = position.X + dx;
                int newY = position.Y + dy;

                if (newX >= 0 && newX < lines && newY >= 0 && newY < columns && map[newX, newY] != '#')
                {
                    neighbors.Add((newX, newY));
                }
            }

            return neighbors;
        }

        private List<(int X, int Y)> ReconstructPath(Dictionary<(int X, int Y), (int X, int Y)> cameFrom, (int X, int Y) current)
        {
            var path = new List<(int X, int Y)> { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path;
        }

        public static char[,] LoadMapFromFile(string filePath)
        {
            var lines = System.IO.File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Length;
            var map = new char[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    map[i, j] = lines[i][j];

            return map;
        }

    }
}