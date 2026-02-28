using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomFirstMazeGenerator : SimpleRandomWalkMazeGenerator
{
    [SerializeField] private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField] private int mazeWidth = 20, mazeHeight = 20;
    [SerializeField][Range(0, 10)] private int offset = 1;
    [SerializeField] private int minTrapsPerRoom = 5, maxTrapsPerRoom = 10;
    [SerializeField] private int minPowerUpsPerRoom = 1, maxPowerUpsPerRoom = 3;
    [SerializeField] private bool randomWalkRooms = true;
    [SerializeField] private bool enableFloorGizmos = false;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    private List<Color> roomColors = new List<Color>();
    private List<Vector2Int> roomCenters = new List<Vector2Int>();
    private HashSet<Vector2Int> floorPositions, corridorPositions, mapPositions;

    private void Awake()
    {
        LevelController.LevelActions += MazeNextLevel;
    }

    private void OnDestroy()
    {
        LevelController.LevelActions -= MazeNextLevel;
    }

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void MazeNextLevel()
    {
        mazeWidth += 5;
        mazeHeight += 5;
        Debug.Log($"Increased maze size to {mazeWidth}x{mazeHeight}");
    }

    private void CreateRooms()
    {
        var roomsList = MazeGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(mazeWidth, mazeHeight, 0)), minRoomWidth, minRoomHeight);
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        ClearRoomData();
        RoomTypes.ClearRoomTypes();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }

        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

        mapPositions = new HashSet<Vector2Int>();
        floorPositions = floor;
        corridorPositions = corridors;
        mapPositions.UnionWith(corridors);
        mapPositions.UnionWith(floor);

        mazeVisualizer.PaintFloorTiles(mapPositions);
        WallGenerator.CreateWalls(mapPositions, mazeVisualizer);
        mazeVisualizer.PlacePlayer(roomsDictionary);
        mazeVisualizer.PlaceCheese(roomsDictionary);
        RoomTypes.AssignRandomRoomTypes(roomsDictionary);
        mazeVisualizer.PlaceObjects(roomsDictionary, ObjectPlacementHelper.PlacementType.OpenSpace, MazeVisualizer.ObjectType.Trap, minTrapsPerRoom, maxTrapsPerRoom);
        mazeVisualizer.PlaceObjects(roomsDictionary, ObjectPlacementHelper.PlacementType.OpenSpace, MazeVisualizer.ObjectType.Powerup, minPowerUpsPerRoom, maxPowerUpsPerRoom);
    }

    private void ClearRoomData()
    {
        roomsDictionary.Clear();
        roomColors.Clear();
        roomCenters.Clear();
    }

    private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> roomFloor)
    {
        roomsDictionary[roomPosition] = roomFloor;
        roomColors.Add(UnityEngine.Random.ColorHSV());
        roomCenters.Add(roomPosition);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            HashSet<Vector2Int> currentRoomFloor = new HashSet<Vector2Int>();

            foreach (var position in roomFloor)
            {
                if (position.x >= roomBounds.xMin + offset && position.x < roomBounds.xMax - offset && position.y > roomBounds.yMin + offset && position.y < roomBounds.yMax - offset)
                {
                    floor.Add(position);
                    currentRoomFloor.Add(position);
                }
            }

            SaveRoomData(roomCenter, currentRoomFloor);
        }

        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        List<Vector2Int> roomCentersCopy = new List<Vector2Int>(roomCenters);
        var currentRoomCenter = roomCentersCopy[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCentersCopy.Remove(currentRoomCenter);

        while (roomCentersCopy.Count > 0)
        {
            Vector2Int closest = FindClosestPoint(currentRoomCenter, roomCentersCopy);
            roomCentersCopy.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }

            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }

            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosestPoint(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;

        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2Int.Distance(currentRoomCenter, position);

            if (currentDistance < length)
            {
                length = currentDistance;
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>();

            for (int column = offset; column < room.size.x - offset; column++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floor.Add(position);
                    roomFloor.Add(position);
                }
            }

            SaveRoomData(new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y)), roomFloor);
        }

        return floor;
    }

    private void OnDrawGizmosSelected()
    {
        if (enableFloorGizmos)
        {
            int i = 0;

            foreach (var roomCenter in roomsDictionary.Keys)
            {
                Gizmos.color = roomColors[i];
                HashSet<Vector2Int> currentPositions = roomsDictionary[roomCenter];

                foreach (var position in floorPositions)
                {
                    if (currentPositions.Contains(position))
                    {
                        Gizmos.DrawCube((Vector3Int)position, Vector3.one);
                    }
                }

                i++;
            }

            if (corridorPositions != null)
            {
                foreach (var position in corridorPositions)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube((Vector3Int)position, Vector3.one);
                }
            }
        }
    }
}
