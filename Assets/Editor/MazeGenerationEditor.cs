using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractMazeGenerator), true)]
public class MazeGenerationEditor : Editor
{
    AbstractMazeGenerator generator;

    private void Awake()
    {
        generator = (AbstractMazeGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Maze"))
        {
            generator.GenerateMaze();
        }
    }
}
