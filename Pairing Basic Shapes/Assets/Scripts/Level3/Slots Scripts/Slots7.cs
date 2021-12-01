using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots7 : MonoBehaviour, IDropHandler
{

	public string id;
	
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag8>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag8>().initPos.x, eventData.pointerDrag.GetComponent<Drag8>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag8>().canvas.transform.GetChild(16).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag8>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				if(Level3Manager.instance.numberOfShapesDragged == 12)
				{
					Level3Manager.instance.setNumber++;
					StartCoroutine(WaitForShapes5());
				}
			}
			else
			{
				for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
				{
					for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
					{
						Level3Manager.instance.shapesLevel3[7].shapes[j].GetComponent<Drag8>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag8>().initPos.x, eventData.pointerDrag.GetComponent<Drag8>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.SetActive(true);
				StartCoroutine(WrongAnswer());
			}
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
				Level3Manager.instance.shapesLevel3[7].shapes[j].GetComponent<Drag8>().enabled = true;
				Level3Manager.instance.shapesLevel3[7].shapes[j].GetComponent<Drag8>().canvasGroup.blocksRaycasts = true;
			}
		}
	}

	IEnumerator WaitForShapes5()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
		{
			for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
			{
				Level3Manager.instance.shapesLevel3[8].shapes[j].gameObject.SetActive(true);
				Level3Manager.instance.numberOfShapesDragged = 0;
				Level3Manager.instance.progressImages[7].gameObject.SetActive(true);
				Level3Manager.instance.Slots[28].gameObject.SetActive(false);
				Level3Manager.instance.Slots[29].gameObject.SetActive(false);
				Level3Manager.instance.Slots[30].gameObject.SetActive(false);
				Level3Manager.instance.Slots[31].gameObject.SetActive(false);
				Level3Manager.instance.Slots[32].gameObject.SetActive(true);
				Level3Manager.instance.Slots[33].gameObject.SetActive(true);
				Level3Manager.instance.Slots[34].gameObject.SetActive(true);
				Level3Manager.instance.Slots[35].gameObject.SetActive(true);
			}
		}
		Level3Manager.instance.StartVoiceInstructionCoroutine();
	}

}
