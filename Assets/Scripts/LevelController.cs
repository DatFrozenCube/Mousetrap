using UnityEngine;
using System;
using MoreMountains.Feedbacks;
using System.Collections;

public class LevelController : MonoBehaviour
{
    public static int LevelNumber = 1;
    public static Action LevelActions;

    public static void DestroyLevel()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = new Vector3(0, 0, 0);

        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");
        foreach (var trap in traps)
        {
            Destroy(trap);
        }

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            Destroy(wall);
        }

        GameObject cheese = GameObject.FindGameObjectWithTag("Cheese");
        Destroy(cheese);
    }
}
