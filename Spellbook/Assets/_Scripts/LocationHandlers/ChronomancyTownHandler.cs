﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChronomancyTownHandler : MonoBehaviour
{
    [SerializeField] private Button findQuestButton;
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button pickupItemButton;
    [SerializeField] private Button leaveButton;

    private Quest[] quests;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        quests = new Quest[]
        {
            new TimeMoveQuest(localPlayer.Spellcaster.NumOfTurnsSoFar),
            new TimeLocationQuest(localPlayer.Spellcaster.NumOfTurnsSoFar),
            new TimeShoppingQuest(localPlayer.Spellcaster.NumOfTurnsSoFar)
        };

        findQuestButton.onClick.AddListener(FindQuest);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("town_chronomancer");
        QuestTracker.instance.TrackErrandQuest("town_chronomancer");
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        bool hasQuestInThiSTown = false;

        // check if player has quest from this town
        foreach (Quest q in quests)
        {
            if (QuestTracker.instance.HasQuest(q))
            {
                hasQuestInThiSTown = true;
                break;
            }
        }
        if (hasQuestInThiSTown)
        {
            PanelHolder.instance.displayNotify("Meridea", "You're already on a quest for this town.", "OK");
        }
        else
        {
            int r = Random.Range(0, quests.Length);
            PanelHolder.instance.displayQuest(quests[r]);
        }
    }
}
