using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopupController : MonoBehaviour {

	// PROPERTIES :
	[SerializeField] private Canvas popupCanvas;
	[SerializeField] private Text timeLabel;
	[SerializeField] private Text scoreLabel;


	public void ShowEndGamePopup(float time, int score) {

		String formatedTime = (int) time / 60 + "' " + Mathf.Floor(time) % 60 + "\" " + Mathf.Floor((time - Mathf.Floor(time)) * 100f);

		timeLabel.text = String.Format(timeLabel.text, formatedTime);
		scoreLabel.text = String.Format(scoreLabel.text, score);

		popupCanvas.gameObject.SetActive(true);

		Debug.Log("Popup displayed.");
	}

	private void Update() {
		if(LeanTouch.Fingers.Count >= 1 && popupCanvas.gameObject.activeSelf) {

			Debug.Log("Reseting the game.");
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}
