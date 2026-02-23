using System;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectGenerator
{
    internal static void CreateObjects(List<Vector2Int> roomCenters, MazeVisualizer mazeVisualizer)
    {
        CreatePlayer(mazeVisualizer, roomCenters);
        CreateCheese(mazeVisualizer, roomCenters);
    }

    private static void CreateCheese(MazeVisualizer mazeVisualizer, List<Vector2Int> roomCenters)
    {
        mazeVisualizer.PlaceCheese(roomCenters);
    }

    private static void CreatePlayer(MazeVisualizer mazeVisualizer, List<Vector2Int> roomCenters)
    {
        mazeVisualizer.PlacePlayer(roomCenters);
    }
}
