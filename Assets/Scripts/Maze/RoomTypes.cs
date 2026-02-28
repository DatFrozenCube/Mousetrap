using System;
using System.Collections.Generic;
using UnityEngine;

public static class RoomTypes
{
    public static Dictionary<Vector2Int, RoomType> roomTypesDictionary = new Dictionary<Vector2Int, RoomType>();

    public enum RoomType
    {
        Spawn, Goal, Trap, Powerup
    }

    public static void AssignRoomType(Vector2Int roomCenter, RoomType roomType)
    {
        RoomTypes.roomTypesDictionary[roomCenter] = roomType;
    }

    public static void ClearRoomTypes()
    {
        RoomTypes.roomTypesDictionary.Clear();
    }
    
    public static void AssignRandomRoomTypes(Dictionary<Vector2Int, HashSet<Vector2Int>> roomDictionary)
    {
        foreach (var roomCenter in roomDictionary.Keys)
        {
            if (!roomTypesDictionary.ContainsKey(roomCenter))
            {
                RoomType randomRoomType = UnityEngine.Random.value < 0.7f ? RoomType.Trap : RoomType.Powerup;
                AssignRoomType(roomCenter, randomRoomType);
            }
        }
    }
}
