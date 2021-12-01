
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class HandManagerLV1 : EventTrigger
{
    public static event System.Action<int> selectedShape = delegate { };
    public static HandManagerLV1 instance;
    public bool canDrag = false;
    

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
    private List<Vector3> shapePos = new List<Vector3>();
    private Vector2 shapeDistance;
    private float distance;

    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        screenMaxValues = new Vector2(Screen.width, Screen.height) / 2f;
        handXOffset = rectTransform.sizeDelta.x / 2f;
        handYOffset = rectTransform.sizeDelta.y / 2f;
        levelEnd = false;
    }
    public void Update()
    {
       
        if (dragging && canDrag)
        {
            localPoint = (Vector2)Input.mousePosition - screenMaxValues;
            rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localPoint.x, -screenMaxValues.x + handXOffset, screenMaxValues.x - handXOffset), Mathf.Clamp(localPoint.y, -screenMaxValues.y + handYOffset, screenMaxValues.y - handYOffset - 55f - 12f));
        }
    }

    public void SetShapePositions(List<Vector3> pos, Vector2 dist)
    {
        shapePos = new List<Vector3>(pos);
        shapeDistance = dist;
    }
    public void SetAudioReplayButton(Button audioReplay)
    {
        audioReplayButton = audioReplay;
        audioReplayButtonRadius = 0.26f;
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
            distance = Vector2.Distance(audioReplayButton.transform.position, transform.position + Vector3.up * 0.8f - Vector3.right * 0.3f);
            if (distance <= audioReplayButtonRadius)
            {
                audioReplayButton.onClick.Invoke();
            }
            if (levelEnd)
            {
                distance = Vector2.Distance(transform.position + Vector3.up * 0.8f - Vector3.right * 0.3f, replayButton.transform.position);
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

    

    public void CheckIfShapeSelected()
    {
        float distanceX, distanceY;
        for(int i = 0; i < shapePos.Count; i++)
        {
            distanceX = Mathf.Abs(shapePos[i].x - (transform.position.x - 0.3f));
            distanceY = Mathf.Abs(shapePos[i].y - (transform.position.y + 0.8f));
            if(distanceX <= shapeDistance.x && distanceY <= shapeDistance.y)
            {
                StopAllCoroutines();
                selectedShape(i);
                return;
            }
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (canDrag)
        {
            dragging = false;
            if (!levelEnd)
            {
                CheckIfShapeSelected();
            }
        }
    }
}
