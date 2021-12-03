using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Level1Manager : MonoBehaviour
{
    #region references and fields

    public static Level1Manager instance;

    [SerializeField] private int setNumber;
    [SerializeField] private int currentShape = 0;
    public Level0MascotManager mascot;
    [SerializeField] private Sprite[] shapeSprites;
    [SerializeField] private Sprite[] shapeHolderSprites;
    public Image[] shapeHolders;
    public Image[] shapes;
    [SerializeField] private RectTransform hand;
    public Transform boxParent;
    public GameObject boxParentGameObject;
    [SerializeField] private Image handImage;
    [SerializeField] private GameObject confetti;
    [SerializeField] private GameObject levelCompletedIcons;
    public Button audioReplay;
    [SerializeField] private Image progressImage;
    [SerializeField] private Sprite[] progressSprites;
    [SerializeField] private TextMeshProUGUI rewardsText;
    [SerializeField] private GameObject homeButtonIcons;
    [SerializeField] private GameObject skipButtonIcons;
    [SerializeField] private Button homeButton;
    public Button skipButton;
    [SerializeField] private Button[] boxes;
    [SerializeField] private Button hintButton;
    [SerializeField] private GameObject presentationSkipButtonIcons;
    [SerializeField] private GameObject presentationEndIcons;


    private WaitForSeconds oneSec = new WaitForSeconds(1f);
    private Vector2 initMascotPos1;
    public bool mascotOnScreen;
    private int correctAnswerStreak;
    private bool firstAttempt;
    private int progress;
    private int correctShapeHolderIndex;
    private bool audioMute = false;
    private bool canSelectBox = false;
    private bool tutorial;
    private Coroutine voiceInstructionRepeatCoroutine;
    public Coroutine presentationCoroutine;
    private Coroutine nextShapeCoroutine;
    private bool menuButtonsOnScreen = false;
    public Sprite buttonImage;
    public Sprite newButtonImage;
    public Button musicButton;
    public Button backButton;
    public GameObject backButtonIcons;
    public Image[] level1Progress;
    public int NumberOfWrongAnswer = 0;
    public Image[] Arrows;
    public int correctShape = 0;

    #endregion

    //----------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
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

    

    void Start()
    {
        if (Constants.Level1.canLoadSavedScene)
        {
            mascotOnScreen = false;
            mascot.gameObject.SetActive(false);
            backButton.interactable = true;
            hintButton.interactable = true;
            StopAllCoroutines();
            Time.timeScale = 1f;
            PlayerPrefs.GetInt("SetNumber_Level1");
            setNumber = PlayerPrefs.GetInt("SetNumber_Level1");
            SetNumberData();
        }
		else
		{
            mascotOnScreen = true;
            setNumber = Constants.Level1.currentSet;
            Progress = 0;
            rewardsText.text = Constants.rewards.ToString();
            if (HandManagerLV1.instance != null)
            {
                HandManagerLV1.instance.enabled = false;
            }
            if (Constants.Level1.gameState == Constants.State.NORMAL)
            {
                Constants.Level1.nextLevel = 3;
            }
            if (Constants.Level1.canSkip)
            {
                skipButton.gameObject.SetActive(true);
                skipButton.onClick.RemoveAllListeners();
                skipButton.onClick.AddListener(SkipButtonClicked);
                level1Progress[0].gameObject.SetActive(true);
            }
			if (Constants.Level1.isGameCompleted)
			{
                skipButton.gameObject.SetActive(true);
                skipButton.onClick.RemoveAllListeners();
                skipButton.onClick.AddListener(SkipButtonClicked);
                for(int i = 0; i < level1Progress.Length; i++)
				{
                    level1Progress[i].gameObject.SetActive(true);
				}
            }
            CanSelectBox = false;
            correctAnswerStreak = 0;
            initMascotPos1 = mascot.rect.anchoredPosition;
            Tutorial = true;
            StartCoroutine(MascotAppear());
        }
    }

    private IEnumerator MascotAppear()
    {
        yield return oneSec;
        yield return oneSec;
        mascot.rect.eulerAngles = Vector3.zero;
        yield return oneSec;
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(0));
        DisableSpeaking();
        yield return oneSec;
        mascot.rect.DOAnchorPos(new Vector3(344f, -90f), 2f);
        mascot.rect.DOScale(1f, 2f);

        yield return oneSec;
        yield return oneSec;
        mascot.SpeechBubbleLeft();
        mascot.ChangeMascotImage();

        // Presentation only if the player plays the level 1st time
        if (Constants.Level1.firstTimeLevel)
        {
            StartPresentation(true);
        }
        else
        {
            // Start the game directly
            mascot.MascotDisappear();
            mascotOnScreen = false;
            Tutorial = false;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].DOFade(1f, 1f).From(0f).SetEase(Ease.Linear);
            }
            audioReplay.gameObject.SetActive(true);
            audioReplay.enabled = true;
            CanSelectBox = false;
            NextShapes();
        }
        /*board.gameObject.SetActive(true);

        handImage.DOFade(1f, 2f).From(0f).SetEase(Ease.InQuad);
        hand.gameObject.SetActive(true);
        NextShapes();*/
    }

    public void StartPresentation(bool firstTime)
    {
        PlayerPrefs.SetInt("SetNumber", setNumber);
        if (presentationCoroutine != null)
        {
            StopCoroutine(presentationCoroutine);
        }
        if (firstTime)
        {
            // Presentation start on level start
            presentationCoroutine = StartCoroutine(Presentation(firstTime));
        }
        else
        {
            // Presentation start on clicking hint button
            AudioManager.instance.StopAllAudioExceptBackground();
            audioReplay.enabled = false;
            audioReplay.gameObject.SetActive(false);
            CanSelectBox = false;
            hand.anchoredPosition = Vector2.zero;
            boxParent.gameObject.SetActive(false);
            if (voiceInstructionRepeatCoroutine != null)
            {
                StopCoroutine(voiceInstructionRepeatCoroutine);
            }
            if (!mascotOnScreen)
            {
                mascot.MascotReappear();
                mascotOnScreen = true;
            }
            Tutorial = true;

            // skip button default listener is level skip, so changing it to presentation skip
            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(PresentationSkipButtonClicked);
            skipButton.gameObject.SetActive(true);
            shapes[0].DOFade(0f, 1f);
            shapes[1].DOFade(0f, 1f);
            shapes[2].DOFade(0f, 1f);
            shapes[3].DOFade(0f, 1f).OnComplete(() => presentationCoroutine = StartCoroutine(Presentation(firstTime)));
        }
    }

    public void PresentationSkipButtonClicked()
    {
        // skip button clicked while presentation is ongoing
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        AudioManager.instance.PlayMenuAudio("PresentationSkip");
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
        // Presentation will be skipped now
        if (presentationCoroutine != null)
        {
            StopCoroutine(presentationCoroutine);
        }
        AudioManager.instance.StopAllAudioExceptBackground();
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        presentationSkipButtonIcons.SetActive(false);
        StartCoroutine(PresentationSkipAnimation());
    }

    private IEnumerator PresentationSkipAnimation()
    {
        // changing skip button listener to level skip
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipButtonClicked);


        if (Constants.Level1.canSkip)
        {
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            skipButton.gameObject.SetActive(false);

        }
        shapes[0].DOFade(0f, 1f);
        shapes[1].DOFade(0f, 1f);
        shapes[2].DOFade(0f, 1f);
        shapes[3].DOFade(0f, 1f);
        handImage.DOFade(0f, 0.5f).OnComplete(() => hand.gameObject.SetActive(false));
        yield return oneSec;

        mascot.MascotDisappear();
        mascotOnScreen = false;
        Tutorial = false;
        boxParent.gameObject.SetActive(true);
        shapeHolders[0].DOFade(1f, 0f);
        shapeHolders[1].DOFade(1f, 0f);
        shapeHolders[2].DOFade(1f, 0f);
        shapeHolders[3].DOFade(1f, 0f);
        audioReplay.gameObject.SetActive(true);
        audioReplay.enabled = true;
        CanSelectBox = false;
        PlayerPrefs.GetInt("SetNumber_Level1");
        // Continue game
        NextShapes();
    }

    private IEnumerator Presentation(bool firstTime)
    {
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(1));
        DisableSpeaking();

        // presentation start, shapes and hand appears
        boxParent.gameObject.SetActive(true);
        for (int i = 0; i < shapeHolders.Length; i++)
        {
            shapeHolders[i].DOFade(1f, 1f).From(0f).SetEase(Ease.Linear);
        }
        handImage.DOFade(1f, 1f).From(0f).SetEase(Ease.Linear);
        hand.gameObject.SetActive(true);
        yield return oneSec;
        NextShapes();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(2));
        DisableSpeaking();

        hand.transform.DOMove(shapeHolders[correctShapeHolderIndex].transform.position - Vector3.up * 0.8f + Vector3.right * 0.3f, 1f).SetEase(Ease.Linear);
        yield return oneSec;
        CheckAnswer(correctShapeHolderIndex);
        yield return oneSec;

        hand.transform.DOMove(Vector3.zero, 1f).SetEase(Ease.Linear);
        yield return oneSec;
        shapeHolders[correctShapeHolderIndex].sprite = shapeHolderSprites[0];
        audioReplay.enabled = false;
        audioReplay.gameObject.SetActive(true);
        hand.transform.DOMove(audioReplay.transform.position - Vector3.up * 0.8f + Vector3.right * 0.3f, 1f).SetEase(Ease.Linear);
        EnableSpeaking();
        yield return new WaitForSeconds(AudioManager.instance.Play("ReplayAudio"));
        DisableSpeaking();
        handImage.DOFade(0f, 0.5f).OnComplete(() => hand.gameObject.SetActive(false));
        yield return oneSec;
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(3));
        DisableSpeaking();
        mascot.MascotDisappear();

        // presentation end
        mascotOnScreen = false;

        if (!Constants.Level1.firstPresentation)
        {
            presentationEndIcons.SetActive(true);
            skipButton.gameObject.SetActive(false);
            MenuButtonsOnScreen = true;
            AudioManager.instance.PlayMenuAudio("PresentationCompletionInstructions");
        }
        else
        {
            Constants.Level1.firstPresentation = false;
            Tutorial = false;
            NextShapes();
            audioReplay.enabled = true;
        }
    }

    public void PresentationPlayAgain()
    {
        MenuButtonsOnScreen = false;
        presentationEndIcons.SetActive(false);
        StartPresentation(false);
    }

    public void PresentationPlayGame()
    {
        // play game pressed at end of presentation
        if (presentationCoroutine != null)
        {
            StopCoroutine(presentationCoroutine);
        }
        AudioManager.instance.StopAllAudioExceptBackground();
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipButtonClicked);
        if (Constants.Level1.canSkip)
        {
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            skipButton.gameObject.SetActive(false);

        }
        Tutorial = false;
        MenuButtonsOnScreen = false;
        presentationEndIcons.SetActive(false);
        CanSelectBox = false;
        NextShapes();
    }

    public void CheckAnswer(int shapeIndex)
    {
        if (!Tutorial)
        {
            AudioManager.instance.DecreaseBackgroundMusicVolume();
            CanSelectBox = false;
        }
        if (voiceInstructionRepeatCoroutine != null)
        {
            StopCoroutine(voiceInstructionRepeatCoroutine);
        }
        if (shapeIndex == correctShapeHolderIndex)
        {
            CorrectAnswer(shapeIndex);
        }
        else
        {
            WrongAnswer(shapeIndex);
        }
    }

    public void NextShapes()
    {
        if(nextShapeCoroutine != null)
        {
            StopCoroutine(nextShapeCoroutine);
        }
        nextShapeCoroutine = StartCoroutine(NextShapesCoroutine(Tutorial));
    }

    private IEnumerator NextShapesCoroutine(bool tutorial)
    {
        NumberOfWrongAnswer = 0;
        for (int i = 0; i < shapeHolders.Length; i++)
        {
            shapeHolders[i].sprite = shapeHolderSprites[0];
        }
        List<int> indexes = new List<int>();
        indexes.Add(0);
        indexes.Add(1);
        indexes.Add(2);
        indexes.Add(3);

        int rightShape = Random.Range(0, indexes.Count);
        indexes.RemoveAt(rightShape);
        int wrongShapeIndex = Random.Range(0, indexes.Count);
        int wrongShape = indexes[wrongShapeIndex];
        indexes.RemoveAt(wrongShapeIndex);
        indexes.Clear();
        indexes.Add(0);
        indexes.Add(1);
        indexes.Add(2);
        indexes.Add(3);
        wrongShapeIndex = Random.Range(0, indexes.Count);
        indexes.RemoveAt(wrongShapeIndex);
        shapes[wrongShapeIndex].sprite = shapeSprites[rightShape];
        shapes[wrongShapeIndex].preserveAspect = true;
        shapes[wrongShapeIndex].SetNativeSize();
        correctShapeHolderIndex = wrongShapeIndex;
        foreach (int index in indexes)
        {
            shapes[index].sprite = shapeSprites[wrongShape];
            shapes[index].preserveAspect = true;
            shapes[index].SetNativeSize();
        }

        for (int i = 0; i < 4; i++)
        {
            shapes[i].DOFade(1f, 1f).From(0f).SetEase(Ease.Linear);
        }



        if (!tutorial)
        {
            if(setNumber == 0)
			{
                EnableSpeaking();
                yield return new WaitForSeconds(mascot.PlayClip(2));
                DisableSpeaking();
            }
            firstAttempt = true;
            CanSelectBox = true;

            StartVoiceInstructionCoroutine();
        }
        AudioManager.instance.IncreaseBackgroundMusicVolume();
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

    private void DisableSpeaking()
    {
        if (mascotOnScreen)
        {
            mascot.speechDotsRect.gameObject.SetActive(false);
            mascot.speechBubble.gameObject.SetActive(false);
        }
    }

    private void EnableSpeaking()
    {
        if (mascotOnScreen)
        {
            mascot.speechBubble.gameObject.SetActive(true);
            mascot.speechDotsRect.gameObject.SetActive(true);
        }
    }

    private void CorrectAnswer(int shapeHolderIndex)
    {
        if (!Tutorial)
        {
            // progress calculation
            setNumber++;
            if (firstAttempt || (setNumber % 4 == 0 && setNumber > 0 && setNumber < 10))
            {
                Progress++;
            }
            if (firstAttempt && setNumber > 9)
            {
                Progress = progressSprites.Length - 1;
                correctAnswerStreak++;
            }
        }
        shapeHolders[shapeHolderIndex].sprite = shapeHolderSprites[1];
        StartCoroutine(CorrectAnswerAnimation());
    }

    private IEnumerator CorrectAnswerAnimation()
    {
        yield return new WaitForSeconds(AudioManager.instance.Play("CorrectAnswer"));

        shapes[0].DOFade(0f, 1f);
        shapes[1].DOFade(0f, 1f);
        shapes[2].DOFade(0f, 1f);
        shapes[3].DOFade(0f, 1f);
        yield return oneSec;
        if (!Tutorial)
        {
            if (correctAnswerStreak == 4 || setNumber == 10)
            {
                // level completed

                shapeHolders[0].gameObject.SetActive(false);
                shapeHolders[1].gameObject.SetActive(false);
                shapeHolders[2].gameObject.SetActive(false);
                shapeHolders[3].gameObject.SetActive(false);
                confetti.SetActive(true);
                Constants.Level1.canSkip = true;
                Constants.Level1.firstTimeLevel = false;
                Constants.Level1.progress = Progress;
                Debug.Log("RewardsBeforeIncreament" + Constants.rewards);
                Constants.rewards = int.Parse(rewardsText.text) + 10;
                Debug.Log("RewardsAfterIncreament" + Constants.rewards);
                rewardsText.text = Constants.rewards.ToString();
                PlayerPrefs.SetString("Score", rewardsText.text);
                PlayerPrefs.SetInt("LevelBar", Constants.Level1.progress);
                yield return new WaitForSeconds(AudioManager.instance.Play("LevelComplete") + 1f);
                EnableSpeaking();
                levelCompletedIcons.SetActive(true);
                yield return new WaitForSeconds(AudioManager.instance.Play("NextLevelInstructions") + 1f);
                DisableSpeaking();
                CanSelectBox = false;
                AudioManager.instance.Stop("BackgroundMusic");
            }
            else
            {
                NextShapes();
            }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        Constants.Level2.canLoadSavedScene = true;
    }

    public void HomeButtonClicked()
    {

        Time.timeScale = 0f;
        PlayerPrefs.SetInt("SetNumber_Level1", setNumber);
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        CanSelectBox = false;
        AudioManager.instance.PlayMenuAudio("HomeButtonAudio");
        homeButtonIcons.SetActive(true);
        levelCompletedIcons.SetActive(false);
        presentationEndIcons.SetActive(false);
        presentationSkipButtonIcons.SetActive(false);
        skipButtonIcons.SetActive(false);
    }

    public void HomeContinueClicked()
    {
        if (Constants.Level1.firstPresentation)
        {
            Time.timeScale = 1f;
            MenuButtonsOnScreen = false;
            AudioManager.instance.Stop("HomeButtonAudio");
            AudioManager.instance.ContinueAudio();
            hintButton.interactable = false;
            CanSelectBox = false;
            homeButtonIcons.SetActive(false);
        }
		else
		{
            if (NumberOfWrongAnswer == 1)
            {
                Time.timeScale = 1f;
                MenuButtonsOnScreen = false;
                AudioManager.instance.Stop("HomeButtonAudio");
                AudioManager.instance.ContinueAudio();
                hintButton.interactable = false;
                CanSelectBox = false;
                homeButtonIcons.SetActive(false);
            }
            else if(NumberOfWrongAnswer == 2)
			{
                Time.timeScale = 1f;
                MenuButtonsOnScreen = false;
                AudioManager.instance.Stop("HomeButtonAudio");
                AudioManager.instance.ContinueAudio();
                hintButton.interactable = false;
                CanSelectBox = false;
                homeButtonIcons.SetActive(false);
            }
            else if(NumberOfWrongAnswer == 3)
			{
                Time.timeScale = 1f;
                MenuButtonsOnScreen = false;
                AudioManager.instance.Stop("HomeButtonAudio");
                AudioManager.instance.ContinueAudio();
                hintButton.interactable = false;
                CanSelectBox = false;
                homeButtonIcons.SetActive(false);
            }
			else if(correctShapeHolderIndex == 1)
			{
                Time.timeScale = 1f;
                MenuButtonsOnScreen = false;
                AudioManager.instance.Stop("HomeButtonAudio");
                AudioManager.instance.ContinueAudio();
                hintButton.interactable = false;
                CanSelectBox = true;
                homeButtonIcons.SetActive(false);
            }
        }
    }

    public void ReplayAudioInstructions()
    {
        StartCoroutine(AudioInstructionsRepeatCoroutine());
    }


    private IEnumerator AudioInstructionsRepeatCoroutine()
    {
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        CanSelectBox = false;
        yield return new WaitForSeconds(mascot.PlayClip(4));
        CanSelectBox = true;
        AudioManager.instance.IncreaseBackgroundMusicVolume();
    }

    private void WrongAnswer(int shapeHolderIndex)
    {
        if (!Tutorial)
        {
            firstAttempt = false;
            correctAnswerStreak = 0;
        }
        shapeHolders[shapeHolderIndex].sprite = shapeHolderSprites[2];
        NumberOfWrongAnswer++;
        StartCoroutine(WrongAnswerAnimation(shapeHolderIndex));
    }
    private IEnumerator WrongAnswerAnimation(int shapeHolderIndex)
    {
        yield return new WaitForSeconds(AudioManager.instance.Play("WrongAnswer"));
        hand.DOAnchorPos(Vector2.zero, 1f);
        yield return oneSec;
        EnableSpeaking();
        yield return new WaitForSeconds(AudioManager.instance.Play("TryAgain"));
        yield return oneSec;
        yield return new WaitForSeconds(mascot.PlayClip(4));
        DisableSpeaking();
        shapeHolders[shapeHolderIndex].sprite = shapeHolderSprites[0];
        CanSelectBox = true;
        StartVoiceInstructionCoroutine();
        AudioManager.instance.IncreaseBackgroundMusicVolume();
        if(NumberOfWrongAnswer == 3)
		{
            correctShape = correctShapeHolderIndex;
            if(correctShape == correctShapeHolderIndex)
			{
                Arrow(correctShapeHolderIndex);
                yield return new WaitForSeconds(2f);
                for(int i = 0; i < Arrows.Length; i++)
				{
                    Arrows[i].gameObject.SetActive(false);
				}
                for (int i = 0; i < boxes.Length; i++)
                {
                    boxes[i].gameObject.GetComponent<Button>().enabled = true;
                }
                Debug.Log("CorrectShapeIndex: " + correctShapeHolderIndex);
            }
        }
    }

    private void KillAllTweens()
    {
        for (int i = 0; i < shapeHolders.Length; i++)
        {
            shapeHolders[i].DOKill();
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
        SceneLoader.instance.LoadNextLevel(Constants.Level1.nextLevel);
        AudioManager.instance.Play("BackgroundMusic");
    }
    public void ReplayLevel()
    {
        StopAllCoroutines();
        KillAllTweens();
        Time.timeScale = 1f;
        SceneLoader.instance.ReplayLevel();
        AudioManager.instance.Play("BackgroundMusic");
    }

    public void BackButtonClicked()
	{
        Time.timeScale = 0f;
        backButtonIcons.SetActive(true);
        AudioManager.instance.PlayMenuAudio("HomeButtonAudio");
	}
    public void BackContinueClicked()
	{
        Time.timeScale = 1f;
        MenuButtonsOnScreen = false;
        AudioManager.instance.Stop("HomeButtonAudio");
        CanSelectBox = true;
        backButtonIcons.SetActive(false);
    }

    public void PreviousLevelClicked()
	{
        Time.timeScale = 0f;
        Constants.Level0.canSkip = true;
        PlayerPrefs.SetInt("SetNumber_Level1", setNumber);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        PlayerPrefs.GetInt("LevelBar");
        PlayerPrefs.GetString("Score");
		if(setNumber != 10)
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            Time.timeScale = 1f;
        }
	}

    public void GoToHomeScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetNumberData()
	{
        if(PlayerPrefs.GetInt("SetNumber_Level1") == 0)
		{

            boxParent.gameObject.SetActive(true);
            for(int i = 0; i < shapeHolders.Length; i++)
			{
                shapeHolders[i].gameObject.SetActive(true);
			}
            for(int j = 0; j < shapes.Length; j++)
			{
                shapes[j].gameObject.SetActive(true);
			}
            NextShapes();
		}
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 1)
        {
            Progress = 0;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 2)
        {
            Progress = 1;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 3)
        {
            Progress = 2;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 4)
        {
            Progress = 3;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 5)
        {
            Progress = 4;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 6)
        {
            Progress = 5;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 7)
        {
            Progress = 6;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
        if (PlayerPrefs.GetInt("SetNumber_Level1") == 8)
        {
            Progress = 7;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
        }
		if(PlayerPrefs.GetInt("SetNumber_Level1") == 9)
		{
            Progress = 8;
            boxParent.gameObject.SetActive(true);
            for (int i = 0; i < shapeHolders.Length; i++)
            {
                shapeHolders[i].gameObject.SetActive(true);
            }
            for (int j = 0; j < shapes.Length; j++)
            {
                shapes[j].gameObject.SetActive(true);
            }
            NextShapes();
		}
    }

    public void Arrow(int correctShape)
	{
        correctShape = correctShapeHolderIndex;
        if (correctShape == 0)
        {
            Arrows[0].gameObject.SetActive(true);
            for(int i = 0; i < boxes.Length; i++)
			{
                boxes[i].gameObject.GetComponent<Button>().enabled = false;
			}
        }
        if (correctShape == 1)
        {
            Arrows[1].gameObject.SetActive(true);
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].gameObject.GetComponent<Button>().enabled = false;
            }
        }
        if (correctShape == 2)
        {
            Arrows[2].gameObject.SetActive(true);
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].gameObject.GetComponent<Button>().enabled = false;
            }
        }
        if(correctShape == 3)
        {
            Arrows[3].gameObject.SetActive(true);
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].gameObject.GetComponent<Button>().enabled = false;
            }
        }
    }

}
