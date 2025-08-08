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
    public int Rows = 4;
    public int Columns = 4;
    public float CellWidth = 3;
    public float CellHeight = 3;
    public bool AddGaps = false;
    public bool AddTraps = true;
    public int Traps = 2;
    public float XOffset = 0;
    public float YOffset = 0;
    public int LevelProgressionRate = 2;

    private BasicMazeGenerator mMazeGenerator;
    private int TrapCounter = 0;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        LevelController.LevelActions += IncrementLevel;
        LevelController.LevelActions += GenerateLevel;
        GenerateLevel();
    }

    private void IncrementLevel()
    {
        if (LevelController.LevelNumber % 2 == 0)
        {
            Traps = 2 * LevelController.LevelNumber;
        }

        if (LevelController.LevelNumber % 2 == 0)
        {
            Rows += LevelProgressionRate;
            Columns += LevelProgressionRate;
        }
    }

    public void GenerateLevel()
    {
        TrapCounter = 0;

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
                if (cell.IsStart)
                {
                    player.position = new Vector3(x, y, 0);
                }
                if (AddTraps)
                {
                    if (cell.IsTrap && Trap != null && TrapCounter < Traps && Random.Range(0, 3) == 1)
                    {
                        //Makes it so that a trap cannot be in the direct center of the cell
                        float trapOffsetY;
                        float trapOffsetX;
                        if (Random.Range(0, 2) == 1)
                        {
                            trapOffsetY = Random.Range(-CellHeight / 2, -CellHeight / 4);
                        }

                        else
                        {
                            trapOffsetY = Random.Range(CellHeight / 4, CellHeight / 2);
                        }

                        if (Random.Range(0, 2) == 1)
                        {
                            trapOffsetX = Random.Range(-CellWidth / 2, -CellWidth / 4);
                        }

                        else
                        {
                            trapOffsetX = Random.Range(CellWidth / 4, CellWidth / 2);
                        }

                        Vector3 trapPosition = new Vector3(x + trapOffsetX, y + trapOffsetY, 0);
                        float distanceToPlayer = Vector3.Distance(player.position, trapPosition);

                        //Prevents traps from spawning on top of the player
                        if (distanceToPlayer > player.GetComponent<Mouse>().TrapDetectionRadius)
                        {
                            tmp = Instantiate(Trap, trapPosition, Quaternion.identity) as GameObject;
                            tmp.transform.parent = transform;
                            TrapCounter++;
                        }
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

        //Detect if goal is too close to player
        Mouse mouse = player.gameObject.GetComponent<Mouse>();
        bool resetPosition = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, mouse.GoalDetectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Cheese"))
            {
                resetPosition = true;
            }
        }

        if (resetPosition)
        {
            mouse.ResetPlayerPosition();
            Debug.Log("Player too close to goal; moving");
        }
    }
}
