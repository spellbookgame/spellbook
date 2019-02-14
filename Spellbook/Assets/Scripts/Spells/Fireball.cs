﻿using UnityEngine;
using UnityEngine.SceneManagement;

// example spell for Elementalist class
public class Fireball : Spell
{
    public Fireball()
    {
        sSpellName = "Fireball";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Elementalist";

        requiredPieces.Add("Elemental D Spell Piece", 1);

        requiredGlyphs.Add("Elemental D Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Elemental D Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            int damage = Random.Range(2, 12);
            enemy.HitEnemy(damage);

            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Elemental D Glyph"] -= 4;

            if (enemy.fCurrentHealth > 0)
            {
                SceneManager.LoadScene("CombatScene");
            }
        } 
    }
}
