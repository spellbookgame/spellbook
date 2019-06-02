﻿using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    public string currentBGM;

    // static clip so any script can access it
    [SerializeField] AudioClip[] soundeffects = new AudioClip[21];
    [SerializeField] AudioClip[] bgms = new AudioClip[1];

    #region static_clips
    public static AudioClip buttonconfirm;
    public static AudioClip characterswipe;
    public static AudioClip spellbookClose;
    public static AudioClip diceroll;
    public static AudioClip dicetrayopen;
    public static AudioClip glyphfound;
    public static AudioClip grabspellpiece;
    public static AudioClip itemfound;
    public static AudioClip dicePickUp;
    public static AudioClip dicePlace;
    public static AudioClip pageTurn1;
    public static AudioClip placespellpiece;
    public static AudioClip questaccept;
    public static AudioClip questfailed;
    public static AudioClip questsuccess;
    public static AudioClip spellbookopen;
    public static AudioClip spellcast;
    public static AudioClip spellcreate;
    public static AudioClip yourturn;
    public static AudioClip inventoryClose;
    public static AudioClip inventoryOpen;
    public static AudioClip parchmentBurn;
    public static AudioClip manaCollect;
    public static AudioClip purchase;
    public static AudioClip combatCollectRewards;
    public static AudioClip combatDefeat;
    public static AudioClip combatDrawingSound;
    public static AudioClip orbFilling;
    public static AudioClip orbFull;
    public static AudioClip combatVictory;
    public static AudioClip crisisAverted;
    public static AudioClip crisisLost;
    public static AudioClip crisisNotification;
    public static AudioClip abyssalOre;
    public static AudioClip aromaticTeaLeaves;
    public static AudioClip crystalMirror;
    public static AudioClip glimmeringCabochon;
    public static AudioClip hollowCabochon;
    public static AudioClip mimeticVellum;
    public static AudioClip mysticTranslocator;
    public static AudioClip opalAmmonite;
    public static AudioClip riftTalisman;
    public static AudioClip infusedSapphire;
    public static AudioClip waxCandle;
    public static AudioClip glowingMushroom;
    public static AudioClip spaceScan;
    public static AudioClip pageTurn2;
    public static AudioClip pageTurn3;
    public static AudioClip tick;
    #endregion

    #region bgms
    //Music
    public static AudioClip lobby;
    public static AudioClip gameBCG;
    public static AudioClip andromedaBGM;
    public static AudioClip minesBGM;
    public static AudioClip forestBGM;
    public static AudioClip merideaBGM;
    public static AudioClip paradosBGM;
    public static AudioClip regulusBGM;
    public static AudioClip sarissaBGM;
    public static AudioClip swampBGM;
    public static AudioClip zandriaBGM;
    public static AudioClip gameOverBGM;
    public static AudioClip combatBGM;
    public static AudioClip marketBGM;
    #endregion

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        // assigning sounds to static audio clips
        #region assign_sounds
        buttonconfirm = soundeffects[0];
        characterswipe = soundeffects[1];
        spellbookClose = soundeffects[2];
        diceroll = soundeffects[3];
        dicetrayopen = soundeffects[4];
        glyphfound = soundeffects[5];
        grabspellpiece = soundeffects[6];
        itemfound = soundeffects[7];
        dicePickUp = soundeffects[8];
        dicePlace = soundeffects[9];
        pageTurn1 = soundeffects[10];
        placespellpiece = soundeffects[11];
        questaccept = soundeffects[12];
        questfailed = soundeffects[13];
        questsuccess = soundeffects[14];
        spellbookopen = soundeffects[15];
        spellcast = soundeffects[16];
        spellcreate = soundeffects[17];
        yourturn = soundeffects[18];
        pageTurn2 = soundeffects[19];
        pageTurn3 = soundeffects[20];
        inventoryClose = soundeffects[21];
        inventoryOpen = soundeffects[22];
        parchmentBurn = soundeffects[23];
        manaCollect = soundeffects[24];
        purchase = soundeffects[25];
        combatCollectRewards = soundeffects[26];
        combatDefeat = soundeffects[27];
        combatDrawingSound = soundeffects[28];
        orbFilling = soundeffects[29];
        orbFull = soundeffects[30];
        combatVictory = soundeffects[31];
        crisisAverted = soundeffects[32];
        crisisLost = soundeffects[33];
        crisisNotification = soundeffects[34];
        abyssalOre = soundeffects[35];
        aromaticTeaLeaves = soundeffects[36];
        crystalMirror = soundeffects[37];
        glimmeringCabochon = soundeffects[38];
        hollowCabochon = soundeffects[39];
        mimeticVellum = soundeffects[40];
        mysticTranslocator = soundeffects[41];
        opalAmmonite = soundeffects[42];
        riftTalisman = soundeffects[43];
        infusedSapphire = soundeffects[44];
        waxCandle = soundeffects[45];
        glowingMushroom = soundeffects[46];
        spaceScan = soundeffects[47];
        tick = soundeffects[48];
        #endregion

        #region assign_bgms
        lobby = bgms[0];
        gameBCG = bgms[1];
        andromedaBGM = bgms[2];
        minesBGM = bgms[3];
        forestBGM = bgms[4];
        merideaBGM = bgms[5];
        paradosBGM = bgms[6];
        regulusBGM = bgms[7];
        sarissaBGM = bgms[8];
        swampBGM = bgms[9];
        zandriaBGM = bgms[10];
        gameOverBGM = bgms[11];
        combatBGM = bgms[12];
        marketBGM = bgms[13];
        #endregion

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }

    public void PlayGameBCM(AudioClip au)
    {
        musicSource.clip = au;
        musicSource.Play();
        currentBGM = au.name;
    }
}