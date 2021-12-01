using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level0MascotManager : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform speechDotsRect;
    public RectTransform speechBubble;
    public Animator SpeechAnimator;

    [SerializeField] private string[] AudioClipNames;
    [SerializeField] private Sprite mascotHandsUp;
    public Sprite mascotNormalSprite;

    private int currentAudioClipIndex = -1;
    public Image mascotImage;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        mascotImage = GetComponent<Image>();
    }

    public float ReplayClip()
    {
        return AudioManager.instance.Play(AudioClipNames[2]);
    }

    public float NextClip()
    {
        return AudioManager.instance.Play(AudioClipNames[++currentAudioClipIndex]);
    }

    public float PlayClip(int index)
    {
        currentAudioClipIndex = index;
        return AudioManager.instance.Play(AudioClipNames[index]);
    }

    public void ChangeMascotImage()
    {
        mascotImage.sprite = mascotHandsUp;
        mascotImage.rectTransform.sizeDelta = new Vector2(190, 300);
        rect.anchoredPosition -= Vector2.right * 10f;
    }

    public void SpeechBubbleLeft()
    {
        speechBubble.anchoredPosition = new Vector2(-75f, speechBubble.anchoredPosition.y);
        speechBubble.localEulerAngles += Vector3.forward*60f ;
        speechDotsRect.anchoredPosition = new Vector2(-75f, speechDotsRect.anchoredPosition.y);
    }
    public void SpeechBubbleRight()
    {
        speechBubble.anchoredPosition = new Vector2(75f, speechBubble.anchoredPosition.y);
        speechBubble.localEulerAngles = Vector3.zero;
        speechDotsRect.anchoredPosition = new Vector2(75f, speechDotsRect.anchoredPosition.y);
    }
    public void MascotDisappear()
    {
        speechBubble.gameObject.SetActive(false);
        speechDotsRect.gameObject.SetActive(false);
        rect.DOAnchorPosY(rect.anchoredPosition.y - 160f, 0.5f).OnComplete(() => gameObject.SetActive(false));
        
    }
    public void MascotReappear()
    {
        rect.DOAnchorPosY(rect.anchoredPosition.y + 160f, 0.5f).OnComplete(()=>gameObject.SetActive(true));
    }

    public void KillAllTweens()
    {
        mascotImage.DOKill();
        rect.DOKill();
        transform.DOKill();
    }
}
