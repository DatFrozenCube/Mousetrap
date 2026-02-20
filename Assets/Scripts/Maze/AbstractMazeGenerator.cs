using UnityEngine;

public abstract class AbstractMazeGenerator : MonoBehaviour
{
    [SerializeField]
    protected MazeVisualizer mazeVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateMaze()
    {
        mazeVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
