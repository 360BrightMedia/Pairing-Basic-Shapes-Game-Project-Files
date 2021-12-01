using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Level0Manager : MonoBehaviour
{
    #region references and fields

    public static Level0Manager instance;


    public RectTransform[] shapeHoleRects; // the holes where the shapes will go
    public RectTransform[] shapeRects; // the movable shapes
    public Transform shapeParent;
    public bool[] shapeCompleted = new bool[2];

    [SerializeField] private int currentShape = 0;
    [SerializeField] private Level0MascotManager mascot;
    [SerializeField] private Sprite[] shapeHoleSprites;
    [SerializeField] private ShapeDraggingManager[] shapeDraggers;
    [SerializeField] private Sprite[] shapeSprites;
    [SerializeField] private Image[] shapeHoleHolders;
    [SerializeField] private Image[] shapeHolders;
    [SerializeField] private Button skipButton;
    [SerializeField] private RectTransform hand;
    [SerializeField] private Image board;
    [SerializeField] private Image handImage;
    [SerializeField] private GameObject confetti;
    [SerializeField] private GameObject levelCompletedIcons;
    [SerializeField] private Button replay;
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button audioReplay;
    [SerializeField] private GameObject homeButtonIcons;
    [SerializeField] private GameObject skipButtonIcons;
    [SerializeField] private Button homeButton;

    private bool mascotOnScreen;
    private bool tutorialMode;
    private WaitForSeconds oneSec = new WaitForSeconds(1f);
    private List<Vector2> shapesInitPos = new List<Vector2>();
    private int correctAnswerStreak;
    private bool audioMute = false;
    private bool menuButtonsOnScreen = false;
    public Sprite buttonImage;
    public Sprite newButtonImage;
    public Button musicButton;

    #endregion

    //----------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
        DraggableShape.correctAnswer += CorrectAnswer;
        DraggableShape.wrongAnswer += WrongAnswer;
        ShapeDraggingManager.correctAnswer += CorrectAnswer;
        ShapeDraggingManager.wrongAnswer += WrongAnswer;
    }

    void Start()
    {

        // setup variables before level start
        currentShape = Constants.Level0.currentShape;
        if (Constants.Level0.gameState == Constants.State.NORMAL)
        {
            Constants.Level0.nextLevel = 2;
        }
        shapeDraggers[0].SetShapeNumber(0);
        shapeDraggers[1].SetShapeNumber(1);
        mascotOnScreen = true;
        tutorialMode = true;
        correctAnswerStreak = 0;
        if(Constants.Level0.canSkip)
        {
            skipButton.gameObject.SetActive(true);
        }

        // starting the level
        StartCoroutine(MascotAppear());
        for (int i = 0; i < shapeRects.Length; i++)
        {
            shapesInitPos.Add(shapeRects[i].anchoredPosition);
        }
    }

    public bool MenuButtonsOnScreen
    {
        get
        {
            return menuButtonsOnScreen;
        }
        set
        {
            menuButtonsOnScreen = value;
            homeButton.interactable = !value;
            if (!tutorialMode && !value)
            {
                audioReplay.interactable = !value;
            }
            else
            {
                audioReplay.interactable = !value;
            }
            skipButton.interactable = !value;
        }
    }

    public bool AudioMute
    {

        // Stop background music
        get
        {
            return audioMute;
        }
        set
        {
            audioMute = value;
            if(audioMute == true)
            {
                ChangeButtonImage();
                AudioManager.instance.Stop("BackgroundMusic");
            }
            else
            {
                musicButton.image.sprite = buttonImage;
                AudioManager.instance.Play("BackgroundMusic");
            }
        }
    }

    void ChangeButtonImage()
	{
        musicButton.image.sprite = newButtonImage;
	}

    private IEnumerator MascotAppear()
    {
        Debug.Log("(StartOfGam1");
        // mascot peep
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(1f);
        Debug.Log("(StartOfGam0");
        // mascot starts speaking
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.NextClip());
        DisableSpeaking();
        yield return oneSec;
        yield return oneSec;
        Debug.Log("(StartOfGam2");
        // mascot moves to the right
        mascot.rect.DOAnchorPos(new Vector3(344f, 150f), 2f);
        mascot.rect.DOScale(1f, 2f);
        mascot.SpeechBubbleLeft();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.NextClip());
        DisableSpeaking();
        yield return oneSec;
        yield return oneSec;
        board.gameObject.SetActive(true);
        Debug.Log("(StartOfGam3");
        // hand image appears as part of tutorial
        handImage.DOFade(1f, 2f).From(0f).SetEase(Ease.InQuad);
        hand.gameObject.SetActive(true);

        // generate the movable shapes and also the respective holes
        NextShapes();
        shapeParent.gameObject.transform.GetChild(0).gameObject.GetComponent<ShapeDraggingManager>().enabled = false;
        shapeParent.gameObject.transform.GetChild(1).gameObject.GetComponent<ShapeDraggingManager>().enabled = false;
        yield return oneSec;
        yield return oneSec;

        // hand picks up shape and drops to correct hole
        hand.DOAnchorPos(shapeRects[0].anchoredPosition - new Vector2(0f, (hand.sizeDelta.y / 2f)), 3f).SetEase(Ease.Linear);
        EnableSpeaking();
        mascot.ChangeMascotImage();
        yield return new WaitForSeconds(mascot.PlayClip(2));
        DisableSpeaking();
        mascot.MascotDisappear();
        mascotOnScreen = false;
        yield return oneSec;
        DraggableShape.instance.canPickUp = true;
        DraggableShape.instance.PickUpOrDrop();
        yield return oneSec;
        hand.DOAnchorPos(shapeHoleRects[0].anchoredPosition + new Vector2(0f, (hand.sizeDelta.y / 2f)), 2f).SetEase(Ease.Linear).OnComplete(() => DraggableShape.instance.PickUpOrDrop());
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(4));
        DisableSpeaking();
        Debug.Log("(StartOfGam4");
        // hand picks up the 2nd shape and drops it in hole
        hand.DOAnchorPos(shapeRects[1].anchoredPosition - new Vector2(0f, (hand.sizeDelta.y / 2f)), 2f).SetEase(Ease.Linear);
        yield return oneSec;
        yield return oneSec;
        DraggableShape.instance.canPickUp = true;
        DraggableShape.instance.PickUpOrDrop();
        yield return oneSec;
        hand.DOAnchorPos(shapeHoleRects[1].anchoredPosition + new Vector2(0f, (hand.sizeDelta.y / 2f)), 2f).SetEase(Ease.Linear).OnComplete(() => DraggableShape.instance.PickUpOrDrop());
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;

        // audio replay tutorial
        audioReplay.gameObject.SetActive(true);
        audioReplay.enabled = false;
        hand.transform.DOMove(audioReplay.transform.position - Vector3.up * 0.8f + Vector3.right * 0.3f, 1f).SetEase(Ease.Linear);
        EnableSpeaking();
        yield return new WaitForSeconds(AudioManager.instance.Play("ReplayAudio"));
        DisableSpeaking();
        hand.transform.DOMove(Vector3.zero, 1f);
        yield return oneSec;
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(5));

        // remove hand
        handImage.DOFade(0f,0.5f).OnComplete(()=>hand.gameObject.SetActive(false));
        DisableSpeaking();

        // presentation over
        tutorialMode = false;
        DraggableShape.instance.enabled = false;
        audioReplay.enabled = true;
        Constants.Level0.firstPresentation = false;

        // level 0 real part starts
        NextShapes();
        shapeParent.gameObject.transform.GetChild(0).gameObject.GetComponent<ShapeDraggingManager>().enabled = true;
        shapeParent.gameObject.transform.GetChild(1).gameObject.GetComponent<ShapeDraggingManager>().enabled = true;
        yield return new WaitForSeconds(mascot.PlayClip(2));
        Debug.Log("(StartOfGam5");
    }

    public void NextShapes()
    {
        StartCoroutine(NextShapesCoroutine());
    }

    private IEnumerator NextShapesCoroutine()
    {
        board.DOFade(1f, 2f).From(0f).SetEase(Ease.Linear);
        correctAnswerStreak = 0;
        for (int i = 0; i < shapeHoleHolders.Length; i++)
        {
            shapeCompleted[i] = false;
            shapeHoleHolders[i].sprite = shapeHoleSprites[currentShape];
            shapeHoleHolders[i].preserveAspect = true;
            shapeHoleHolders[i].SetNativeSize();
            shapeHoleHolders[i].DOFade(1f, 2f).From(0f).SetEase(Ease.InQuad);
            shapeHolders[i].sprite = shapeSprites[currentShape];
            shapeHolders[i].preserveAspect = true;
            shapeHolders[i].SetNativeSize();
            shapeHolders[i].DOFade(1f, 2f).From(0f).SetEase(Ease.InQuad);
            shapeHolders[i].gameObject.SetActive(true);
        }
        if (!tutorialMode)
        {
            shapeDraggers[0].UpdateShapeLimits();
            shapeDraggers[1].UpdateShapeLimits();
            
            hand.DOAnchorPos(Vector2.zero, 1f);
            yield return oneSec;
            shapeDraggers[0].shapeFinished = false;
            shapeDraggers[1].shapeFinished = false;
            ShapeDraggingManager.canDrag = true;
        }

        AudioManager.instance.IncreaseBackgroundMusicVolume();
    }

    private void DisableSpeaking()
    {
        if (mascotOnScreen)
        {
            mascot.SpeechAnimator.SetTrigger("Speaking");
            mascot.speechDotsRect.gameObject.SetActive(false);
            mascot.speechBubble.gameObject.SetActive(false);
            //mascot.SpeechAnimator.StartPlayback();
        }
    }

    private void EnableSpeaking()
    {
        if (mascotOnScreen)
        {
            mascot.speechBubble.gameObject.SetActive(true);
            mascot.speechDotsRect.gameObject.SetActive(true);
            mascot.SpeechAnimator.ResetTrigger("Speaking");
        }
    }

    private void CorrectAnswer(int shapeNumber)
    {
        ShapeDraggingManager.pickedUpShape = false;
        shapeDraggers[shapeNumber].shapeFinished = true;
        ShapeDraggingManager.canDrag = false;
        shapeCompleted[shapeNumber] = true;
        StartCoroutine(CorrectAnswerAnimation());
    }

    private IEnumerator CorrectAnswerAnimation()
    {
        
        correctAnswerStreak++;
        if (correctAnswerStreak == 2)
        {
            ShapeDraggingManager.canDrag = false;
            board.DOFade(0f, 1f);
            shapeHolders[0].DOFade(0f, 1f);
            shapeHolders[1].DOFade(0f, 1f);
            shapeHoleHolders[0].DOFade(0f, 1f);
            shapeHoleHolders[1].DOFade(0f, 1f);
            currentShape++;
        }

        
        yield return new WaitForSeconds(AudioManager.instance.Play("CorrectAnswer"));
        if (correctAnswerStreak == 2)
        {
            yield return oneSec;
            if (currentShape == 4)
            {
                // level completed
                Constants.Level0.canSkip = true;
                Constants.Level0.firstTimeLevel = false;
                confetti.SetActive(true);
                yield return new WaitForSeconds(AudioManager.instance.Play("LevelComplete") + 1f);
                EnableSpeaking();
                levelCompletedIcons.SetActive(true);
                yield return new WaitForSeconds(AudioManager.instance.Play("PresentationCompletionInstructions") + 1f);
                DisableSpeaking();
                AudioManager.instance.Stop("BackgroundMusic");
            }
            else
            {
                // generate next shapes
                shapeRects[0].anchoredPosition = shapesInitPos[0];
                shapeRects[1].anchoredPosition = shapesInitPos[1];
                AudioManager.instance.DecreaseBackgroundMusicVolume();
                if (!tutorialMode)
                {
                    NextShapes();
                }
            }
        }
        else
        {
            Debug.Log("CanDrag");
            ShapeDraggingManager.canDrag = true;
        }
    }

    public void ToggleBackgroundMusic()
    {
        AudioMute = !AudioMute;
    }

    public void SkipButtonClicked()
    {
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        AudioManager.instance.PlayMenuAudio("PresentationSkip");
        ShapeDraggingManager.canDrag = false;
        skipButton.gameObject.SetActive(false);
        skipButtonIcons.SetActive(true);
    }

    public void SkipContinueClicked()
    {
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        AudioManager.instance.Stop("PresentationSkip");
        AudioManager.instance.ContinueAudio();
        ShapeDraggingManager.canDrag = true;
        skipButton.gameObject.SetActive(true);
        skipButtonIcons.SetActive(false);
    }

    public void HomeButtonClicked()
    {
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        ShapeDraggingManager.canDrag = false;
        AudioManager.instance.PlayMenuAudio("HomeButtonAudio");
        homeButtonIcons.SetActive(true);
        levelCompletedIcons.SetActive(false);
        skipButtonIcons.SetActive(false);
    }

    public void HomeContinueClicked()
    {
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        AudioManager.instance.Stop("HomeButtonAudio");
        AudioManager.instance.ContinueAudio();
        ShapeDraggingManager.canDrag = true;
        homeButtonIcons.SetActive(false);
    }

    public void ReplayAudioInstructions()
    {
        StartCoroutine(AudioInstructionsRepeatCoroutine());
    }

    private IEnumerator AudioInstructionsRepeatCoroutine()
    {
        audioReplay.interactable = false;
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        ShapeDraggingManager.canDrag = false;
        yield return new WaitForSeconds(mascot.ReplayClip());
        ShapeDraggingManager.canDrag = true;
        AudioManager.instance.IncreaseBackgroundMusicVolume();
        audioReplay.interactable = true;
    }

    private void WrongAnswer(int shapeNumber)
    {
        shapeRects[shapeNumber].anchoredPosition = shapesInitPos[shapeNumber];
        StartCoroutine(WrongAnswerAnimation());
    }
    private IEnumerator WrongAnswerAnimation()
    {
        ShapeDraggingManager.canDrag = false;
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(2));
        DisableSpeaking();
        ShapeDraggingManager.canDrag = true;
    }

    public void NextLevel()
    {
        StopAllCoroutines();
        KillAllTweens();
        Time.timeScale = 1f;
        SceneLoader.instance.LoadNextLevel(Constants.Level0.nextLevel);
        AudioManager.instance.Play("BackgroundMusic");
    }

    private void KillAllTweens()
    {
        for(int i = 0; i < shapeRects.Length;i++)
        {
            shapeRects[i].DOKill();
            shapeHolders[i].DOKill();
            shapeRects[i].transform.DOKill();
            shapeHoleHolders[i].DOKill();
        }
        hand.DOKill();
        handImage.DOKill();
        hand.transform.DOKill();
        board.DOKill();
        mascot.KillAllTweens();
    }

    public void ReplayLevel()
    {
        StopAllCoroutines();
        KillAllTweens();
        Time.timeScale = 1f;
        SceneLoader.instance.ReplayLevel();
        AudioManager.instance.Play("BackgroundMusic");


































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































    }

    private void OnDestroy()
    {
        DraggableShape.correctAnswer -= CorrectAnswer;
        DraggableShape.wrongAnswer -= WrongAnswer;
        ShapeDraggingManager.correctAnswer -= CorrectAnswer;
        ShapeDraggingManager.wrongAnswer -= WrongAnswer;
    }

}
