using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Drag7 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	RectTransform rectTransform;
	public Canvas canvas;
	public CanvasGroup canvasGroup;
	public string nameOfSprites;
	public Vector2 initPos;
	public Canvas canvas2;

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
		if (this.gameObject.transform.position != Level3Manager.instance.Slots[24].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[25].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[26].transform.position || this.gameObject.transform.position != Level3Manager.instance.Slots[27].transform.position)
		{
			AudioManager.instance.Play("WrongAnswer");
			eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<Drag7>().initPos.x, eventData.pointerDrag.GetComponent<Drag7>().initPos.y), 0f);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
	}



}

