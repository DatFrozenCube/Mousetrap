using UnityEngine;

[CreateAssetMenu(fileName = "MazePresetParameters", menuName = "PCG/SimpleRandomWalkData")]
public class MazePresetSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}