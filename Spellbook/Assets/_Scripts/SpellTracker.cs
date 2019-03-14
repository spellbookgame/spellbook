﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Grace Ko
/// singleton used to track player's spells
/// instantiated in MainPageHandler.setUpMainPage();
/// </summary>
public class SpellTracker : MonoBehaviour
{
    public static SpellTracker instance = null;

    private SpellCaster spellCaster;

    void Awake()
    {
        //Check if there is already an instance of SpellTracker
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of QuestTracker.
            Destroy(gameObject);

        //Set QuestTracker to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        spellCaster = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster;
    }

    // checks if any spells affect mana, calculates accordingly, and sets panel text
    public int CheckManaSpell(int manaCount)
    {
        StringBuilder sb = new StringBuilder();
        
        // if player's active spells list contains Crystal Scent or Arcana Harvest
        if (spellCaster.activeSpells.Any(x => x.sSpellName == "Brew - Crystal Scent" || x.sSpellName == "Arcana Harvest"))
        {
            // if Crystal Scent is active, + 20% mana
            if (spellCaster.activeSpells.Any(x => x.sSpellName == "Brew - Crystal Scent"))
            {
                manaCount += (int)(manaCount * 0.2);
                sb.Append(" Brew - Crystal Scent ");
            }
            // if Arcana Harvest is active, double mana
            if (spellCaster.activeSpells.Any(x => x.sSpellName == "Arcana Harvest"))
            {
                manaCount *= 2;
                sb.Append(" Arcana Harvest ");
            }

            PanelHolder.instance.displayEvent("You found Mana!", sb.ToString() + " is active and you earned " + manaCount + " mana.");
            return manaCount;
        }
        else
        {
            PanelHolder.instance.displayEvent("You found Mana!", "You earned " + manaCount + " mana.");
            return manaCount;
        }
    }

    public int CheckGlyphSpell(string glyph)
    {
        int glyphCount = 1;

        // add an additional glyph to player's inventory if arcana harvest is active
        if(spellCaster.activeSpells.Any(x => x.sSpellName == "Arcana Harvest"))
        {
            glyphCount = 2;
            PanelHolder.instance.displayEvent("You found a Glyph!", "Arcana Harvest is active and you earned 2 " + glyph + "s.");
            return glyphCount;
        }
        else
        {
            PanelHolder.instance.displayEvent("You found a Glyph!", "You found 1 " + glyph + ".");
            return glyphCount;
        }
    }

    // referenced in DiceRoll.cs
    public string CheckMoveSpell()
    {
        if(spellCaster.activeSpells.Any(x => x.sSpellName == "Accelerate"))
        {
            return "Accelerate";
        }
        else if(spellCaster.activeSpells.Any(x => x.sSpellName == "Teleport"))
        {
            return "Teleport";
        }
        else
        {
            return "";
        }
    }
}