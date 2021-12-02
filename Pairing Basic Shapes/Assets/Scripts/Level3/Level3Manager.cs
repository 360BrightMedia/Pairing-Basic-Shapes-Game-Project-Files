using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Level3Manager : MonoBehaviour
{

    public static Level3Manager instance;

    public int setNumber;
    public Level0MascotManager mascot;
    [SerializeField] Sprite[] circleSprites;
    [SerializeField] Sprite[] rectangleSprites;
    [SerializeField] Sprite[] squareSprites;
    [SerializeField] Sprite[] triangleSprites;
    [SerializeField] Sprite[] progressSprites;
    public Image[] shapes;
    [SerializeField] RectTransform hand;
    [SerializeField] Image handImage;
    public GameObject confetti;
    public GameObject levelCompletedIcons;
    [SerializeField] Button audioReplay;
    [SerializeField] Image progressImage;
    public Image[] progressImages;
    public TextMeshProUGUI rewardsText;
    [SerializeField] GameObject homeButtonIcons;
    [SerializeField] GameObject skipButtonIcons;
    [SerializeField] Button homeButton;
    public Button skipButton;
    [SerializeField] Button hintButton;
    [SerializeField] Button[] boxes;
    [SerializeField] GameObject presentationSkipButtonIcons;
    [SerializeField] GameObject presentationEndIcons;
    WaitForSeconds oneSec = new WaitForSeconds(1f);
    bool onoff = false;
    public Image speechbubble;
    public Image speechDots;
    public GameObject handGameObject;
    private bool mascotOnScreen;
    bool tutorial;
    bool audioMute = false;
    bool canSelectBox = false;
    bool menuButtonsOnScreen = false;
    int progress;
    int correctAnswerStreak;
    Vector2 initMascotPos1;
    [SerializeField] RectTransform[] circleRects;
    [SerializeField] Image audioReplayImage;
    public List<ShapesLevel3> shapesLevel3 = new List<ShapesLevel3>();
    public int numberOfShapesDragged = 0;
    public Image[] Slots;
    public Button backButton;
    public GameObject backButtonGameObject;
    public Button musicButton;
    public Sprite buttonImage;
    public Sprite newButtonImage;
    Coroutine voiceInstructionRepeatCoroutine;
    public GameObject panel;


    private void Awake()
    {
        instance = this;
        presentationEndIcons.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        presentationEndIcons.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(PlayGame);
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
            hintButton.interactable = !value;
            if (!Tutorial && !value)
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

    public bool Tutorial
    {
        get
        {
            return tutorial;
        }
        set
        {
            tutorial = value;
            hintButton.interactable = !value;

        }
    }

    public bool AudioMute
    {
        get
        {
            return audioMute;
        }
        set
        {
            audioMute = value;
            if (audioMute == true)
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

    public bool CanSelectBox
    {
        get
        {
            return canSelectBox;
        }
        set
        {
            if (!Tutorial && value)
            {
                canSelectBox = value;
                audioReplay.interactable = value;
                for (int i = 0; i < boxes.Length; i++)
                {
                    boxes[i].interactable = value;
                }
            }
            else
            {
                canSelectBox = value;
                audioReplay.interactable = value;
                for (int i = 0; i < boxes.Length; i++)
                {
                    boxes[i].interactable = value;
                }
            }
        }
    }

    public int Progress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = value;
            if (progress < progressSprites.Length)
            {
                progressImage.sprite = progressSprites[progress];
            }
            else
            {
                progress = progressSprites.Length - 1;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
		if (Constants.Level3.canLoadSavedScene)
		{
            mascotOnScreen = false;
            mascot.gameObject.SetActive(false);
            backButton.interactable = true;
            hintButton.interactable = true;
            StopAllCoroutines();
            PlayerPrefs.GetInt("SetNumber");
            setNumber = PlayerPrefs.GetInt("SetNumber");
            SetNumberData();
        }
		else
		{
            mascotOnScreen = true;
            setNumber = 0;
            Progress = Constants.Level3.progress;
            rewardsText.text = Constants.rewards.ToString();
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].gameObject.SetActive(false);
            }
            if (Constants.Level3.gameState == Constants.State.NORMAL)
            {
                Constants.Level3.nextLevel = 5;
            }
            if (Constants.Level3.canSkip)
            {
                skipButton.gameObject.SetActive(true);
                skipButton.onClick.RemoveAllListeners();
                skipButton.onClick.AddListener(SkipButtonClicked);
                progressImages[9].gameObject.SetActive(true);
            }
            correctAnswerStreak = 0;
            initMascotPos1 = mascot.rect.anchoredPosition;
            Tutorial = true;
            backButton.interactable = false;
            hintButton.interactable = false;
            StartCoroutine(MascotAppear());
        }
    }

    IEnumerator MascotAppear()
	{
        Debug.Log("Name of Scene: Level3");
        yield return oneSec;
        yield return oneSec;
        mascot.rect.eulerAngles = Vector3.zero;
        yield return oneSec;
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        mascot.SpeechBubbleRight();
        onoff = true;
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(0));
        DisableSpeaking();
        onoff = false;
        yield return oneSec;
        mascot.rect.DOAnchorPos(new Vector2(344f, -100f), 2f);
        mascot.rect.DOScale(1f, 2f);
        yield return oneSec;
        yield return oneSec;
        mascot.SpeechBubbleLeft();
        onoff = true;
        mascot.ChangeMascotImage();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(1));
        mascot.MascotDisappear();
        yield return oneSec;
        for(int i = 0; i < shapes.Length; i++)
		{
            shapes[i].gameObject.SetActive(true);
		}
        yield return new WaitForSeconds(mascot.PlayClip(2));
        handGameObject.SetActive(true);
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(-237.17f, -18.2f), 2f);
        yield return oneSec;
        yield return oneSec;
        circleRects[1].DOAnchorPos(new Vector2(-102f, -188f), 2f);
        hand.DOAnchorPos(new Vector2(-87f, -232f), 2f);
        yield return oneSec;
        yield return oneSec;
        shapes[4].gameObject.SetActive(false);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(-87f, -119f), 2f);
        yield return oneSec;
        yield return oneSec;
        circleRects[2].DOAnchorPos(new Vector2(-102f, -188f), 2f);
        hand.DOAnchorPos(new Vector2(-87f, -232f), 2f);
        yield return oneSec;
        yield return oneSec;
        shapes[9].gameObject.SetActive(false);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(91f, 76f), 2f);
        yield return oneSec;
        yield return oneSec;
        circleRects[0].DOAnchorPos(new Vector2(-102f, -188f), 2f);
        hand.DOAnchorPos(new Vector2(-87f, -232f), 2f);
        yield return oneSec;
        yield return oneSec;
        shapes[2].gameObject.SetActive(false);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(0f, 0f), 2f);
        yield return oneSec;
        yield return oneSec;
        yield return new WaitForSeconds(mascot.PlayClip(3));
        yield return oneSec;
        audioReplayImage.gameObject.SetActive(true);
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(393f, 107f), 2f);
        yield return oneSec;
        AudioManager.instance.Play("ReplayAudio");
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        handGameObject.SetActive(false);
        yield return oneSec;
        for(int i = 0; i < shapes.Length; i++)
		{
            shapes[i].gameObject.SetActive(false);
		}
        presentationEndIcons.SetActive(true);
        presentationEndIcons.transform.GetChild(0).GetComponent<Button>().enabled = false;
        presentationEndIcons.transform.GetChild(1).GetComponent<Button>().enabled = false;
        AudioManager.instance.Play("PresentationCompletionInstructions");
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        AudioManager.instance.Stop("BackgroundMusic");
        skipButton.gameObject.SetActive(false);
        presentationEndIcons.transform.GetChild(0).GetComponent<Button>().enabled = true;
        presentationEndIcons.transform.GetChild(1).GetComponent<Button>().enabled = true;
    }

    public void PresentationPlayAgain()
    {
        musicButton.image.sprite = buttonImage;
        AudioManager.instance.Play("BackgroundMusic");
        presentationEndIcons.SetActive(false);
        audioReplay.gameObject.SetActive(false);
        hand.DOAnchorPos(new Vector2(0f, 0f), 0f);
        mascot.rect.DOAnchorPos(new Vector2(0f, -90f), 0f);
        mascot.mascotImage.sprite = mascot.mascotNormalSprite;
        mascot.gameObject.SetActive(true);
        mascot.SpeechBubbleRight();
        mascot.transform.GetChild(0).gameObject.SetActive(false);
        mascot.transform.GetChild(1).gameObject.SetActive(false);
        shapes[2].rectTransform.DOAnchorPos(new Vector2(71.3f, 126.7f), 0f);
        shapes[4].rectTransform.DOAnchorPos(new Vector2(-257.9f, 28f), 0f);
        shapes[9].rectTransform.DOAnchorPos(new Vector2(-99.9f, -67f), 0f);
        skipButton.gameObject.SetActive(true);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipPresentationButtonClicked);
        StartCoroutine(MascotAppear());
    }

    public void PlayGame()
	{
        musicButton.image.sprite = buttonImage;
        AudioManager.instance.Play("BackgroundMusic");
        presentationEndIcons.SetActive(false);
        backButton.interactable = true;
        hintButton.interactable = true;
        StartCoroutine(Play());
    }

    IEnumerator Play()
	{
        yield return oneSec;
        for (int i = 0; i < shapesLevel3.Count; i++)
        {
            for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
            {
                shapesLevel3[0].shapes[j].gameObject.SetActive(true);
                shapesLevel3[0].shapes[j].gameObject.GetComponent<DragAndDropController>().enabled = false;
            }
        }
        setNumber = 0;
        yield return new WaitForSeconds(mascot.PlayClip(2));
        for (int i = 0; i < shapesLevel3.Count; i++)
        {
            for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
            {
                shapesLevel3[0].shapes[j].gameObject.GetComponent<DragAndDropController>().enabled = true;
            }
        }
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[0].gameObject.SetActive(true);
            Slots[1].gameObject.SetActive(true); Slots[2].gameObject.SetActive(true); Slots[3].gameObject.SetActive(true);
        }
        StartVoiceInstructionCoroutine();
        presentationEndIcons.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        presentationEndIcons.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(PlayWithSavedData);
    }

    public void StartVoiceInstructionCoroutine()
    {
        if (voiceInstructionRepeatCoroutine != null)
        {
            StopCoroutine(voiceInstructionRepeatCoroutine);
        }
        voiceInstructionRepeatCoroutine = StartCoroutine(StartVoiceInstructionAfterTime(15f));
    }

    private IEnumerator StartVoiceInstructionAfterTime(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            CanSelectBox = false;
            yield return new WaitForSeconds(AudioManager.instance.Play("LV1_Mascot5"));
            CanSelectBox = true;
        }
    }

    public void DisableSpeaking()
    {
        if (mascotOnScreen)
        {
            mascot.speechDotsRect.gameObject.SetActive(false);
            mascot.speechBubble.gameObject.SetActive(false);
        }
    }

    public void EnableSpeaking()
    {
        if (mascotOnScreen)
        {
            mascot.speechBubble.gameObject.SetActive(true);
            mascot.speechDotsRect.gameObject.SetActive(true);
        }
    }

    public void HomeButtonClicked()
    {
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        CanSelectBox = false;
        homeButtonIcons.SetActive(true);
        levelCompletedIcons.SetActive(false);
        presentationEndIcons.SetActive(false);
        presentationSkipButtonIcons.SetActive(false);
        skipButtonIcons.SetActive(false);
        backButton.interactable = false;
        StartCoroutine(HomeButtons());
    }

    IEnumerator HomeButtons()
	{
        panel.SetActive(true);
        yield return new WaitForSeconds(AudioManager.instance.PlayMenuAudio("HomeButtonAudio"));
        panel.SetActive(false);
    }

    public void HomeContinueClicked()
    { 
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        AudioManager.instance.Stop("HomeButtonAudio");
        AudioManager.instance.ContinueAudio();
        CanSelectBox = true;
        homeButtonIcons.SetActive(false);
        backButtonGameObject.SetActive(false);
        hintButton.interactable = false;
        backButton.interactable = true;
    }

    public void SkipPresentationButtonClicked()
    {
        Time.timeScale = 0f;
        AudioManager.instance.PauseAudio();
        AudioManager.instance.Play("PresentationSkip");
        skipButton.gameObject.SetActive(false);
        presentationSkipButtonIcons.SetActive(true);
    }

    public void PresentationSkipContinueClicked()
    {
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        AudioManager.instance.Stop("PresentationSkip");
        AudioManager.instance.ContinueAudio();
        skipButton.gameObject.SetActive(true);
        presentationSkipButtonIcons.SetActive(false);
    }

    public void PresentationSkipExitClicked()
    {
        StopAllCoroutines();
        KillAllTweens();
        AudioManager.instance.StopAllAudioExceptBackground();
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        presentationSkipButtonIcons.SetActive(false);
        StartCoroutine(PresentationSkipAnimation());
    }

    IEnumerator PresentationSkipAnimation()
    {
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipButtonClicked);
        if (Constants.Level4.canSkip)
        {
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            skipButton.gameObject.SetActive(false);
        }
        for(int i = 0; i < shapes.Length; i++)
		{
            shapes[i].gameObject.SetActive(false);
		}
        handImage.DOFade(0f, 0.5f).OnComplete(() => hand.gameObject.SetActive(false));
        mascot.MascotDisappear();
        mascotOnScreen = false;
        Tutorial = false;
        yield return oneSec;
        audioReplay.gameObject.SetActive(true);
        audioReplay.enabled = true;
        CanSelectBox = false;
        for(int i = 0; i < Slots.Length; i++)
		{
            Slots[i].gameObject.SetActive(false);
            Slots[0].gameObject.SetActive(true);
            Slots[1].gameObject.SetActive(true);
            Slots[2].gameObject.SetActive(true);
            Slots[3].gameObject.SetActive(true);
        }
        numberOfShapesDragged = 0;
        PlayerPrefs.GetInt("SetNumber");
        SetNumberData();
    }

    public void SkipButtonClicked()
    {
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        AudioManager.instance.PlayMenuAudio("SkipAudio");

        CanSelectBox = false;
        skipButton.gameObject.SetActive(false);
        skipButtonIcons.SetActive(true);
    }

    public void SkipContinueClicked()
    {
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        AudioManager.instance.Stop("SkipAudio");
        AudioManager.instance.ContinueAudio();
        CanSelectBox = true;
        skipButton.gameObject.SetActive(true);
        skipButtonIcons.SetActive(false);
    }

    public void SkipExitClicked()
	{
        AudioManager.instance.Stop("SkipAudio");
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        Constants.Level4.canLoadSavedScene = true;
	}

    public void ReplayAudioInstructions()
    {
        StartCoroutine(AudioInstructionsRepeatCoroutine());
    }


    private IEnumerator AudioInstructionsRepeatCoroutine()
    {
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        CanSelectBox = false;
        yield return new WaitForSeconds(mascot.PlayClip(2));
        CanSelectBox = true;
        AudioManager.instance.IncreaseBackgroundMusicVolume();
    }

    private void KillAllTweens()
    {
        for(int i = 0; i < shapes.Length; i++)
		{
            shapes[i].DOKill();
        }
        hand.DOKill();
        handImage.DOKill();
        hand.transform.DOKill();
        mascot.KillAllTweens();
    }

    public void NextLevel()
    {
        StopAllCoroutines();
        KillAllTweens();
        Time.timeScale = 1f;
        SceneLoader.instance.LoadNextLevel(Constants.Level3.nextLevel);
        AudioManager.instance.Play("BackgroundMusic");
    }

    public void ReplayLevel()
    {
        levelCompletedIcons.SetActive(false);
        musicButton.image.sprite = buttonImage;
        confetti.SetActive(false);
        AudioManager.instance.Stop("BackgroundMusic");
        AudioManager.instance.Play("BackgroundMusic");
        audioReplay.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(true);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipButtonClicked);
        mascot.rect.DOAnchorPos(new Vector2(0f, -90f), 0f);
        mascot.mascotImage.sprite = mascot.mascotNormalSprite;
        mascot.gameObject.SetActive(true);
        hand.DOAnchorPos(new Vector2(0f, 0f), 0f);
        shapes[2].rectTransform.DOAnchorPos(new Vector2(71.3f, 126.7f), 0f);
        shapes[4].rectTransform.DOAnchorPos(new Vector2(-257.9f, 28f), 0f);
        shapes[9].rectTransform.DOAnchorPos(new Vector2(-99.9f, -67f), 0f);
        StartCoroutine(MascotAppear());
    }

    public void ToggleBackgroundMusic()
    {
        AudioMute = !AudioMute;
    }

    // Update is called once per frame
    void Update()
    {
        if (onoff)
        {
            speechbubble.gameObject.SetActive(true);
            speechDots.gameObject.SetActive(true);
        }
        else
        {
            speechbubble.gameObject.SetActive(false);
            speechDots.gameObject.SetActive(false);
        }
    }

    public void BackButtonClicked()
    {
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        CanSelectBox = false;
        AudioManager.instance.PlayMenuAudio("HomeButtonAudio");
        backButtonGameObject.SetActive(true);
    }

    public void PreviousLevelClicked()
    {
        Time.timeScale = 0f;
        Constants.Level2.canSkip = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("SetNumber", setNumber);
        PlayerPrefs.GetInt("LevelBar");
        PlayerPrefs.GetString("Score");
        if (setNumber != 10)
        {
            Constants.Level2.canSkip = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            Time.timeScale = 1f;
        }
    }

    public void StartPresentation()
    {
        PlayerPrefs.SetInt("SetNumber", setNumber);
        hintButton.interactable = false;
        musicButton.image.sprite = buttonImage;
        AudioManager.instance.Stop("BackgroundMusic");
        AudioManager.instance.Play("BackgroundMusic");
        for(int i = 0; i < shapesLevel3.Count; i++)
		{
            for(int j = 0; j < shapesLevel3[i].shapes.Length; j++)
			{
                shapesLevel3[i].shapes[j].gameObject.SetActive(false);
			}
		}
        audioReplay.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(true);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipPresentationButtonClicked);
        mascot.rect.DOAnchorPos(new Vector2(0f, -90f), 0f);
        mascot.mascotImage.sprite = mascot.mascotNormalSprite;
        mascot.gameObject.SetActive(true);
        mascot.SpeechBubbleRight();
        hand.DOAnchorPos(new Vector2(0f, 0f), 0f);
        shapes[2].rectTransform.DOAnchorPos(new Vector2(71.3f, 126.7f), 0f);
        shapes[4].rectTransform.DOAnchorPos(new Vector2(-257.9f, 28f), 0f);
        shapes[9].rectTransform.DOAnchorPos(new Vector2(-99.9f, -67f), 0f);
        StartCoroutine(MascotAppear());
        presentationEndIcons.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        presentationEndIcons.gameObject.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(PlayWithSavedData);
    }

    public void GoToHomeScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void PlayWithSavedData()
	{
        AudioManager.instance.Play("BackgroundMusic");
        hintButton.interactable = true;
        skipButton.gameObject.SetActive(false);
        presentationEndIcons.SetActive(false);
        PlayerPrefs.GetInt("SetNumber");
        SetNumberData();
    }

    public void SetNumberData()
	{
        if (PlayerPrefs.GetInt("SetNumber") == 0)
        {
            audioReplay.interactable = true;
            AudioManager.instance.Play("LV3_Mascot3");
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[0].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[0].shapes[j].gameObject.GetComponent<DragAndDropController>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[0].gameObject.SetActive(true);
                Slots[1].gameObject.SetActive(true); Slots[2].gameObject.SetActive(true); Slots[3].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 1)
        {
            progressImages[0].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[1].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[1].shapes[j].gameObject.GetComponent<Drag2>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[4].gameObject.SetActive(true);
                Slots[5].gameObject.SetActive(true); Slots[6].gameObject.SetActive(true); Slots[7].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 2)
        {
            progressImages[1].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[2].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[2].shapes[j].gameObject.GetComponent<Drag3>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[8].gameObject.SetActive(true);
                Slots[9].gameObject.SetActive(true); Slots[10].gameObject.SetActive(true); Slots[11].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 3)
        {
            progressImages[2].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[3].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[3].shapes[j].gameObject.GetComponent<Drag4>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[12].gameObject.SetActive(true);
                Slots[13].gameObject.SetActive(true); Slots[14].gameObject.SetActive(true); Slots[15].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 4)
        {
            progressImages[3].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[4].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[4].shapes[j].gameObject.GetComponent<Drag5>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[16].gameObject.SetActive(true);
                Slots[17].gameObject.SetActive(true); Slots[18].gameObject.SetActive(true); Slots[19].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 5)
        {
            progressImages[4].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[5].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[5].shapes[j].gameObject.GetComponent<Drag6>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[20].gameObject.SetActive(true);
                Slots[21].gameObject.SetActive(true); Slots[22].gameObject.SetActive(true); Slots[23].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 6)
        {
            progressImages[5].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[6].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[6].shapes[j].gameObject.GetComponent<Drag7>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[24].gameObject.SetActive(true);
                Slots[25].gameObject.SetActive(true); Slots[26].gameObject.SetActive(true); Slots[27].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 7)
        {
            progressImages[6].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[7].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[7].shapes[j].gameObject.GetComponent<Drag8>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[28].gameObject.SetActive(true);
                Slots[29].gameObject.SetActive(true); Slots[30].gameObject.SetActive(true); Slots[31].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 8)
        {
            progressImages[7].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[8].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[8].shapes[j].gameObject.GetComponent<Drag9>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[32].gameObject.SetActive(true);
                Slots[33].gameObject.SetActive(true); Slots[34].gameObject.SetActive(true); Slots[35].gameObject.SetActive(true);
            }
        }
        if (PlayerPrefs.GetInt("SetNumber") == 9)
        {
            progressImages[8].gameObject.SetActive(true);
            audioReplay.interactable = true;
            for (int i = 0; i < shapesLevel3.Count; i++)
            {
                for (int j = 0; j < shapesLevel3[i].shapes.Length; j++)
                {
                    shapesLevel3[9].shapes[j].gameObject.SetActive(true);
                    shapesLevel3[9].shapes[j].gameObject.GetComponent<Drag10>().enabled = true;
                }
            }
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[36].gameObject.SetActive(true);
                Slots[37].gameObject.SetActive(true); Slots[38].gameObject.SetActive(true); Slots[39].gameObject.SetActive(true);
            }
        }
    }
}
