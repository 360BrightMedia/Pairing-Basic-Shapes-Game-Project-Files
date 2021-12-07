using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots9 : MonoBehaviour, IDropHandler
{

	public string id;
	int lastProgressBarId = 1;
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag10>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag10>().initPos.x, eventData.pointerDrag.GetComponent<Drag10>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag10>().canvas.transform.GetChild(18).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag10>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				eventData.pointerDrag.GetComponent<Drag10>().isSloted = true;
				Debug.Log(Level3Manager.instance.numberOfShapesDragged);
				if (Level3Manager.instance.numberOfShapesDragged == 12)
				{
					if (lastProgressBarId == 1)
					{
						Level3Manager.instance.progressImages[9].gameObject.SetActive(true);
					}
					Level3Manager.instance.confetti.SetActive(true);
					Debug.Log("Reward value is" + Level3Manager.instance.rewardsText.text);
					Constants.rewards = int.Parse(Level3Manager.instance.rewardsText.text) + 10;
					Debug.Log("Constants are:" + Constants.rewards.ToString());
					Level3Manager.instance.rewardsText.text = Constants.rewards.ToString();
					PlayerPrefs.SetString("Score", Level3Manager.instance.rewardsText.text);
					PlayerPrefs.SetInt("LevelBar", lastProgressBarId);
					AudioManager.instance.Play("LevelComplete");
					Level3Manager.instance.EnableSpeaking();
					Level3Manager.instance.levelCompletedIcons.SetActive(true);
					AudioManager.instance.Play("NextLevelInstructions");
					Level3Manager.instance.DisableSpeaking();
					Level3Manager.instance.CanSelectBox = false;
				}
			}
			else
			{
				for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
				{
					for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
					{
						Level3Manager.instance.shapesLevel3[9].shapes[j].GetComponent<Drag10>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag10>().initPos.x, eventData.pointerDrag.GetComponent<Drag10>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.SetActive(true);
				StartCoroutine(WrongAnswer());
			}
			Level3Manager.instance.EnableAndDisable(9);
			eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
		}
	}

	IEnumerator WrongAnswer()
	{
		yield return new WaitForSeconds(1f);
		yield return new WaitForSeconds(Level3Manager.instance.mascot.PlayClip(2));
		for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
		{
			for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
			{
				Level3Manager.instance.shapesLevel3[9].shapes[j].GetComponent<Drag10>().enabled = true;
				Level3Manager.instance.shapesLevel3[9].shapes[j].GetComponent<Drag10>().canvasGroup.blocksRaycasts = true;
			}
		}
	}

}
