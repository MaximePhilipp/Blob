using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndController : MonoBehaviour {

	// CONSTANTS :
	private float DELAY_TO_FINISH_GAME_SECONDS = 5f;

	// PROPERTIES :
	[SerializeField] private GameObject playerGameObject;
	private bool isPlayerInTheZone;


	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject == playerGameObject && !isPlayerInTheZone) {
			Debug.Log("Player entered the finish zone.");
			isPlayerInTheZone = true;
			StartCoroutine(CountdownToTheFinish());
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject == playerGameObject && isPlayerInTheZone) {
			Debug.Log("Player left the finish zone.");
			isPlayerInTheZone = false;
			StopAllCoroutines();
		}
	}

	private IEnumerator CountdownToTheFinish() {
		yield return new WaitForSeconds(DELAY_TO_FINISH_GAME_SECONDS);
		Debug.Log("Game Finished.");

		// TODO : Reset on the click on the result
		Application.LoadLevel (Application.loadedLevelName);
	}
}
