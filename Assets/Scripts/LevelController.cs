using UnityEngine;
using System;
using MoreMountains.Feedbacks;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public static int LevelNumber = 1;
    public static Action LevelActions;

    private void Awake()
    {
        RoomFirstMazeGenerator mazeGenerator = GameObject.FindGameObjectWithTag("MazeGenerator").GetComponent<RoomFirstMazeGenerator>();
        LevelActions += mazeGenerator.MazeNextLevel;
    }
}
