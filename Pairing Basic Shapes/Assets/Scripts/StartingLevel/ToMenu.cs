using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ToMenu : MonoBehaviour
{
	Level0MascotManager mascot;
	int currentSceneIndex;

	public void LoadMainMenu()
	{
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
		Time.timeScale = 0f;
		AudioManager.instance.Stop("BackgroundMusic");
		SceneManager.LoadScene(0);
	}

}
