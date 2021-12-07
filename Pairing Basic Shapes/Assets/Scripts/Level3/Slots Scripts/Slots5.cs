using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots5 : MonoBehaviour, IDropHandler
{

	public string id;
	
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag6>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag6>().initPos.x, eventData.pointerDrag.GetComponent<Drag6>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag6>().canvas.transform.GetChild(14).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag6>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				eventData.pointerDrag.GetComponent<Drag6>().isSloted = true;
				if (Level3Manager.instance.numberOfShapesDragged == 12)
				{
					Level3Manager.instance.setNumber++;
					StartCoroutine(WaitForShapes3());
				}
			}
			else
			{
				for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
				{
					for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
					{
						Level3Manager.instance.shapesLevel3[5].shapes[j].GetComponent<Drag6>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag6>().initPos.x, eventData.pointerDrag.GetComponent<Drag6>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.SetActive(true);
				StartCoroutine(WrongAnswer());
			}
			Level3Manager.instance.EnableAndDisable(5);
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
				Level3Manager.instance.shapesLevel3[5].shapes[j].GetComponent<Drag6>().enabled = true;
				Level3Manager.instance.shapesLevel3[5].shapes[j].GetComponent<Drag6>().canvasGroup.blocksRaycasts = true;
			}
		}
	}

	IEnumerator WaitForShapes3()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
		{
			for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
			{
				Level3Manager.instance.shapesLevel3[6].shapes[j].gameObject.SetActive(true);
				Level3Manager.instance.numberOfShapesDragged = 0;
				Level3Manager.instance.progressImages[5].gameObject.SetActive(true);
				Level3Manager.instance.Slots[20].gameObject.SetActive(false);
				Level3Manager.instance.Slots[21].gameObject.SetActive(false);
				Level3Manager.instance.Slots[22].gameObject.SetActive(false);
				Level3Manager.instance.Slots[23].gameObject.SetActive(false);
				Level3Manager.instance.Slots[24].gameObject.SetActive(true);
				Level3Manager.instance.Slots[25].gameObject.SetActive(true);
				Level3Manager.instance.Slots[26].gameObject.SetActive(true);
				Level3Manager.instance.Slots[27].gameObject.SetActive(true);
				Level3Manager.instance.shapesLevel3[5].shapes[j].GetComponent<Drag6>().isCurrentlyDragged = false;
				Level3Manager.instance.shapesLevel3[5].shapes[j].GetComponent<Drag6>().isSloted = false;
			}
		}
		Level3Manager.instance.StartVoiceInstructionCoroutine();
	}

}
