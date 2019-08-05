using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public MazeCell[,] GenerateMap(int width, int height)
    {
        MazeCell[,] maze = new MazeCell[width, height];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }
        }

        GenerateMaze(maze);
        MakePass(maze);
        RemoveExcessWalls(maze);

        return maze;
    }

    private void MakePass(MazeCell[,] maze)
    {
        int maxDistance = 0;
        MazeCell current = null;
        for(int x = 0; x < maze.GetLength(0); x++)
            for(int y = 0; y < maze.GetLength(1); y++)
                if(maxDistance < maze[x, y]._distanceFromStart)
                {
                    if (x == 0 || x == maze.GetLength(0) - 1 || y == 0 || y == maze.GetLength(1) - 1)
                    {
                        maxDistance = maze[x, y]._distanceFromStart;
                        current = maze[x, y];
                    }
                }

        if (current._x == 0 || current._x == maze.GetLength(0) - 1)
        {
            current.RemoveLeftWall();
        }
        if (current._y == 0 || current._y == maze.GetLength(1) - 1)
        {
            current.RemoveBottomWall();
        }
    }

    private void GenerateMaze(MazeCell[,] maze)
    {
        Stack<MazeCell> mazeStack = new Stack<MazeCell>();

        var currentCell = maze[0, 0];

        do
        {
            List<MazeCell> unvisitableNeighbors = new List<MazeCell>();

            currentCell.WasVisited();
            int x = currentCell._x;
            int y = currentCell._y;

            if(x > 0 && !maze[x - 1, y].IsVisited()) unvisitableNeighbors.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].IsVisited()) unvisitableNeighbors.Add(maze[x, y - 1]);
            if (x < maze.GetLength(0) - 2 && !maze[x + 1, y].IsVisited()) unvisitableNeighbors.Add(maze[x + 1, y]);
            if (y < maze.GetLength(1) - 2 && !maze[x, y + 1].IsVisited()) unvisitableNeighbors.Add(maze[x, y + 1]);

            if(unvisitableNeighbors.Count > 0)
            {
                int randomCount = UnityEngine.Random.Range(0, unvisitableNeighbors.Count);
                var chosenCell = unvisitableNeighbors[randomCount];

                RemoveWall(currentCell, chosenCell);
                mazeStack.Push(chosenCell);
                chosenCell.SetDistance(mazeStack.Count);
                currentCell = chosenCell;
            }
            else
            {
                currentCell = mazeStack.Pop();
            }

        } while (mazeStack.Count > 0);
    }

    private void RemoveWall(MazeCell currentCell, MazeCell chosenCell)
    {
        if(currentCell._x == chosenCell._x)
        {
            if (currentCell._y > chosenCell._y)
                currentCell.RemoveBottomWall();
            else
                chosenCell.RemoveBottomWall();
        }
        else
        {
            if (currentCell._x > chosenCell._x)
                currentCell.RemoveLeftWall();
            else
                chosenCell.RemoveLeftWall();
        }
    }

    private void RemoveExcessWalls(MazeCell[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
            maze[x, maze.GetLength(1) - 1].RemoveLeftWall();
        for (int y = 0; y < maze.GetLength(1); y++)
            maze[maze.GetLength(0) - 1, y].RemoveBottomWall();
    }
}

public class MazeCell
{
    public int _x { get; private set; }
    public int _y { get; private set; }
    private bool _visited;

    public bool _activeLeftWall { get; private set; }
    public bool _activeBottomWall { get; private set; }

    public int _distanceFromStart { get; private set; }

    public MazeCell(int x, int y)
    {
        _x = x;
        _y = y;
        _visited = false;

        _activeLeftWall = true;
        _activeBottomWall = true;
        _distanceFromStart = 0;
    }

    public void SetDistance(int distance)
    {
        _distanceFromStart = distance;
    }

    public bool IsVisited()
    {
        return _visited;
    }

    public void WasVisited()
    {
        _visited = true;
    }

    public void RemoveLeftWall()
    {
        _activeLeftWall = false;
    }

    public void RemoveBottomWall()
    {
        _activeBottomWall = false;
    }
}
