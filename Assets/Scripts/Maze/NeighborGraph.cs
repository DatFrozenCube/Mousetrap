using System.Collections.Generic;
using UnityEngine;

public class NeighborGraph
{
    private static List<Vector2Int> Neighbors4Directions  = new List<Vector2Int>
    {
        new Vector2Int(0, 1),   // Up
        new Vector2Int(1, 0),   // Right
        new Vector2Int(0, -1),  // Down
        new Vector2Int(-1, 0)   // Left
    };

    private static List<Vector2Int> Neighbors8Directions  = new List<Vector2Int>
    {
        new Vector2Int(0, 1),   // Up
        new Vector2Int(1, 0),   // Right
        new Vector2Int(0, -1),  // Down
        new Vector2Int(-1, 0),  // Left
        new Vector2Int(1, 1),   // Up-Right
        new Vector2Int(1, -1),  // Down-Right
        new Vector2Int(-1, -1), // Down-Left
        new Vector2Int(-1, 1)   // Up-Left
    };

    List<Vector2Int> graph;

    public NeighborGraph(IEnumerable<Vector2Int> verticies)
    {
        graph = new List<Vector2Int>(verticies);
    }

    public List<Vector2Int> GetNeighbors(Vector2Int startPosition, bool includeDiagonals)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        List<Vector2Int> directions = includeDiagonals ? Neighbors8Directions : Neighbors4Directions;
        foreach (var direction in directions)
        {
            Vector2Int neighborPos = startPosition + direction;
            if (graph.Contains(neighborPos))
                neighbors.Add(neighborPos);
        }
        return neighbors;
    }
}
