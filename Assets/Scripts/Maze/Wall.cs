using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Vector2Int index;

    public bool isForwardWall = true;
    public bool isBackWall = true;
    public bool isRightWall = true;
    public bool isLeftWall = true;

    public GameObject ForWardWall;
    public GameObject BackWall;
    public GameObject RightWall;
    public GameObject LeftWall;

    private void Start()
    {
        ShowWalls();
    }

    public void ShowWalls()
    {
        ForWardWall.SetActive(isForwardWall);
        BackWall.SetActive(isBackWall);
        RightWall.SetActive(isRightWall);
        LeftWall.SetActive(isLeftWall);
    }

    public bool CheckAllWall()
    {
        return isForwardWall && isBackWall && isRightWall && isRightWall;
    }
}
