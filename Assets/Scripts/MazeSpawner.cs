using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField]
    private int _width;

    [SerializeField]
    private int _height;

    [SerializeField]
    private Cell _cell;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator mazeGenerator = new MazeGenerator();
        var mazeCells = mazeGenerator.GenerateMap(_width, _height);

        for (int x = 0; x < mazeCells.GetLength(0); x++)
        {
            for (int y = 0; y < mazeCells.GetLength(1); y++)
            {
                Cell cell = Instantiate(_cell, new Vector3(x, 0, y), Quaternion.identity);
                cell.SetActiveWallBottom(mazeCells[x, y]._activeBottomWall);
                cell.SetActiveWallLeft(mazeCells[x, y]._activeLeftWall);

            }
        }
    }
}
