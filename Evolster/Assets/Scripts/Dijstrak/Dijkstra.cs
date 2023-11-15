using System.Collections.Generic;

public class Dijkstra
{
    public static int[,] CalculateDistances(Tile[,] tiles, int startX, int startY)
    {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);

        int[,] distances = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                distances[x, y] = int.MaxValue;
            }
        }

        distances[startX, startY] = 0;

        Queue<(int, int)> queue = new Queue<(int, int)>();
        queue.Enqueue((startX, startY));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int x = current.Item1;
            int y = current.Item2;

            List<(int, int)> neighbors = GetNeighbors(x, y, width, height);

            foreach (var neighbor in neighbors)
            {
                int nx = neighbor.Item1;
                int ny = neighbor.Item2;

                int newDistance = distances[x, y] + 1; // Assuming each step has a distance of 1
                if (newDistance < distances[nx, ny] && tiles[nx, ny].walkable)
                {
                    distances[nx, ny] = newDistance;
                    queue.Enqueue((nx, ny));
                }
            }
        }

        return distances;
    }

    private static List<(int, int)> GetNeighbors(int x, int y, int width, int height)
    {
        List<(int, int)> neighbors = new List<(int, int)>();

        if (x > 0) neighbors.Add((x - 1, y));
        if (x < width - 1) neighbors.Add((x + 1, y));
        if (y > 0) neighbors.Add((x, y - 1));
        if (y < height - 1) neighbors.Add((x, y + 1));

        return neighbors;
    }
}

