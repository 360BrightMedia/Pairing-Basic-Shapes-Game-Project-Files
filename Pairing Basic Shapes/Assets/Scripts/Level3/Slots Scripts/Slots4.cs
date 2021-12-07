using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots4 : MonoBehaviour, IDropHandler
{

	public string id;
	
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag5>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag5>().initPos.x, eventData.pointerDrag.GetComponent<Drag5>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag5>().canvas.transform.GetChild(13).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag5>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				eventData.pointerDrag.GetComponent<Drag5>().isSloted = true;
				if (Level3Manager.instance.numberOfShapesDragged == 12)
				{
					Level3Manager.instance.setNumber++;
					StartCoroutine(WaitForShapes2());
				}
			}
			else
			{
				for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
				{
					for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
					{
						Level3Manager.instance.shapesLevel3[4].shapes[j].GetComponent<Drag5>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag5>().initPos.x, eventData.pointerDrag.GetComponent<Drag5>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.SetActive(true);
				StartCoroutine(WrongAnswer());
			}
			Level3Manager.instance.EnableAndDisable(4);
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
				Level3Manager.instance.shapesLevel3[4].shapes[j].GetComponent<Drag5>().enabled = true;
				Level3Manager.instance.shapesLevel3[4].shapes[j].GetComponent<Drag5>().canvasGroup.blocksRaycasts = true;
			}
		}
	}

	IEnumerator WaitForShapes2()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
		{
			for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
			{
				Level3Manager.instance.shapesLevel3[5].shapes[j].gameObject.SetActive(true);
				Level3Manager.instance.numberOfShapesDragged = 0;
				Level3Manager.instance.progressImages[4].gameObject.SetActive(true);
				Level3Manager.instance.Slots[16].gameObject.SetActive(false);
				Level3Manager.instance.Slots[17].gameObject.SetActive(false);
				Level3Manager.instance.Slots[18].gameObject.SetActive(false);
				Level3Manager.instance.Slots[19].gameObject.SetActive(false);
				Level3Manager.instance.Slots[20].gameObject.SetActive(true);
				Level3Manager.instance.Slots[21].gameObject.SetActive(true);
				Level3Manager.instance.Slots[22].gameObject.SetActive(true);
				Level3Manager.instance.Slots[23].gameObject.SetActive(true);
				Level3Manager.instance.shapesLevel3[4].shapes[j].GetComponent<Drag5>().isCurrentlyDragged = false;
				Level3Manager.instance.shapesLevel3[4].shapes[j].GetComponent<Drag5>().isSloted = false;
			}
		}
		Level3Manager.instance.StartVoiceInstructionCoroutine();
	}

}
