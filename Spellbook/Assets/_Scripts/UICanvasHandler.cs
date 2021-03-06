﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvasHandler : MonoBehaviour
{
    public static UICanvasHandler instance = null;

    // public variables
    public int spacesMoved = 0;     // reset on EndTurn click
    public bool chronomancerGone;   // ensures the fade transition only happens at beginning of game
    public bool initialSoundsSet;   // only set separate BGMs at start of game

    #region private_fields
    [SerializeField] private GameObject spellbookButton;
    [SerializeField] private GameObject diceButton;
    [SerializeField] private GameObject scanButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private GameObject spellbookMainButton;
    [SerializeField] private GameObject libraryButton;
    [SerializeField] private GameObject questButton;
    [SerializeField] private GameObject progressButton;
    [SerializeField] private GameObject movePanel;
    [SerializeField] private DiceUIHandler diceUIHandler;
    [SerializeField] private GameObject combatButton;

    private TutorialHandler tutorialHandler;
    #endregion

    void Awake()
    {
        //Check if there is already an instance of UICanvasHandler
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of UICanvasHandler.
            Destroy(gameObject);

        //Set UICanvasHandler to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    // called once when UICanvasHandler is instantiated
    private void Start()
    {
        #region onClickListeners
        AudioClip[] pageTurnSounds = new AudioClip[]
        {
            SoundManager.pageTurn1,
            SoundManager.pageTurn2,
            SoundManager.pageTurn3
        };
        // set onclick listeners once in the game
        spellbookMainButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(pageTurnSounds[Random.Range(0, pageTurnSounds.Length)]);
            SceneManager.LoadScene("SpellbookScene");
        });
        libraryButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(pageTurnSounds[Random.Range(0, pageTurnSounds.Length)]);
            SceneManager.LoadScene("LibraryScene");
        });
        questButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(pageTurnSounds[Random.Range(0, pageTurnSounds.Length)]);
            SceneManager.LoadScene("QuestLogScene");
        });
        progressButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(pageTurnSounds[Random.Range(0, pageTurnSounds.Length)]);
            SceneManager.LoadScene("SpellbookProgress");
        });

        // set onclick listeners for main scene buttons
        spellbookButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookopen);
            ActivateSpellbookButtons(true);
            SceneManager.LoadScene("SpellbookScene");
        });
        inventoryButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.inventoryOpen);
            SceneManager.LoadScene("InventoryScene");
        });
        scanButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.selectScan);
            LoadHandler.instance.sceneBuildIndex = 3;
            SceneManager.LoadScene("LoadingScene");
            // SceneManager.LoadScene("VuforiaScene");
        });
        #endregion

        tutorialHandler = GameObject.Find("tutorialHandler").GetComponent<TutorialHandler>();

        // initially position the buttons properly on main player scene
        spellbookButton.transform.localPosition = new Vector3(-530, -1060, 0);
        diceButton.transform.localPosition = new Vector3(-180, -1060, 0);
        scanButton.transform.localPosition = new Vector3(180, -1060, 0);
        inventoryButton.transform.localPosition = new Vector3(530, -1060, 0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // set render camera to main camera
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // if we're not in main scene
        if (!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
        {
            // if dice tray is open but moved scenes, close dice tray
            if(diceUIHandler.diceTrayOpen)
            {
                diceUIHandler.OpenCloseDiceTray();
            }

            // deactive main buttons
            spellbookButton.SetActive(false);
            diceButton.SetActive(false);
            inventoryButton.SetActive(false);
            endTurnButton.SetActive(false);
            scanButton.SetActive(false);
        }
        // if we're in the main scene
        else
        {
            spellbookButton.SetActive(true);
            diceButton.SetActive(true);
            inventoryButton.SetActive(true);
            scanButton.SetActive(true);
        }
    }

    // activate end turn button if player has rolled
    public void ActivateEndTurnButton(bool enabled)
    {
        endTurnButton.SetActive(enabled);
    }

    // enable dice/scan button if it's player's turn
    public void EnableDiceButton(bool enabled)
    {
        diceButton.GetComponent<Button>().interactable = enabled;
        diceButton.transform.GetChild(0).gameObject.SetActive(enabled);

        scanButton.GetComponent<Button>().interactable = enabled;
        scanButton.transform.GetChild(0).gameObject.SetActive(enabled);
    }

    // set the spellbook buttons active if in spellbook scene
    public void ActivateSpellbookButtons(bool enabled)
    {
        spellbookMainButton.SetActive(enabled);
        spellbookMainButton.GetComponent<UIButtonScale>().ScaleUp();    // to show we're in the first page
        libraryButton.SetActive(enabled);
        questButton.SetActive(enabled);
        progressButton.SetActive(enabled);
    }

    public void EnableMainSceneButtons(bool enabled)
    {
        spellbookButton.GetComponent<Button>().interactable = enabled;
        diceButton.GetComponent<Button>().interactable = enabled;
        scanButton.GetComponent<Button>().interactable = enabled;
        inventoryButton.GetComponent<Button>().interactable = enabled;

        // for the glow backgrounds
        spellbookButton.transform.GetChild(0).gameObject.SetActive(enabled);
        diceButton.transform.GetChild(0).gameObject.SetActive(enabled);
        scanButton.transform.GetChild(0).gameObject.SetActive(enabled);
        inventoryButton.transform.GetChild(0).gameObject.SetActive(enabled);
    }

    public void ShowMovePanel()
    {
        StartCoroutine(StartMovePanel());
    }

    private IEnumerator StartMovePanel()
    {
        yield return new WaitForSeconds(1f);
        // close dice tray
        if(diceUIHandler.diceTrayOpen)
            diceUIHandler.OpenCloseDiceTray();
        // set move text
        movePanel.transform.GetChild(0).GetComponent<Text>().text = "Move " + spacesMoved.ToString();
        movePanel.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        movePanel.SetActive(false);
    }

    //Temporary
    public void Combat()
    {
        combatButton.SetActive(false);
        SceneManager.LoadScene("CombatSceneV2");
    }

    public void ShowTutorialPrompt()
    {
        tutorialHandler.GetComponent<TutorialHandler>().PromptTutorial();
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.mainTutorialShown = true;
    }
}
