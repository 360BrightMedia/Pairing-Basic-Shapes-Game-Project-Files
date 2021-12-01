using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{

    public int sceneToContinue;

    public void ContinueGame()
	{
		sceneToContinue = PlayerPrefs.GetInt("SavedScene");
		if (sceneToContinue != 0)
		{
			SceneManager.LoadScene(sceneToContinue);
			if (sceneToContinue == 2)
			{
				Constants.Level1.canLoadSavedScene = true;
			}
			if(sceneToContinue == 3)
			{
				Constants.Level2.canLoadSavedScene = true;
			}
			if(sceneToContinue == 4)
			{
				Constants.Level3.canLoadSavedScene = true;
			}
			if(sceneToContinue == 5)
			{
				Constants.Level4.canLoadSavedScene = true;
			}
            AudioManager.instance.Play("BackgroundMusic");
			Time.timeScale = 1f;
		}
		else
			return;
	}
}
