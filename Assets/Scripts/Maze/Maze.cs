using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int width = 5;
    public int height = 5;

    private Wall wallPrefab;
    private Wall[,] WallMap;

    private List<Wall> wallHistory;

    private void Awake()
    {
        string path = "Walls";
        wallPrefab = Resources.Load<GameObject>(path).GetComponent<Wall>();
    }

    void Start()
    {
        BatchWalls();
        MakeMaze(WallMap[0, 0]);
        WallMap[width - 1, height - 1].isRightWall = false;
    }

    void BatchWalls()
    {
        WallMap = new Wall[width, height];
        wallHistory = new List<Wall>();

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                Wall copy_wall = Instantiate<Wall>(wallPrefab, this.transform);

                copy_wall.index = new Vector2Int(x, y);

                copy_wall.name = "wall" + x + "_" + y;
                copy_wall.transform.localPosition = new Vector3((x - 12) * 2, 0, (y - 12) * 2);
                copy_wall.gameObject.SetActive(true);

                WallMap[x, y] = copy_wall;
            }
        }
    }

    private void MakeMaze(Wall startWall)
    {
        Wall[] neighbors = GetNeighborWalls(startWall);
        if (neighbors.Length > 0)
        {
            Wall nextWall = neighbors[Random.Range(0, neighbors.Length)];
            ConnectWalls(startWall, nextWall);
            wallHistory.Add(nextWall);
            MakeMaze(nextWall);
        }

        else
        {
            if (wallHistory.Count > 0)
            {
                Wall lastWall = wallHistory[wallHistory.Count - 1];

                wallHistory.Remove(lastWall);
                MakeMaze(lastWall);
            }
        }
    }

    private Wall[] GetNeighborWalls(Wall wall)
    {
        List<Wall> retwallList = new List<Wall>();
        Vector2Int index = wall.index;
        //forward
        if (index.y + 1 < height)
        {
            Wall neighbor = WallMap[index.x, index.y + 1];

            if (neighbor.CheckAllWall())
            {
                retwallList.Add(neighbor);
            }
        }
        //back
        if (index.y - 1 >= 0)
        {
            Wall neighbor = WallMap[index.x, index.y - 1];

            if (neighbor.CheckAllWall())
            {
                retwallList.Add(neighbor);
            }
        }

        //left
        if (index.x - 1 >= 0)
        {
            Wall neighbor = WallMap[index.x - 1, index.y];

            if (neighbor.CheckAllWall())
            {
                retwallList.Add(neighbor);
            }
        }

        //right
        if (index.x + 1 < width)
        {
            Wall neighbor = WallMap[index.x + 1, index.y];

            if (neighbor.CheckAllWall())
            {
                retwallList.Add(neighbor);
            }
        }

        return retwallList.ToArray();
    }

    private void ConnectWalls(Wall _w0, Wall _w1)
    {
        Vector2Int dir = _w0.index - _w1.index;
        //forward
        if (dir.y <= -1)
        {
            _w0.isForwardWall = false;
            _w1.isBackWall = false;
        }

        //back
        else if (dir.y >= 1)
        {
            _w0.isBackWall = false;
            _w1.isForwardWall = false;
        }

        //left
        else if (dir.x >= 1)
        {
            _w0.isLeftWall = false;
            _w1.isRightWall = false;
        }

        //right
        else if (dir.x <= -1)
        {
            _w0.isRightWall = false;
            _w1.isLeftWall = false;
        }

        _w0.ShowWalls();
        _w1.ShowWalls();
    }
}
