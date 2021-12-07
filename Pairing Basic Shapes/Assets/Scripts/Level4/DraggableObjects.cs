using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DraggableObjects : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	RectTransform rectTransform;
	public Canvas canvas;
	public CanvasGroup canvasGroup;
	public string nameOfSprite;
	public Vector2 initPos;
	public Canvas canvas2;
	public bool isSloted = false;
	public bool isCurrentlyDragged = false;

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
		if( this.gameObject.transform.position != Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(4).transform.position || this.gameObject.transform.position != Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(5).transform.position || this.gameObject.transform.position != Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(6).transform.position || this.gameObject.transform.position != Level4Manager.instance.NonInteractableBoxParent.transform.GetChild(7).transform.position)
		{
			AudioManager.instance.Play("WrongAnswer");
			eventData.pointerDrag.GetComponent<RectTransform>().DOAnchorPos(new Vector2(eventData.pointerDrag.GetComponent<DraggableObjects>().initPos.x, eventData.pointerDrag.GetComponent<DraggableObjects>().initPos.y), 0f);
			Level4Manager.instance.EnableAndDisable();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if(isCurrentlyDragged)
		{
			canvasGroup.blocksRaycasts = false;
		}
		else
		{
			canvasGroup.blocksRaycasts = true;
		}
		Debug.Log("Click");
		Level4Manager.instance.GameObjectReferences(this.gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		nameOfSprite = this.gameObject.name;
		gameObject.transform.parent = canvas.transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
