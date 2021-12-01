using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// Class for the hand image
public class DraggableShape : EventTrigger
{
    public static event System.Action<int> correctAnswer = delegate { };
    public static event System.Action<int> wrongAnswer = delegate { };
    public static DraggableShape instance;
    public bool canDrag = false;
    public bool pickedUpShape = false;
    public bool canPickUp;
    public bool canDrop;
    public int pickedUpShapeNumber = -1;

    public bool[] availableDestForShapes;
    public bool levelEnd;
    public Button replayButton;
    public float replayButtonRadius = 0.45f;
    public Button nextLevelButton;
    public float nextLevelButtonRadius = 0.45f;
    public Button audioReplayButton;
    public float audioReplayButtonRadius = 0.24f;



    private bool dragging;
    private RectTransform rectTransform;
    private Vector2 screenMaxValues;
    private Vector2 localPoint;
    private float handXOffset;
    private float handYOffset;
    private float distance;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        screenMaxValues = new Vector2(Screen.width, Screen.height) / 2f;
        handXOffset = rectTransform.sizeDelta.x / 2f;
        handYOffset = rectTransform.sizeDelta.y / 2f;
    }
   
    public void PickUpShape()
    {
        bool canPickUpShape = false;
        
        for (int i = 0; i < Level0Manager.instance.shapeRects.Length; i++)
        {
            if (!Level0Manager.instance.shapeCompleted[i])
            {
                distance = Vector2.Distance(rectTransform.anchoredPosition + new Vector2(0, handYOffset), Level0Manager.instance.shapeRects[i].anchoredPosition);
                if (distance < (Level0Manager.instance.shapeRects[i].sizeDelta.x / 2f) || distance < (Level0Manager.instance.shapeRects[i].sizeDelta.y / 2f))
                {
                    canPickUpShape = true;
                    pickedUpShapeNumber = i;
                    break;
                }
            }
        }
        if (canPickUpShape)
        {
            pickedUpShape = true;
            Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.SetParent(transform);
            Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.SetAsFirstSibling();
            canDrop = true;
            canPickUp = false;
        }
    }

   
    public void DropShape()
    {

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
            pickedUpShapeNumber = -1;
        }
        else
        {
            Level0Manager.instance.shapeRects[pickedUpShapeNumber].transform.SetParent(Level0Manager.instance.shapeParent);
            pickedUpShape = false;
            canDrop = false;
            canPickUp = true;
            wrongAnswer(pickedUpShapeNumber);
            pickedUpShapeNumber = -1;
        }
        StopAllCoroutines();
    }

    public void PickUpOrDrop()
    {
        if (canPickUp)
        {
            PickUpShape();
        }
        else if (canDrop)
        {
            DropShape();
        }
    }

    public void SetReplayAndContinueButtons(Button replay, Button nextLevel)
    {
        replayButton = replay;
        nextLevelButton = nextLevel;
        replayButtonRadius = 0.5f;
        nextLevelButtonRadius = 0.5f;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (canDrag)
        {
            if (canPickUp)
            {
                distance = Vector2.Distance(audioReplayButton.transform.position, transform.position + Vector3.up * 0.8f - Vector3.right * 0.3f);
                if (distance <= audioReplayButtonRadius)
                {
                    audioReplayButton.onClick.Invoke();
                }
                PickUpShape();
            }
            else if(levelEnd)
            {
                float distance = Vector2.Distance(transform.position + Vector3.up * 0.8f - Vector3.right * 0.3f, replayButton.transform.position);
                if (distance < replayButtonRadius)
                {
                    replayButton.onClick.Invoke();
                    return;
                }
                distance = Vector2.Distance(transform.position + Vector3.up * 0.8f - Vector3.right * 0.3f, nextLevelButton.transform.position);
                if (distance < nextLevelButtonRadius)
                {
                    nextLevelButton.onClick.Invoke();
                    return;
                }
            }
            dragging = true;
        }

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (canDrag)
        {
            if (canDrop)
            {
                DropShape();
            }
            dragging = false;
        }
    }
}