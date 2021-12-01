using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader instance;
	
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		
    }

    public void LoadNextLevel(int level)
	{
		AudioManager.instance.StopAllAudioExceptBackground();
		if (this == null)
        {
			return;
        }
		
		AsyncOperation sceneload = SceneManager.LoadSceneAsync(level);
		AudioManager.instance.SetBackgroundMusicVolume(0.5f);
	}

	public void ReplayLevel()
	{
		AudioManager.instance.StopAllAudioExceptBackground();
		AsyncOperation sceneload = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		AudioManager.instance.SetBackgroundMusicVolume(0.5f);
	}
	
	private void LoadPrevLevel()
	{
		AudioManager.instance.StopAllAudioExceptBackground();
		AsyncOperation sceneload = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
		AudioManager.instance.SetBackgroundMusicVolume(0.5f);
	}
}
