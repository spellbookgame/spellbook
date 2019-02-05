﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public int iTier;
    public int iManaCost;   // cost in mana crystals
    public string sSpellName;
    public string sSpellClass;

    public Dictionary<string, int> requiredPieces;

    // CTOR
    public Spell()
    {
        requiredPieces = new Dictionary<string, int>();
    }

    // Virtual functions
    public abstract void SpellCast(SpellCaster player);
}
