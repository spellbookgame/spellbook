﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Helper Script for managing Unity events on selection and deselection.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Spring 2019
/// </summary>
public class UISelectionHelper : MonoBehaviour {

	// Public Fields
	[Header("Configuration")]
	public bool selectOnStart;
	public bool deselectOnStart;

	[Header("Events")]
	public OnSelectedEvent OnSelected;
	public OnSelectedEvent OnDeselected;

	public void Start() {
		if (selectOnStart) {
			DoSelectionActions();
		}
		if (deselectOnStart) {
			DoDeselectionActions();
		}
	}

	public void Update() {

	}

	public void DoSelectionActions() {
		OnSelected.Invoke();
	}

	public void DoDeselectionActions() {
		OnDeselected.Invoke();
	}
}

[System.Serializable]
public class OnSelectedEvent : UnityEvent { }