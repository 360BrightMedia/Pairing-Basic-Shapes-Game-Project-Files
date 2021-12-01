using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EmptySlots : MonoBehaviour, IDropHandler
{

	public string id ;
	WaitForSeconds oneSec = new WaitForSeconds(1f);

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		id = this.gameObject.GetComponent<IDs>().id;
			
		if (eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<DraggableObjects>().nameOfSprite == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.GetComponent<RectTransform>().transform.parent = eventData.pointerDrag.GetComponent<DraggableObjects>().canvas.transform.GetChild(1).transform;
				eventData.pointerDrag.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
				eventData.pointerDrag.gameObject.GetComponent<DraggableObjects>().canvasGroup.blocksRaycasts = true;
				Level4Manager.instance.shapesCount++;
				eventData.pointerDrag.gameObject.GetComponent<DraggableObjects>().enabled = false;
				if (Level4Manager.instance.shapesCount == 4)
				{
					Level4Manager.instance.setNumber++;
					Level4Manager.instance.currentShapes++;
					if(Level4Manager.instance.setNumber == 1)
					{
						StartCoroutine(Set1());
					}
					if (Level4Manager.instance.setNumber == 2)
					{
						StartCoroutine(Set2());
					}
					if (Level4Manager.instance.setNumber == 3)
					{
						StartCoroutine(Set3());
					}
					if (Level4Manager.instance.setNumber == 4)
					{
						StartCoroutine(Set4());
					}
					if (Level4Manager.instance.setNumber == 5)
					{
						StartCoroutine(Set5());
					}
					if (Level4Manager.instance.setNumber == 6)
					{
						StartCoroutine(Set6());
					}
					if (Level4Manager.instance.setNumber == 7)
					{
						StartCoroutine(Set7());
					}
					if (Level4Manager.instance.setNumber == 8)
					{
						StartCoroutine(Set8());
					}
					if (Level4Manager.instance.setNumber == 9)
					{
						StartCoroutine(Set9());
					}
					if (Level4Manager.instance.setNumber == 10)
					{
						StartCoroutine(Set10());
					}
					if (Level4Manager.instance.setNumber == 11)
					{
						StartCoroutine(Set11());
					}
					if (Level4Manager.instance.setNumber == 12)
					{
						StartCoroutine(Set12());
					}
				}
			}
			else
			{
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<DraggableObjects>().initPos.x, eventData.pointerDrag.GetComponent<DraggableObjects>().initPos.y), 0f);
				StartCoroutine(Wrong());
			}
			eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
		}
		
	}

	public IEnumerator Wrong()
	{
		yield return oneSec;
		yield return oneSec;
		AudioManager.instance.Play("TryAgain");
		yield return oneSec;
		yield return oneSec;
		yield return new WaitForSeconds(Level4Manager.instance.mascot.PlayClip(2));
		
	}

	IEnumerator Set1()
	{
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[0].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[0].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[0].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[0].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if (Level4Manager.instance.currentShapes == 1)
		{
			Level4Manager.instance.upperImages[1].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(3).gameObject.name;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set2()
	{
		Level4Manager.instance.barImages[0].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[1].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 2)
		{
			Level4Manager.instance.upperImages[2].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set3()
	{
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[2].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 3)
		{
			Level4Manager.instance.upperImages[3].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set4()
	{
		Level4Manager.instance.barImages[1].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[3].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if (Level4Manager.instance.currentShapes == 4)
		{
			Level4Manager.instance.upperImages[4].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set5()
	{
		Level4Manager.instance.barImages[2].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[4].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 5)
		{
			Level4Manager.instance.upperImages[5].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set6()
	{
		Level4Manager.instance.barImages[3].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[5].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 6)
		{
			Level4Manager.instance.upperImages[6].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set7()
	{
		Level4Manager.instance.barImages[4].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[6].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 7)
		{
			Level4Manager.instance.upperImages[7].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set8()
	{
		Level4Manager.instance.barImages[5].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[7].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 8)
		{
			Level4Manager.instance.upperImages[8].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set9()
	{
		Level4Manager.instance.barImages[6].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[8].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 9)
		{
			Level4Manager.instance.upperImages[9].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}
	IEnumerator Set10()
	{
		Level4Manager.instance.barImages[7].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[9].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 10)
		{
			Level4Manager.instance.upperImages[10].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set11()
	{
		Level4Manager.instance.barImages[8].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[10].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].rectTransform.DOAnchorPos(new Vector2(-279f, -157f), 0f);
		Level4Manager.instance.shapes[1].rectTransform.DOAnchorPos(new Vector2(-94f, -157f), 0f);
		Level4Manager.instance.shapes[2].rectTransform.DOAnchorPos(new Vector2(93f, -157f), 0f);
		Level4Manager.instance.shapes[3].rectTransform.DOAnchorPos(new Vector2(270f, -157f), 0f);
		Level4Manager.instance.shapes[0].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		if(Level4Manager.instance.currentShapes == 11)
		{
			Level4Manager.instance.upperImages[11].upperImages.gameObject.SetActive(true);
			Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(0).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(1).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(2).gameObject.SetActive(true);
			Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(3).gameObject.SetActive(true);
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(0).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(1).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(2).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.InteractaleObjectsParent.transform.GetChild(3).GetComponent<DraggableObjects>().enabled = true;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).GetComponent<IDs>().id = Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(0).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).GetComponent<IDs>().id = Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(1).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).GetComponent<IDs>().id = Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(2).gameObject.name;
			Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).GetComponent<IDs>().id = Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(3).gameObject.name;
			PlayerPrefs.SetInt("SetNumber", Level4Manager.instance.setNumber);
			PlayerPrefs.SetInt("CurrentSet", Level4Manager.instance.currentShapes);
		}
		Level4Manager.instance.StartVoiceInstructionCoroutine();
	}

	IEnumerator Set12()
	{
		Level4Manager.instance.barImages[9].gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		Level4Manager.instance.shapesCount = 0;
		Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(0).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(1).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(2).gameObject.SetActive(false);
		Level4Manager.instance.upperImages[11].upperImages.transform.GetChild(3).gameObject.SetActive(false);
		Level4Manager.instance.shapes[0].gameObject.SetActive(false);
		Level4Manager.instance.shapes[1].gameObject.SetActive(false);
		Level4Manager.instance.shapes[2].gameObject.SetActive(false);
		Level4Manager.instance.shapes[3].gameObject.SetActive(false);
		Level4Manager.instance.NonInteractableBoxParent.SetActive(false);
		Level4Manager.instance.confetti.SetActive(true);
		Constants.rewards = int.Parse(Level4Manager.instance.rewardsText.text) + 10;
		Level4Manager.instance.rewardsText.text = Constants.rewards.ToString();
		AudioManager.instance.Play("LevelComplete");
		Level4Manager.instance.EnableSpeaking();
		Level4Manager.instance.levelCompletedIcons.SetActive(true);
		AudioManager.instance.Play("EndOfTheGameMusic");
		Level4Manager.instance.DisableSpeaking();
		Level4Manager.instance.CanSelectBox = false;
	}

}

