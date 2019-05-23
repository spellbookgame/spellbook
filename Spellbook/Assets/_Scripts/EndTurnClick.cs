﻿using UnityEngine;

public class EndTurnClick : MonoBehaviour
{
    public void OnEndTurnClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Player localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        bool endSuccessful = localPlayer.onEndTurnClick();
        if (endSuccessful)
        {
            // reset player attribute values
            localPlayer.Spellcaster.hasAttacked = false;
            localPlayer.Spellcaster.hasRolled = false;
            localPlayer.Spellcaster.scannedSpaceThisTurn = false;
            localPlayer.Spellcaster.numSpellsCastThisTurn = 0;
            UICanvasHandler.instance.spacesMoved = 0;
            SpellTracker.instance.agendaActive = false;

            // reset consequence bools
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - localPlayer.Spellcaster.tsunamiConsTurn >= 1)
                localPlayer.Spellcaster.tsunamiConsequence = false;
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - localPlayer.Spellcaster.cometConsTurn >= 1)
                localPlayer.Spellcaster.cometConsequence = false;

            // collect end of turn mana
            int manaCollected = localPlayer.Spellcaster.CollectManaEndTurn();
            GameObject.Find("ScriptContainer").GetComponent<MainPageHandler>().DisplayMana(manaCollected);

            // close Dice tray if it's open
            if(GameObject.Find("Dice Tray"))
                GameObject.Find("Dice Tray").GetComponent<DiceUIHandler>().OpenCloseDiceTray();

            // disable buttons
            UICanvasHandler.instance.ActivateEndTurnButton(false);
            UICanvasHandler.instance.EnableDiceButton(false);
        }
    }
}
