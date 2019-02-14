﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnClick : MonoBehaviour
{
    public SceneScript sceneScript;
    public void OnEndTurnClick()
    {
        //GameObject player = GameObject.Find("LocalPlayer(Clone)");
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");
        player.GetComponent<Player>().onEndTurnClick();
        sceneScript.loadMainScene();
    }
}
