﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// script from Kiwasi Games
public class SlotHandler : MonoBehaviour, IDropHandler
{
    SpellManager spellManager;
    Player localPlayer;

    public GameObject item
    {
        get
        {
            // if it has a child, return the first child
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    void Start()
    {
        spellManager = GameObject.Find("Canvas").GetComponent<SpellManager>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
    }

    void Update()
    {
        if(transform.parent.name.Equals("panel_spellpieces"))
        {
            // updating the text for each spell piece
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.dspellPieces[transform.GetChild(0).name].ToString();

            // disable/enable DragHandler script
            if(localPlayer.Spellcaster.dspellPieces[transform.GetChild(0).name] <= 0)
            {
                transform.GetChild(0).GetComponent<DragHandler>().enabled = false;
            }
            else
            {
                transform.GetChild(0).GetComponent<DragHandler>().enabled = true;
            }
        }
    }

    // happens before OnEndDrag in DragHandler.cs
    public void OnDrop(PointerEventData eventData)
    {
        // if the slot contains an item and it is a child of the spell pieces panel, destroy itemToDrag
        if (item && transform.parent.name.Equals("panel_spellpieces"))
        {
            // add the spell piece back to player's inventory
            localPlayer.Spellcaster.dspellPieces[DragHandler.itemToDrag.name] += 1;

            // remove spell piece from the hashset
            spellManager.hashSpellPieces.Remove(DragHandler.itemToDrag.name);
            Debug.Log("Removed " + DragHandler.itemToDrag.name + ". Hash Count is now: " + spellManager.hashSpellPieces.Count);

            Destroy(DragHandler.itemToDrag.gameObject);

            // using lambda function to call HasChanged method in SpellManager.cs
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }

        // if the slot has no item, then allow item to be dragged in
        if (!item)
        {
            // set item being dragged's parent to current slot's transform
            DragHandler.itemToDrag.transform.SetParent(transform);

            // using lambda function to call HasChanged method in SpellManager.cs
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
    }
}
