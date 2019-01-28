﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    public int iTier;
    public int iCost;   // cost in mana crystals
    public string sSpellName;
    public string sSpellClass;

    public HashSet<string> requiredPieces;

    // CTOR
    public Spell()
    {
        // requiredPieces must ALL have different values in order for HashSet to work
        requiredPieces = new HashSet<string>();
    }

    // Virtual functions
    public abstract void SpellCast();
}
