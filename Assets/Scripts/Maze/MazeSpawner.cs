using UnityEngine;
using System.Collections;
using System.Data;

public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = true;
    public int RandomSeed = 12345;
    public GameObject Wall;
    public GameObject Pillar;
    public GameObject Goal;
    public GameObject Trap;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 4;
    public float CellHeight = 4;
    public bool AddGaps = false;
    public bool AddTraps = true;
    public float XOffset = 0;
    public float YOffset = 0;

    private BasicMazeGenerator mMazeGenerator;

    private void Start()
    {
        LevelController.LevelActions += GenerateLevel;
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        if (!FullRandom)
        {
            Random.InitState(RandomSeed);
        }

        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeAlgorithm(Rows, Columns);
                break;
        }

        mMazeGenerator.GenerateMaze();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = (column + XOffset) * (CellWidth + (AddGaps ? .2f : 0));
                float y = (row + YOffset) * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;

                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, y, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 90)) as GameObject;// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, y + CellHeight / 2, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, y, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 270)) as GameObject;// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, y - CellHeight / 2, 0) + Wall.transform.position, Quaternion.Euler(0, 0, 180)) as GameObject;// back
                    tmp.transform.parent = transform;
                }
                if (cell.IsGoal && Goal != null)
                {
                    tmp = Instantiate(Goal, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    tmp.transform.parent = transform;
                }
                if (AddTraps)
                {
                    if (cell.IsTrap && Trap != null)
                    {
                        float trapOffsetY = Random.Range(-CellHeight / 2, CellHeight / 2);
                        float trapOffsetX = Random.Range(-CellWidth / 2, CellWidth / 2);
                        tmp = Instantiate(Trap, new Vector3(x + trapOffsetX, y + trapOffsetY, 0), Quaternion.identity) as GameObject;
                        tmp.transform.parent = transform;
                    }
                }
            }
        }

        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = (column + XOffset) * (CellWidth + (AddGaps ? .2f : 0));
                    float y = (row + YOffset) * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, y - CellHeight / 2, 0), Quaternion.identity) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }
    }
}
