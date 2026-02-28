using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static ObjectPlacementHelper;

public class ObjectPlacementHelper
{
    public Dictionary<PlacementType, List<Vector2Int>> roomTileByType;

    HashSet<Vector2Int> roomFloorNoCorridor;

    public ObjectPlacementHelper(HashSet<Vector2Int> currentRoomFloorPositions)
    {
        roomTileByType = new Dictionary<PlacementType, List<Vector2Int>>();
        NeighborGraph neighborGraph = new NeighborGraph(currentRoomFloorPositions);

        foreach (var position in currentRoomFloorPositions)
        {
            int neighborCount = neighborGraph.GetNeighbors(position, true).Count;
            PlacementType placementType = neighborCount < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;

            if (!roomTileByType.ContainsKey(placementType))
            {
                roomTileByType[placementType] = new List<Vector2Int>();
            }

            if (placementType == PlacementType.NearWall)
            {
                roomTileByType[PlacementType.NearWall].Add(position);
            }
            else
            {
                roomTileByType[PlacementType.OpenSpace].Add(position);
            }
        }
    }

    public Vector2Int GetObjectPlacement(PlacementType placementType)
    {
        int index;
        try
        {
            index = Random.Range(0, roomTileByType[placementType].Count);
        }
        catch
        {
            return Vector2Int.zero; // No available positions of the specified type
        }

        Vector2Int position = roomTileByType[placementType].ElementAt(index);
        roomTileByType[placementType].Remove(position);
        return position;
    }

    public enum PlacementType
    {
        NearWall, OpenSpace
    }
}
