using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Drag5 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	RectTransform rectTransform;
	public Canvas canvas;
	public CanvasGroup canvasGroup;
	public string nameOfSprites;
	public Vector2 initPos;
	public Canvas canvas2;
	public bool isSloted = false;
	public bool isCurrentlyDragged = false;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		nameOfSprites = this.gameObject.GetComponent<Image>().sprite.name;

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("OnBeginDrag");
		canvasGroup.blocksRaycasts = false;
		this.gameObject.transform.parent = canvas2.transform;
		isCurrentlyDragged = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("OnDrag");
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDrag");
		canvasGroup.blocksRaycasts = true;
		if (this.gameObject.transform.position != Level3Manager.instance.Slots[16].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[17].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[18].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[19].transform.position)
		{
			AudioManager.instance.Play("WrongAnswer");
			eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag5>().initPos.x, eventData.pointerDrag.GetComponent<Drag5>().initPos.y), 0f);
			Level3Manager.instance.EnableAndDisable(4);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (isCurrentlyDragged)
		{
			canvasGroup.blocksRaycasts = false;
		}
		else
		{
			canvasGroup.blocksRaycasts = true;
		}
		Debug.Log("Click");
		Level3Manager.instance.GameObjectReferences(this.gameObject, 4);
	}



}

