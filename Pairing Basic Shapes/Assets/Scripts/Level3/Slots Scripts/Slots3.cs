using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slots3 : MonoBehaviour, IDropHandler
{

	public string id;
	
	

	public void OnDrop(PointerEventData eventData)
	{
		Debug.Log("OnDrop");
		if(eventData.pointerDrag != null)
		{
			if(eventData.pointerDrag.GetComponent<Drag4>().nameOfSprites == id)
			{
				AudioManager.instance.Play("CorrectAnswer", replay: true);
				eventData.pointerDrag.gameObject.SetActive(false);
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag4>().initPos.x, eventData.pointerDrag.GetComponent<Drag4>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.transform.parent = eventData.pointerDrag.GetComponent<Drag4>().canvas.transform.GetChild(12).transform;
				eventData.pointerDrag.gameObject.GetComponent<Drag4>().canvasGroup.blocksRaycasts = true;
				Level3Manager.instance.numberOfShapesDragged++;
				eventData.pointerDrag.GetComponent<Drag4>().isSloted = true;
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
						Level3Manager.instance.shapesLevel3[3].shapes[j].GetComponent<Drag4>().enabled = false;
					}
				}
				AudioManager.instance.Play("WrongAnswer");
				eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag4>().initPos.x, eventData.pointerDrag.GetComponent<Drag4>().initPos.y), 0f);
				eventData.pointerDrag.gameObject.SetActive(true);
				StartCoroutine(WrongAnswer());
			}
			Level3Manager.instance.EnableAndDisable(3);
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
				Level3Manager.instance.shapesLevel3[3].shapes[j].GetComponent<Drag4>().enabled = true;
				Level3Manager.instance.shapesLevel3[3].shapes[j].GetComponent<Drag4>().canvasGroup.blocksRaycasts = true;
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
				Level3Manager.instance.shapesLevel3[4].shapes[j].gameObject.SetActive(true);
				Level3Manager.instance.numberOfShapesDragged = 0;
				Level3Manager.instance.progressImages[3].gameObject.SetActive(true);
				Level3Manager.instance.Slots[12].gameObject.SetActive(false);
				Level3Manager.instance.Slots[13].gameObject.SetActive(false);
				Level3Manager.instance.Slots[14].gameObject.SetActive(false);
				Level3Manager.instance.Slots[15].gameObject.SetActive(false);
				Level3Manager.instance.Slots[16].gameObject.SetActive(true);
				Level3Manager.instance.Slots[17].gameObject.SetActive(true);
				Level3Manager.instance.Slots[18].gameObject.SetActive(true);
				Level3Manager.instance.Slots[19].gameObject.SetActive(true);
				Level3Manager.instance.shapesLevel3[3].shapes[j].GetComponent<Drag4>().isCurrentlyDragged = false;
				Level3Manager.instance.shapesLevel3[3].shapes[j].GetComponent<Drag4>().isSloted = false;
			}
		}
		Level3Manager.instance.StartVoiceInstructionCoroutine();
	}

}
