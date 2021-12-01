using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots1 : MonoBehaviour, IDropHandler
{

	public string id;
	
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag2>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag2>().initPos.x, eventData.pointerDrag.GetComponent<Drag2>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag2>().canvas.transform.GetChild(10).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag2>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				if(Level3Manager.instance.numberOfShapesDragged == 12)
				{
					Level3Manager.instance.setNumber++;
					StartCoroutine(WaitForSecondsShapes());
				}
			}
			else
			{
				for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
				{
					for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
					{
						Level3Manager.instance.shapesLevel3[1].shapes[j].GetComponent<Drag2>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag2>().initPos.x, eventData.pointerDrag.GetComponent<Drag2>().initPos.y), 0f);
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
				Level3Manager.instance.shapesLevel3[1].shapes[j].GetComponent<Drag2>().enabled = true;
				Level3Manager.instance.shapesLevel3[1].shapes[j].GetComponent<Drag2>().canvasGroup.blocksRaycasts = true;
			}
		}
	}

	IEnumerator WaitForSecondsShapes()
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < Level3Manager.instance.shapesLevel3.Count; i++)
		{
			for (int j = 0; j < Level3Manager.instance.shapesLevel3[i].shapes.Length; j++)
			{
				Level3Manager.instance.shapesLevel3[2].shapes[j].gameObject.SetActive(true);
				Level3Manager.instance.numberOfShapesDragged = 0;
				Level3Manager.instance.progressImages[1].gameObject.SetActive(true);
				Level3Manager.instance.Slots[4].gameObject.SetActive(false);
				Level3Manager.instance.Slots[5].gameObject.SetActive(false);
				Level3Manager.instance.Slots[6].gameObject.SetActive(false);
				Level3Manager.instance.Slots[7].gameObject.SetActive(false);
				Level3Manager.instance.Slots[8].gameObject.SetActive(true);
				Level3Manager.instance.Slots[9].gameObject.SetActive(true);
				Level3Manager.instance.Slots[10].gameObject.SetActive(true);
				Level3Manager.instance.Slots[11].gameObject.SetActive(true);
			}
		}
		Level3Manager.instance.StartVoiceInstructionCoroutine();
	}

}
