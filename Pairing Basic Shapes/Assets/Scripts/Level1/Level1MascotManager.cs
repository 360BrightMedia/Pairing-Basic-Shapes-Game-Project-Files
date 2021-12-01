using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level1MascotManager : MonoBehaviour
{
    public RectTransform rect;
    public RectTransform speechDotsRect;
    public RectTransform speechBubble;
    public Animator SpeechAnimator;

    [SerializeField] private string[] AudioClipNames = { "LV1_Mascot1", "LV1_Mascot2" };
    [SerializeField] private Sprite mascotHandsUp;

    private int currentAudioClipIndex = -1;
    private Image mascotImage;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        mascotImage = GetComponent<Image>();
    }

    public float ReplayClip()
    {
        return AudioManager.instance.Play(AudioClipNames[currentAudioClipIndex]);
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
        mascotImage.SetNativeSize();
        rect.anchoredPosition -= Vector2.right * 10f;
    }

    public void MascotDisappear()
    {
        rect.DOAnchorPosY(rect.anchoredPosition.y - 160f, 0.5f);
    }
    public void MascotReappear()
    {
        rect.DOAnchorPosY(rect.anchoredPosition.y + 160f, 0.5f);
    }

    public void SpeedBubbleLeft()
    {
        speechBubble.anchoredPosition = new Vector2(-75f, speechBubble.anchoredPosition.y);
        speechBubble.localEulerAngles += Vector3.forward * 60f;
        speechDotsRect.anchoredPosition = new Vector2(-75f, speechDotsRect.anchoredPosition.y);
    }
    public void SpeedBubbleRight()
    {
        speechBubble.anchoredPosition = new Vector2(75f, speechBubble.anchoredPosition.y);
        speechBubble.localEulerAngles = Vector3.zero;
        speechDotsRect.anchoredPosition = new Vector2(75f, speechDotsRect.anchoredPosition.y);
    }

    public void KillAllTweens()
    {
        mascotImage.DOKill();
        rect.DOKill();
        transform.DOKill();
    }
}
