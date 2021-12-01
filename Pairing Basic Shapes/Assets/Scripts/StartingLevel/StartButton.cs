using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    
    public void StartGame()
	{
		SceneManager.LoadScene(1);
		Time.timeScale = 1f;
		AudioManager.instance.Play("BackgroundMusic");
	}

	public void Level1()
	{
		SceneManager.LoadScene(2);
		Time.timeScale = 1f;
		AudioManager.instance.Play("BackgroundMusic");
	}

	public void Level2()
	{
		SceneManager.LoadScene(3);
		Time.timeScale = 1f;
		AudioManager.instance.Play("BackgroundMusic");
	}

	public void Level3()
	{
		SceneManager.LoadScene(4);
		Time.timeScale = 1f;
		AudioManager.instance.Play("BackgroundMusic");
	}

	public void Level4()
	{
		SceneManager.LoadScene(5);
		Time.timeScale = 1f;
		AudioManager.instance.Play("BackgroundMusic");
	}

}
