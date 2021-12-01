using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Level2GM : MonoBehaviour
{

    public static Level2GM instance;
    public int setNumber;
    public int currentSet;
    public Level0MascotManager mascot;
    [SerializeField] Image[] shapes;
    [SerializeField] RectTransform hand;
    [SerializeField] GameObject boxParent;
    [SerializeField] Image handImage;
    [SerializeField] GameObject confetti;
    [SerializeField] GameObject levelCompletedIcones;
    [SerializeField] Button audioReplay;
    [SerializeField] Image progressImage;
    [SerializeField] Sprite[] progressSprites;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] GameObject homeButtonIcons;
    [SerializeField] GameObject skipButtonIcons;
    [SerializeField] Button homeButton;
    [SerializeField] Button skipButton;
    [SerializeField] Button hintButton;
    [SerializeField] Button[] boxes;
    [SerializeField] GameObject presentationSkipButtonIcons;
    [SerializeField] GameObject presentationEndIcons;
    
    WaitForSeconds oneSec = new WaitForSeconds(1f);
    Vector2 initMascotPos1;
    bool mascotOnScreen;
    int CorrectAnswerStreak;
    bool firstAttempt;
    int progress;
    int correctShapeHolderIndex;
    bool audioMute = false;
    bool canSelectBox = false;
    bool tutorial;
    Coroutine VoiceInstructionRepeatCoroutine;
    Coroutine presentationCoroutine;
    Coroutine nextShapeCoroutine;
    bool menuButtonsOnScreen = false;
    bool onoff = false;
    public Image speechBubbleImage;
    public Image speechDotsImage;
    public Image shapeImage;
    public GameObject handGameObject;
    public Image[] tickImages;
    public Image audioReplayImage;
    public GameObject[] sets;
    public List<UpperShapes2> upperShapes = new List<UpperShapes2>();
    public List<Shapes2> shape = new List<Shapes2>();
    int numberOfCircles;
    int numberOfTriangles;
    int numberOfRectangles;
    int numberOfSquares;
    int numberOfCircles2;
    int numberOfSquares2;
    int numberOfRectangles2;
    int numberOfTriangles2;
    int numberOfSquares3;
    int numberOfRectangles3;
    public Image[] progressImages;
    public Sprite buttonImage;
    public Sprite newButtonImage;
    public Button musicButton;
    public Button backButton;
    public GameObject backButtonGameObject;
    Coroutine voiceInstructionRepeatCoroutine;
    public int NumberOfWrongAnswers;
    public int NumberOfWrongAnswers1;
    public int NumberOfWrongAnswers2;
    public int NumberOfWrongAnswers3;
    public int NumberOfWrongAnswers4;
    public int NumberOfWrongAnswers5;
    public int NumberOfWrongAnswers6;
    public int NumberOfWrongAnswers7;
    public int NumberOfWrongAnswers8;
    public int NumberOfWrongAnswers9;
    public GameObject Panel;
    public Image Level3;
    public Image Level4;


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
            if(!Tutorial && !value)
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

    public bool CanSelectBox
	{
		get
		{
            return canSelectBox;
		}
		set
		{
            if(!Tutorial && value)
			{
                canSelectBox = value;
                audioReplay.interactable = value;
                for(int i = 0; i < boxes.Length; i++)
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
            if(progress < progressSprites.Length)
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
        if(Constants.rewards > 40)
		{
            progressImages[9].gameObject.SetActive(true);
            Level3.gameObject.SetActive(true);
            Level4.gameObject.SetActive(true);
            mascot.gameObject.SetActive(false);
            Time.timeScale = 1f;
            setNumber = 0;
            Constants.Level2.canLoadSavedScene = true;
        }
        if (Constants.Level2.canLoadSavedScene)
        {
            mascotOnScreen = false;
            mascot.gameObject.SetActive(false);
            backButton.interactable = true;
            hintButton.interactable = true;
            StopAllCoroutines();
            Time.timeScale = 1f;
            PlayerPrefs.GetInt("SetNumber");
            setNumber = PlayerPrefs.GetInt("SetNumber");
            SetNumberData();
        }
		else
		{
            mascotOnScreen = true;
            setNumber = 0;
            Progress = Constants.Level2.progress;
            rewardText.text = Constants.rewards.ToString();
            numberOfCircles = 0;
            numberOfTriangles = 0;
            numberOfRectangles = 0;
            numberOfSquares = 0;
            numberOfCircles2 = 0;
            numberOfSquares2 = 0;
            numberOfRectangles2 = 0;
            numberOfTriangles2 = 0;
            numberOfSquares3 = 0;
            numberOfRectangles3 = 0;
            NumberOfWrongAnswers = 0;
            NumberOfWrongAnswers1 = 0;
            NumberOfWrongAnswers2 = 0;
            NumberOfWrongAnswers3 = 0;
            NumberOfWrongAnswers4 = 0;
            NumberOfWrongAnswers5 = 0;
            NumberOfWrongAnswers6 = 0;
            NumberOfWrongAnswers7 = 0;
            NumberOfWrongAnswers8 = 0;
            NumberOfWrongAnswers9 = 0;
            if (Constants.Level2.gameState == Constants.State.NORMAL)
            {
                Constants.Level2.nextLevel = 4;
            }
            if (Constants.Level2.canSkip)
            {
                skipButton.gameObject.SetActive(true);
                skipButton.onClick.RemoveAllListeners();
                skipButton.onClick.AddListener(SkipButtonClicked);
                progressImages[9].gameObject.SetActive(true);
            }
            CorrectAnswerStreak = 0;
            currentSet = 0;
            initMascotPos1 = mascot.rect.anchoredPosition;
            Tutorial = true;
            StartCoroutine(MascotAppear());
        }
       
    }

    IEnumerator MascotAppear()
	{
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
        mascot.rect.DOAnchorPos(new Vector2(344f, -90f), 2f);
        mascot.rect.DOScale(1f, 2f);

        yield return oneSec;
        yield return oneSec;
        boxParent.SetActive(true);
        shapeImage.gameObject.SetActive(true);
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
        handGameObject.SetActive(true);
        hand.DOAnchorPos(new Vector2(-264f, -112f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[0].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(39f, -225f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[1].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(203f, -225f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[2].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(340f, -111f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[3].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        yield return oneSec;
        for(int i = 0; i < tickImages.Length; i++)
		{
            tickImages[i].gameObject.SetActive(false);
		}
        shapes[1].gameObject.SetActive(false);
        shapes[2].gameObject.SetActive(false);
        shapes[3].gameObject.SetActive(false);
        shapes[5].gameObject.SetActive(false);
        shapes[6].gameObject.SetActive(false);
        shapes[9].gameObject.SetActive(false);
        handGameObject.SetActive(false);
        yield return oneSec;
        yield return oneSec;
        for(int i = 0; i < shapes.Length; i++)
		{
            shapes[i].gameObject.SetActive(false);
		}
        shapeImage.gameObject.SetActive(false);
        mascot.MascotReappear();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(2));
        hand.DOAnchorPos(new Vector2(0f, 0f), 2f);
        yield return oneSec;
        yield return oneSec;
        audioReplayImage.gameObject.SetActive(true);
        audioReplay.GetComponent<Button>().enabled = false;
        handGameObject.SetActive(true);
        hand.DOAnchorPos(new Vector2(394f, 103f), 2f);
        yield return oneSec;
        AudioManager.instance.Play("ReplayAudio");
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        handGameObject.SetActive(false);
        mascot.MascotDisappear();
        yield return oneSec;
        setNumber = 0;
        sets[0].gameObject.SetActive(true);
        for(int i = 0; i < shape.Count; i++)
		{
            for(int j = 0; j < shape[i].image.Length; j++)
			{
                shape[0].image[j].GetComponent<Button>().enabled = false;
			}
		}
        skipButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(mascot.PlayClip(1));
        backButton.interactable = true;
        hintButton.interactable = true;
        audioReplay.GetComponent<Button>().enabled = true;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[0].image[j].GetComponent<Button>().enabled = true;
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    public void Set1(Button button)
	{
        GameObject set1Objects = button.gameObject;
        string nameOfCircles = set1Objects.GetComponent<Image>().sprite.name;
        if(nameOfCircles == upperShapes[0].id)
		{
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set1Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfCircles++;
            set1Objects.GetComponent<Button>().enabled = false;
            if (numberOfCircles == 3)
			{
                progressImages[0].gameObject.SetActive(true);
                setNumber++;
                for(int i = 0; i < shape.Count; i++)
				{
                    for(int j = 0; j < shape[i].image.Length; j++)
					{
                        shape[0].image[j].gameObject.GetComponent<Button>().enabled = false;
					}
				}
                StartCoroutine(WaitForSeconds1());
			}
		}
        else
        {
            NumberOfWrongAnswers++;
            AudioManager.instance.Play("WrongAnswer");
            set1Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set2(Button button)
    {
        GameObject set2Objects = button.gameObject;
        string nameOfCircles = set2Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[1].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay : true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set2Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfTriangles++;
            set2Objects.GetComponent<Button>().enabled = false;
            if (numberOfTriangles == 2)
            {
                progressImages[1].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[1].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds2());
            }
        }
        else
        {
            NumberOfWrongAnswers1++;
            AudioManager.instance.Play("WrongAnswer");
            set2Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set3(Button button)
    {
        GameObject set3Objects = button.gameObject;
        string nameOfCircles = set3Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[2].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer" , replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set3Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfRectangles++;
            set3Objects.GetComponent<Button>().enabled = false;
            if (numberOfRectangles == 2)
            {
                progressImages[2].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[2].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds3());
            }
        }
        else
        {
            NumberOfWrongAnswers2++;
            AudioManager.instance.Play("WrongAnswer");
            set3Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set4(Button button)
    {
        GameObject set4Objects = button.gameObject;
        string nameOfCircles = set4Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[3].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set4Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfSquares++;
            set4Objects.GetComponent<Button>().enabled = false;
            if (numberOfSquares == 3)
            {
                progressImages[3].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[3].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds4());
            }
        }
        else
        {
            NumberOfWrongAnswers3++;
            AudioManager.instance.Play("WrongAnswer");
            set4Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set5(Button button)
    {
        StartCoroutine(PlayCorrectAnswer());
        GameObject set5Objects = button.gameObject;
        string nameOfCircles = set5Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[4].id)
        {
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set5Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfTriangles2++;
            set5Objects.GetComponent<Button>().enabled = false;
            if (numberOfTriangles2 == 2)
            {
                progressImages[4].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[4].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds5());
            }
        }
        else
        {
            NumberOfWrongAnswers4++;
            AudioManager.instance.Play("WrongAnswer");
            set5Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set6(Button button)
    {
        GameObject set6Objects = button.gameObject;
        string nameOfCircles = set6Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[5].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set6Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfCircles2++;
            set6Objects.GetComponent<Button>().enabled = false;
            if (numberOfCircles2 == 3)
            {
                progressImages[5].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[5].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds6());
            }
        }
        else
        {
            NumberOfWrongAnswers5++;
            AudioManager.instance.Play("WrongAnswer");
            set6Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set7(Button button)
    {
        GameObject set7Objects = button.gameObject;
        string nameOfCircles = set7Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[6].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set7Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfRectangles2++;
            set7Objects.GetComponent<Button>().enabled = false;
            if (numberOfRectangles2 == 2)
            {
                progressImages[6].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[6].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds7());
            }
        }
        else
        {
            NumberOfWrongAnswers6++;
            AudioManager.instance.Play("WrongAnswer");
            set7Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set8(Button button)
    {
        GameObject set8Objects = button.gameObject;
        string nameOfCircles = set8Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[7].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set8Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfSquares2++;
            set8Objects.GetComponent<Button>().enabled = false;
            if (numberOfSquares2 == 3)
            {
                progressImages[7].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[7].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds8());
            }
        }
        else
        {
            NumberOfWrongAnswers7++;
            AudioManager.instance.Play("WrongAnswer");
            set8Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set9(Button button)
    {
        GameObject set9Objects = button.gameObject;
        string nameOfCircles = set9Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[8].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set9Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfSquares3++;
            set9Objects.GetComponent<Button>().enabled = false;
            if (numberOfSquares3 == 3)
            {
                progressImages[8].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[8].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds9());
            }
        }
        else
        {
            NumberOfWrongAnswers8++;
            AudioManager.instance.Play("WrongAnswer");
            set9Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    public void Set10(Button button)
    {
        GameObject set10Objects = button.gameObject;
        string nameOfCircles = set10Objects.GetComponent<Image>().sprite.name;
        if (nameOfCircles == upperShapes[9].id)
        {
            StartCoroutine(PlayCorrectAnswer());
            //AudioManager.instance.DecreaseBackgroundMusicVolume();
            //AudioManager.instance.Play("CorrectAnswer", replay: true);
            //AudioManager.instance.IncreaseBackgroundMusicVolume();
            set10Objects.transform.GetChild(0).gameObject.SetActive(true);
            numberOfRectangles3++;
            set10Objects.GetComponent<Button>().enabled = false;
            if (numberOfRectangles3 == 2)
            {
                progressImages[9].gameObject.SetActive(true);
                setNumber++;
                for (int i = 0; i < shape.Count; i++)
                {
                    for (int j = 0; j < shape[i].image.Length; j++)
                    {
                        shape[9].image[j].gameObject.GetComponent<Button>().enabled = false;
                    }
                }
                StartCoroutine(WaitForSeconds10());
            }
        }
        else
        {
            NumberOfWrongAnswers9++;
            AudioManager.instance.Play("WrongAnswer");
            set10Objects.transform.GetChild(1).gameObject.SetActive(true);
            StartCoroutine(WrongAnswerMusic());
        }
    }

    IEnumerator WrongAnswerMusic()
	{
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[0].image[j].gameObject.GetComponent<Button>().enabled = false;
            }
        }
        yield return oneSec;
        yield return oneSec;
        AudioManager.instance.Play("TryAgain");
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[i].image[j].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        yield return new WaitForSeconds(mascot.PlayClip(1));
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
            }
        }
        if(setNumber == 0 && NumberOfWrongAnswers == 3)
		{
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[0].image[2].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[0].image[2].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 1 && NumberOfWrongAnswers1 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[1].image[9].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[1].image[9].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 2 && NumberOfWrongAnswers2 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[2].image[5].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[2].image[5].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 3 && NumberOfWrongAnswers3 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[3].image[1].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[3].image[1].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 4 && NumberOfWrongAnswers4 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[4].image[1].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[4].image[1].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 5 && NumberOfWrongAnswers5 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[5].image[4].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[5].image[4].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 6 && NumberOfWrongAnswers6 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[6].image[3].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[6].image[3].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 7 && NumberOfWrongAnswers7 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[7].image[8].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[7].image[8].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 8 && NumberOfWrongAnswers8 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[8].image[0].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[8].image[0].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
            //NumberOfWrongAnswers = 0;
        }
        if (setNumber == 9 && NumberOfWrongAnswers9 == 3)
        {
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = false;
                }
            }
            shape[9].image[0].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            shape[9].image[0].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
                }
            }
        }
        //NumberOfWrongAnswers = 0;
    }

    IEnumerator WaitForSeconds1()
	{
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[0].image[1].gameObject.SetActive(false);
                shape[0].image[3].gameObject.SetActive(false);
                shape[0].image[4].gameObject.SetActive(false);
                shape[0].image[5].gameObject.SetActive(false);
                shape[0].image[6].gameObject.SetActive(false);
                shape[0].image[7].gameObject.SetActive(false);
                shape[0].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[0].image[0].gameObject.SetActive(false);
                shape[0].image[2].gameObject.SetActive(false);
                shape[0].image[8].gameObject.SetActive(false);
            }
        }
        upperShapes[0].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[1].SetActive(true);
        upperShapes[1].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[1].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds2()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[1].image[0].gameObject.SetActive(false);
                shape[1].image[2].gameObject.SetActive(false);
                shape[1].image[3].gameObject.SetActive(false);
                shape[1].image[4].gameObject.SetActive(false);
                shape[1].image[5].gameObject.SetActive(false);
                shape[1].image[6].gameObject.SetActive(false);
                shape[1].image[7].gameObject.SetActive(false);
                shape[1].image[8].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[1].image[1].gameObject.SetActive(false);
                shape[1].image[9].gameObject.SetActive(false);
            }
        }
        upperShapes[1].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[2].SetActive(true);
        upperShapes[2].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[2].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds3()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[2].image[0].gameObject.SetActive(false);
                shape[2].image[1].gameObject.SetActive(false);
                shape[2].image[2].gameObject.SetActive(false);
                shape[2].image[3].gameObject.SetActive(false);
                shape[2].image[6].gameObject.SetActive(false);
                shape[2].image[7].gameObject.SetActive(false);
                shape[2].image[8].gameObject.SetActive(false);
                shape[2].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[2].image[4].gameObject.SetActive(false);
                shape[2].image[5].gameObject.SetActive(false);
            }
        }
        upperShapes[2].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[3].SetActive(true);
        upperShapes[3].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[3].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds4()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[3].image[0].gameObject.SetActive(false);
                shape[3].image[2].gameObject.SetActive(false);
                shape[3].image[3].gameObject.SetActive(false);
                shape[3].image[4].gameObject.SetActive(false);
                shape[3].image[5].gameObject.SetActive(false);
                shape[3].image[6].gameObject.SetActive(false);
                shape[3].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[3].image[1].gameObject.SetActive(false);
                shape[3].image[7].gameObject.SetActive(false);
                shape[3].image[8].gameObject.SetActive(false);
            }
        }
        upperShapes[3].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[4].SetActive(true);
        upperShapes[4].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[4].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds5()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[4].image[0].gameObject.SetActive(false);
                shape[4].image[2].gameObject.SetActive(false);
                shape[4].image[3].gameObject.SetActive(false);
                shape[4].image[4].gameObject.SetActive(false);
                shape[4].image[6].gameObject.SetActive(false);
                shape[4].image[7].gameObject.SetActive(false);
                shape[4].image[8].gameObject.SetActive(false);
                shape[4].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[4].image[1].gameObject.SetActive(false);
                shape[4].image[5].gameObject.SetActive(false);
            }
        }
        upperShapes[4].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[5].SetActive(true);
        upperShapes[5].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[5].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds6()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[5].image[0].gameObject.SetActive(false);
                shape[5].image[1].gameObject.SetActive(false);
                shape[5].image[2].gameObject.SetActive(false);
                shape[5].image[3].gameObject.SetActive(false);
                shape[5].image[5].gameObject.SetActive(false);
                shape[5].image[6].gameObject.SetActive(false);
                shape[5].image[7].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[5].image[4].gameObject.SetActive(false);
                shape[5].image[8].gameObject.SetActive(false);
                shape[5].image[9].gameObject.SetActive(false);
            }
        }
        upperShapes[5].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[6].SetActive(true);
        upperShapes[6].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[6].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds7()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[6].image[0].gameObject.SetActive(false);
                shape[6].image[1].gameObject.SetActive(false);
                shape[6].image[2].gameObject.SetActive(false);
                shape[6].image[4].gameObject.SetActive(false);
                shape[6].image[6].gameObject.SetActive(false);
                shape[6].image[7].gameObject.SetActive(false);
                shape[6].image[8].gameObject.SetActive(false);
                shape[6].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[6].image[3].gameObject.SetActive(false);
                shape[6].image[5].gameObject.SetActive(false);
            }
        }
        upperShapes[6].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[7].SetActive(true);
        upperShapes[7].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[7].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds8()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[7].image[0].gameObject.SetActive(false);
                shape[7].image[3].gameObject.SetActive(false);
                shape[7].image[4].gameObject.SetActive(false);
                shape[7].image[5].gameObject.SetActive(false);
                shape[7].image[6].gameObject.SetActive(false);
                shape[7].image[7].gameObject.SetActive(false);
                shape[7].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[7].image[1].gameObject.SetActive(false);
                shape[7].image[2].gameObject.SetActive(false);
                shape[7].image[8].gameObject.SetActive(false);
            }
        }
        upperShapes[7].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[8].SetActive(true);
        upperShapes[8].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[8].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds9()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[8].image[2].gameObject.SetActive(false);
                shape[8].image[3].gameObject.SetActive(false);
                shape[8].image[4].gameObject.SetActive(false);
                shape[8].image[5].gameObject.SetActive(false);
                shape[8].image[6].gameObject.SetActive(false);
                shape[8].image[7].gameObject.SetActive(false);
                shape[8].image[8].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[8].image[0].gameObject.SetActive(false);
                shape[8].image[1].gameObject.SetActive(false);
                shape[8].image[9].gameObject.SetActive(false);
            }
        }
        upperShapes[8].image.gameObject.SetActive(false);
        yield return oneSec;
        sets[9].SetActive(true);
        upperShapes[9].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[9].image[j].gameObject.SetActive(true);
            }
        }
        PlayerPrefs.SetInt("SetNumber", setNumber);
    }

    IEnumerator WaitForSeconds10()
    {
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[9].image[1].gameObject.SetActive(false);
                shape[9].image[2].gameObject.SetActive(false);
                shape[9].image[3].gameObject.SetActive(false);
                shape[9].image[4].gameObject.SetActive(false);
                shape[9].image[5].gameObject.SetActive(false);
                shape[9].image[7].gameObject.SetActive(false);
                shape[9].image[8].gameObject.SetActive(false);
                shape[9].image[9].gameObject.SetActive(false);
            }
        }
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[9].image[0].gameObject.SetActive(false);
                shape[9].image[6].gameObject.SetActive(false);
            }
        }
        upperShapes[9].image.gameObject.SetActive(false);
        yield return oneSec;
        boxParent.SetActive(false);
        confetti.SetActive(true);
        Constants.rewards = int.Parse(rewardText.text) + 10;
        rewardText.text = Constants.rewards.ToString();
        PlayerPrefs.SetString("Score", rewardText.text);
        yield return new WaitForSeconds(AudioManager.instance.Play("LevelComplete") + 1f);
        EnableSpeaking();
        levelCompletedIcones.SetActive(true);
        yield return new WaitForSeconds(AudioManager.instance.Play("NextLevelInstructions") + 1f);
        DisableSpeaking();
        CanSelectBox = false;
        AudioManager.instance.Stop("BackgroundMusic");
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

    void DisableSpeaking()
	{
		if (mascotOnScreen)
		{
            mascot.speechDotsRect.gameObject.SetActive(false);
            mascot.speechBubble.gameObject.SetActive(false);
		}
	}

    void EnableSpeaking()
	{
		if (mascotOnScreen)
		{
            mascot.speechBubble.gameObject.SetActive(true);
            mascot.speechDotsRect.gameObject.SetActive(true);
		}
	}

    public void HomeButtonClicked()
    {
        PlayerPrefs.SetInt("SetNumber", setNumber);
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        AudioManager.instance.PauseAudio();
        CanSelectBox = false;
        AudioManager.instance.PlayMenuAudio("HomeButtonAudio");
        homeButtonIcons.SetActive(true);
        levelCompletedIcones.SetActive(false);
        presentationEndIcons.SetActive(false);
        presentationSkipButtonIcons.SetActive(false);
        skipButtonIcons.SetActive(false);
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
    }

    public void ToggleBackgroundMusic()
    {
        AudioMute = !AudioMute;
    }

    public void BackButtonClicked()
    {
        Time.timeScale = 0f;
        MenuButtonsOnScreen = true;
        backButton.interactable = false;
        AudioManager.instance.PauseAudio();
        CanSelectBox = false;
        AudioManager.instance.PlayMenuAudio("HomeButtonAudio");
        backButtonGameObject.SetActive(true);
    }

    public void PreviousLevelClicked()
    {
        AudioManager.instance.Stop("HomeButtonAudio");
        Time.timeScale = 0f;
        AudioManager.instance.Stop("BackgroundMusic");
        Constants.Level1.canSkip = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        AudioManager.instance.Play("BackgroundMusic");
        Constants.Level1.firstTimeLevel = false;
        Level1Manager.instance.StopAllCoroutines();
        Level1Manager.instance.NextShapes();
        Level1Manager.instance.skipButton.gameObject.SetActive(true);
        Level1Manager.instance.skipButton.onClick.RemoveAllListeners();
        Level1Manager.instance.skipButton.onClick.AddListener(SkipButtonClicked);
        PlayerPrefs.SetInt("SetNumber", setNumber);
        PlayerPrefs.GetInt("LevelBar");
        PlayerPrefs.GetString("Score");
        if (setNumber != 10)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            Time.timeScale = 1f;
            Constants.Level1.firstTimeLevel = false;
            Level1Manager.instance.StopAllCoroutines();
            Level1Manager.instance.NextShapes();
        }
    }

    public void StartPresentation()
    {
        PlayerPrefs.SetInt("SetNumber", setNumber);
        hintButton.interactable = false;
        musicButton.image.sprite = buttonImage;
        AudioManager.instance.Stop("BackgroundMusic");
        AudioManager.instance.Play("BackgroundMusic");
        boxParent.SetActive(false);
        for (int i = 0; i < sets.Length; i++)
        {
            sets[i].gameObject.SetActive(false);
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
        StartCoroutine(Presentation());

    }
    IEnumerator Presentation()
    {
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
        mascot.rect.DOAnchorPos(new Vector3(344f, -90f), 2f);
        mascot.rect.DOScale(1f, 2f);

        yield return oneSec;
        yield return oneSec;
        boxParent.SetActive(true);
        shapeImage.gameObject.SetActive(true);
        mascot.SpeechBubbleLeft();
        onoff = true;
        mascot.ChangeMascotImage();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(1));
        mascot.MascotDisappear();
        yield return oneSec;
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].gameObject.SetActive(true);
        }
        handGameObject.SetActive(true);
        hand.DOAnchorPos(new Vector2(-264f, -112f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[0].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(39f, -225f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[1].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(203f, -225f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[2].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        hand.DOAnchorPos(new Vector2(340f, -111f), 2f);
        yield return oneSec;
        yield return oneSec;
        tickImages[3].gameObject.SetActive(true);
        AudioManager.instance.Play("CorrectAnswer");
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < tickImages.Length; i++)
        {
            tickImages[i].gameObject.SetActive(false);
        }
        shapes[1].gameObject.SetActive(false);
        shapes[2].gameObject.SetActive(false);
        shapes[3].gameObject.SetActive(false);
        shapes[5].gameObject.SetActive(false);
        shapes[6].gameObject.SetActive(false);
        shapes[9].gameObject.SetActive(false);
        handGameObject.SetActive(false);
        yield return oneSec;
        yield return oneSec;
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].gameObject.SetActive(false);
        }
        shapeImage.gameObject.SetActive(false);
        mascot.MascotReappear();
        EnableSpeaking();
        yield return new WaitForSeconds(mascot.PlayClip(2));
        hand.DOAnchorPos(new Vector2(0f, 0f), 2f);
        yield return oneSec;
        yield return oneSec;
        audioReplayImage.gameObject.SetActive(true);
        audioReplay.GetComponent<Button>().enabled = false;
        handGameObject.SetActive(true);
        hand.DOAnchorPos(new Vector2(394f, 103f), 2f);
        yield return oneSec;
        AudioManager.instance.Play("ReplayAudio");
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        handGameObject.SetActive(false);
        mascot.MascotDisappear();
        yield return oneSec;
        boxParent.SetActive(false);
        presentationEndIcons.SetActive(true);
        presentationEndIcons.transform.GetChild(0).GetComponent<Button>().enabled = false;
        presentationEndIcons.transform.GetChild(1).GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(AudioManager.instance.Play("PresentationCompletionInstructions"));
        presentationEndIcons.transform.GetChild(0).GetComponent<Button>().enabled = true;
        presentationEndIcons.transform.GetChild(1).GetComponent<Button>().enabled = true;
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
        audioReplay.interactable = true;
        if (Constants.Level4.canSkip)
        {
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            skipButton.gameObject.SetActive(false);
        }
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].gameObject.SetActive(false);
        }
        hand.gameObject.SetActive(false);
        shapeImage.gameObject.SetActive(false);
        for (int i = 0; i < tickImages.Length; i++)
        {
            tickImages[i].gameObject.SetActive(false);
        }
        mascot.MascotDisappear();
        mascotOnScreen = false;
        Tutorial = false;
        yield return oneSec;
        audioReplay.gameObject.SetActive(true);
        audioReplay.enabled = true;
        CanSelectBox = false;
        boxParent.SetActive(true);
        backButton.interactable = true;
        hintButton.interactable = true;
        numberOfCircles = 0; numberOfTriangles = 0; numberOfRectangles = 0; numberOfSquares = 0; numberOfCircles2 = 0; numberOfTriangles2 = 0; numberOfRectangles2 = 0; numberOfSquares2 = 0;
        numberOfRectangles3 = 0; numberOfSquares3 = 0;
        Debug.Log("Number Of Circles clicked: " + numberOfCircles); Debug.Log("Number Of Triangles clicked: " + numberOfTriangles); Debug.Log("Number Of Rectangles clicked: " + numberOfRectangles);
        Debug.Log("Number Of Squares clicked: " + numberOfSquares); Debug.Log("Number Of Circles2 clicked: " + numberOfCircles2); Debug.Log("Number Of Triangles2 clicked: " + numberOfTriangles2);
        Debug.Log("Number Of Rectangles2 clicked: " + numberOfRectangles2); Debug.Log("Number Of Squares2 clicked: " + numberOfSquares2); Debug.Log("Number Of Rectangles3 clicked: " + numberOfRectangles3);
        Debug.Log("Number Of Squares3 clicked: " + numberOfSquares3);
        PlayerPrefs.GetInt("SetNumber");
        SetNumberData();
    }

    public void PlayGame()
    {
        AudioManager.instance.Play("BackgroundMusic");
        skipButton.gameObject.SetActive(false);
        presentationEndIcons.SetActive(false);
        boxParent.SetActive(true);
        PlayerPrefs.GetInt("SetNumber");
        SetNumberData();
    }

    public void SetNumberData()
    {
        if (PlayerPrefs.GetInt("SetNumber") == 0)
        {
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[0].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[0].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[0].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 1)
        {
            progressImages[0].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[1].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[1].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[1].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 2)
        {
            progressImages[1].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[2].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[2].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[2].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 3)
        {
            progressImages[2].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[3].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[3].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[3].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 4)
        {
            progressImages[3].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[4].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[4].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[4].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 5)
        {
            progressImages[4].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[5].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[5].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[5].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 6)
        {
            progressImages[5].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[6].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[6].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[6].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 7)
        {
            progressImages[6].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[7].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[7].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[7].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 8)
        {
            progressImages[7].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[8].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[8].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[8].image.gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("SetNumber") == 9)
        {
            progressImages[8].gameObject.SetActive(true);
            boxParent.SetActive(true);
            audioReplay.interactable = true;
            sets[9].SetActive(true);
            for (int i = 0; i < shape.Count; i++)
            {
                for (int j = 0; j < shape[i].image.Length; j++)
                {
                    shape[9].image[j].gameObject.SetActive(true);
                }
            }
            upperShapes[9].image.gameObject.SetActive(true);
        }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        Constants.Level3.canLoadSavedScene = true;
    }

    public void ReplayAudioInstructions()
    {
        StartCoroutine(AudioInstructionsRepeatCoroutine());
    }

    private IEnumerator AudioInstructionsRepeatCoroutine()
    {
        AudioManager.instance.DecreaseBackgroundMusicVolume();
        CanSelectBox = false;
        yield return new WaitForSeconds(mascot.PlayClip(1));
        CanSelectBox = true;
        AudioManager.instance.IncreaseBackgroundMusicVolume();
    }

    // Update is called once per frame
    void Update()
    {
		if (onoff)
		{
            speechBubbleImage.gameObject.SetActive(true);
            speechDotsImage.gameObject.SetActive(true);
		}
		else
		{
            speechBubbleImage.gameObject.SetActive(false);
            speechDotsImage.gameObject.SetActive(false);
        }
    }

    public void ReplayLevel()
    {
        levelCompletedIcones.SetActive(false);
        musicButton.image.sprite = buttonImage;
        AudioManager.instance.Stop("BackgroundMusic");
        AudioManager.instance.Play("BackgroundMusic");
        audioReplay.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(true);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipButtonClicked);
        hand.DOAnchorPos(new Vector2(0f, 0f), 0f);
        numberOfCircles = 0; numberOfTriangles = 0; numberOfRectangles = 0; numberOfSquares = 0; numberOfCircles2 = 0; numberOfTriangles2 = 0; numberOfRectangles2 = 0; numberOfSquares2 = 0;
        numberOfRectangles3 = 0; numberOfSquares3 = 0;
        confetti.SetActive(false);
        boxParent.SetActive(true);
        sets[0].SetActive(true);
        upperShapes[0].image.gameObject.SetActive(true);
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[0].image[j].gameObject.SetActive(true);
                shape[i].image[j].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        StartCoroutine(StartOver());
    }

    IEnumerator StartOver()
    {
        yield return new WaitForSeconds(mascot.PlayClip(1));
        for (int i = 0; i < shape.Count; i++)
        {
            for (int j = 0; j < shape[i].image.Length; j++)
            {
                shape[i].image[j].gameObject.GetComponent<Button>().enabled = true;
            }
        }
    }

    private void KillAllTweens()
    {
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
        SceneLoader.instance.LoadNextLevel(Constants.Level2.nextLevel);
        AudioManager.instance.Play("BackgroundMusic");
    }

    IEnumerator PlayCorrectAnswer()
	{
        Panel.SetActive(true);
        yield return new WaitForSeconds(AudioManager.instance.Play("CorrectAnswer", replay: true));
        Panel.SetActive(false);
    }

}
