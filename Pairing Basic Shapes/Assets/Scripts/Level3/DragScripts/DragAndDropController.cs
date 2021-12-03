using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DragAndDropController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	RectTransform rectTransform;
	public Canvas canvas;
	public CanvasGroup canvasGroup;
	public string nameOfSprites;
	public Vector2 initPos;
	public Canvas canvas2;
	float positivePosX = 391f;
	float negativePosX = -391f;
	//static bool pickUpShape = false;
	//static bool canDrag = false;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		nameOfSprites = this.gameObject.GetComponent<Image>().sprite.name;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		//pickUpShape = true;
		//canDrag = true;
		Debug.Log("OnBeginDrag");
		canvasGroup.blocksRaycasts = false;
	    this.gameObject.transform.parent = canvas2.transform;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("OnDrag");
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		if(eventData.pointerDrag.transform.position.x == positivePosX || eventData.pointerDrag.transform.position.x == negativePosX)
		{
			canvasGroup.blocksRaycasts = false;
			eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<DragAndDropController>().initPos.x, eventData.pointerDrag.GetComponent<DragAndDropController>().initPos.y), 0f);
			canvasGroup.blocksRaycasts = true;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		//pickUpShape = false;
		//canDrag = false;
		canvasGroup.blocksRaycasts = true;
		if (this.gameObject.transform.position != Level3Manager.instance.Slots[0].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[1].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[2].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[3].transform.position)
		{
			AudioManager.instance.Play("WrongAnswer");
			eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<DragAndDropController>().initPos.x, eventData.pointerDrag.GetComponent<DragAndDropController>().initPos.y), 0f);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
	}

	

}
