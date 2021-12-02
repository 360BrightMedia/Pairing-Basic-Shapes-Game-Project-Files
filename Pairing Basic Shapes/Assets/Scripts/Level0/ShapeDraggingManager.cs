using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShapeDraggingManager : EventTrigger
{
    public static event System.Action<int> correctAnswer = delegate { };
    public static event System.Action<int> wrongAnswer = delegate { };
    public static bool canDrag = false;
    public static bool pickedUpShape = false;
    public bool canPickUp;
    public bool canDrop;
    public int pickedUpShapeNumber = -1;
    private bool dragging;
    private RectTransform rectTransform;
    private Vector2 screenMaxValues;
    private Vector2 localPoint;
    private float shapeXLimit;
    private float shapeYLimit;
    private float distance;
    Vector2 offset;
    public bool shapeFinished = false;
    public int touches;


    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        screenMaxValues = new Vector2(Screen.width, Screen.height) / 2f;
        shapeXLimit = rectTransform.sizeDelta.x / 2f;
        shapeYLimit = rectTransform.sizeDelta.y / 2f;

    }

    public void UpdateShapeLimits()
    {
        shapeXLimit = rectTransform.sizeDelta.x / 2f;
        shapeYLimit = rectTransform.sizeDelta.y / 2f;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (canDrag && !pickedUpShape && !shapeFinished)
        {
            touches++;
            if (touches != 1) return;
            canDrop = true;
            pickedUpShape = true;
            dragging = true;
            StartCoroutine(StartVoiceInstructionAfterTime(15f));
            offset = GetMousePos() - (Vector2)transform.position;
        }
    }
    public void Update()
    {
        Debug.Log("Drag Check "+dragging+" / "+ canDrag);
        if (dragging && canDrag)
        {
            if (touches != 1) return;
            var mousePosition = GetMousePos();
            transform.position = mousePosition - offset;
        }  
    }

    private IEnumerator StartVoiceInstructionAfterTime(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            canDrag = false;
            yield return new WaitForSeconds(AudioManager.instance.Play("LV0_Mascot3"));
            Debug.Log("Candrag");
              canDrag = true;
        }
    }
    public void StartVoiceInstructionCoroutine()
    {
        StartCoroutine(StartVoiceInstructionAfterTime(15f));
    }

    public void SetShapeNumber(int shape)
    {
        pickedUpShapeNumber = shape;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (canDrag)
        {
            if (canDrop)
            {
                touches--;
                DropShape();
            }
            dragging = false;
            canDrag = true;
        }
    }

    public void DropShape()
    {
        StopAllCoroutines();
        bool canDropShape = false;
        for (int i = 0; i < Level0Manager.instance.shapeRects.Length; i++)
        {
            distance = Vector2.Distance(Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.position, Level0Manager.instance.shapeHoleRects[pickedUpShapeNumber].transform.position);
            if (distance < 1f)
            {
                canDropShape = true;
                break;
            }
        }
        if (canDropShape)
        {
            pickedUpShape = false;
            Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.SetParent(Level0Manager.instance.shapeParent);
            Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.position = Level0Manager.instance.shapeHoleRects[pickedUpShapeNumber].transform.position;
            canDrop = false;
            canPickUp = true;
            correctAnswer(pickedUpShapeNumber);
        }
        else
        {
            Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.SetParent(Level0Manager.instance.shapeParent);
            pickedUpShape = false;
            canDrop = false;
            canPickUp = true;
            wrongAnswer(pickedUpShapeNumber);
        }
        
    }

    Vector2 GetMousePos()
	{
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

}
