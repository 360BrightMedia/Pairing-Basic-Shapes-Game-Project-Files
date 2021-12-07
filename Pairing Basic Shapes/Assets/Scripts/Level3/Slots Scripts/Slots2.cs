using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots2 : MonoBehaviour, IDropHandler
{

	public string id;
	
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag3>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag3>().initPos.x, eventData.pointerDrag.GetComponent<Drag3>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag3>().canvas.transform.GetChild(11).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag3>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				eventData.pointerDrag.GetComponent<Drag3>().isSloted = true;
				if (Level3Manager.instance.numberOfShapesDragged == 12)
				{
					Level3Manager.instance.setNumber++;
					StartCoroutine(WaitForShapes());
				}
			}
			else
			{
				for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
				{
					for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
					{
						Level3Manager.instance.shapesLevel3[2].shapes[j].GetComponent<Drag3>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag3>().initPos.x, eventData.pointerDrag.GetComponent<Drag3>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.SetActive(true);
				StartCoroutine(WrongAnswer());
			}
			Level3Manager.instance.EnableAndDisable(2);
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
				Level3Manager.instance.shapesLevel3[2].shapes[j].GetComponent<Drag3>().enabled = true;
				Level3Manager.instance.shapesLevel3[2].shapes[j].GetComponent<Drag3>().canvasGroup.blocksRaycasts = true;
			}
		}
	}

	IEnumerator WaitForShapes()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
		{
			for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
			{
				Level3Manager.instance.shapesLevel3[3].shapes[j].gameObject.SetActive(true);
				Level3Manager.instance.numberOfShapesDragged = 0;
				Level3Manager.instance.progressImages[2].gameObject.SetActive(true);
				Level3Manager.instance.Slots[8].gameObject.SetActive(false);
				Level3Manager.instance.Slots[9].gameObject.SetActive(false);
				Level3Manager.instance.Slots[10].gameObject.SetActive(false);
				Level3Manager.instance.Slots[11].gameObject.SetActive(false);
				Level3Manager.instance.Slots[12].gameObject.SetActive(true);
				Level3Manager.instance.Slots[13].gameObject.SetActive(true);
				Level3Manager.instance.Slots[14].gameObject.SetActive(true);
				Level3Manager.instance.Slots[15].gameObject.SetActive(true);
				Level3Manager.instance.shapesLevel3[2].shapes[j].GetComponent<Drag3>().isCurrentlyDragged = false;
				Level3Manager.instance.shapesLevel3[2].shapes[j].GetComponent<Drag3>().isSloted = false;
			}
		}
		Level3Manager.instance.StartVoiceInstructionCoroutine();
	}

}
