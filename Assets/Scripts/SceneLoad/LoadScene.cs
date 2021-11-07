using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	public void _On_GameStartScene()
	{
		SceneManager.LoadScene("GameStart");
	}

	public void _On_MainScene()
	{
		SceneManager.LoadScene("Main");
	}

	public void _On_GameExitScen()
    {
		SceneManager.LoadScene("GameExit");
	}

	public void _On_ExitGame()
    {
		Application.Quit();
    }
}
