using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeVisualizer : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [Header("Tiles")]
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallTop;
    [SerializeField] private TileBase wallSideRight;
    [SerializeField] private TileBase wallSideLeft;
    [SerializeField] private TileBase wallBottom;
    [SerializeField] private TileBase wallFull;
    [SerializeField] private TileBase wallInnerCornerDownLeft;
    [SerializeField] private TileBase wallInnerCornerDownRight;
    [SerializeField] private TileBase wallDiagonalCornerDownRight;
    [SerializeField] private TileBase wallDiagonalCornerDownLeft;
    [SerializeField] private TileBase wallDiagonalCornerUpRight;
    [SerializeField] private TileBase wallDiagonalCornerUpLeft;

    [Header("Objects")]
    [SerializeField] private Cheese cheesePrefab;
    [SerializeField] private Trap trapPrefab;
    [SerializeField] private Powerup powerUpPrefab;
    [SerializeField] private Mouse player;

    private enum RoomType
    {
        Spawn, Goal, Trap, Powerup
    }

    public enum ObjectType
    {
        Trap, Powerup
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    internal void PlacePlayer(Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary)
    {
        List<Vector2Int> roomCenters = new List<Vector2Int>(roomsDictionary.Keys);
        Vector2Int trySpawn = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        Vector3 gridSnap = floorTilemap.GetCellCenterWorld(floorTilemap.LocalToCell((Vector3Int)trySpawn));

        bool isSpawnedInWall = player.SpawnPlayer((Vector2)gridSnap);

        if (isSpawnedInWall)
        {
            PlacePlayer(roomsDictionary);
        }

        else
        {
            RoomTypes.AssignRoomType(trySpawn, RoomTypes.RoomType.Spawn);
        }
    }

    internal void PlaceCheese(Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary)
    {
        Vector2Int playerPosition = new Vector2Int(Mathf.RoundToInt(player.GetComponent<Transform>().position.x), Mathf.RoundToInt(player.GetComponent<Transform>().position.y));
        float greatestDistance = 0f;
        Vector2Int furthestRoomCenter = Vector2Int.zero;

        foreach (var roomCenter in roomsDictionary.Keys)
        {
            float currentDistance = Vector2Int.Distance(playerPosition, roomCenter);
            if (currentDistance > greatestDistance)
            {
                greatestDistance = currentDistance;
                furthestRoomCenter = roomCenter;
            }
        }

        Vector3 gridSnap = floorTilemap.GetCellCenterWorld(floorTilemap.LocalToCell((Vector3Int)furthestRoomCenter));

        Instantiate(cheesePrefab, gridSnap, Quaternion.identity);
        RoomTypes.AssignRoomType(furthestRoomCenter, RoomTypes.RoomType.Goal);
    }

    internal void PlaceObjects(Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary, ObjectPlacementHelper.PlacementType placementType, ObjectType objectType, int minObjects, int maxObjects)
    {
        foreach (var roomCenter in roomsDictionary.Keys)
        {
            ObjectPlacementHelper helper = new ObjectPlacementHelper(roomsDictionary[roomCenter]);
            int objectsToPlace = UnityEngine.Random.Range(minObjects, maxObjects + 1);

            if (helper.roomTileByType.ContainsKey(placementType))
            {
                if (objectsToPlace > helper.roomTileByType[placementType].Count)
                {
                    objectsToPlace = helper.roomTileByType[placementType].Count;
                }
            }

            int totalObjectsPlaced = 0;
            List<Vector2Int> objectLocations = new List<Vector2Int>();

            while (totalObjectsPlaced < objectsToPlace)
            {
                Vector2Int objectPosition = helper.GetObjectPlacement(placementType);

                //Go to the next room if there are no valid trap positions in the current room
                if (objectPosition == Vector2Int.zero)
                {
                    break;
                }

                objectLocations.Add(objectPosition);
                totalObjectsPlaced++;
            }

            if (RoomTypes.roomTypesDictionary[roomCenter] == RoomTypes.RoomType.Trap && objectType == ObjectType.Trap)
            {
                ForEachObject(trapPrefab.gameObject, objectLocations);
            }

            else if (RoomTypes.roomTypesDictionary[roomCenter] == RoomTypes.RoomType.Powerup && objectType == ObjectType.Powerup)
            {
                ForEachObject(powerUpPrefab.gameObject, objectLocations);
            }
        }
    }

    private void ForEachObject(GameObject prefab, List<Vector2Int> objectLocations)
    {
        foreach (var objectLocation in objectLocations)
        {
            Vector3 gridSnap = floorTilemap.GetCellCenterWorld(floorTilemap.LocalToCell((Vector3Int)objectLocation));
            Instantiate(prefab, gridSnap, Quaternion.identity);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallByteTypes.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallByteTypes.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallByteTypes.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallByteTypes.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        DestroyImmediate(GameObject.FindGameObjectWithTag("Cheese"));
        foreach (var trap in GameObject.FindGameObjectsWithTag("Trap"))
        {
            DestroyImmediate(trap);
        }
        foreach (var powerUp in GameObject.FindGameObjectsWithTag("Powerup"))
        {
            DestroyImmediate(powerUp);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallByteTypes.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
