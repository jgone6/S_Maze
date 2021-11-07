using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public PlayerMove player;

    public LoadScene NextScene;

    private void Update()
    {
        GameWin();
    }

    private void GameWin()
    {
        if (player.transform.position.y < -10)
        {
            NextScene._On_GameExitScen();
        }
    }
}
