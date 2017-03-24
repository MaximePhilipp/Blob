using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndController : MonoBehaviour {

	// CONSTANTS :
	private float DELAY_TO_FINISH_GAME_SECONDS = 5f;

	// PROPERTIES :
	[SerializeField] private GameObject playerGameObject;
	private bool isPlayerInTheZone;
	private CircleCollider2D collider;
	private float startingTime;


	private void Awake() {
		collider = GetComponent<CircleCollider2D>();
		startingTime = Time.time;
	}

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

		Debug.Log("There are " + GetLittleEmojisAmount() + " little emojis with the player.");
		Debug.Log("The player took " + GetGameDuration() + " seconds to complete the level.");

		// TODO : Reset on the click on the result
		Application.LoadLevel (Application.loadedLevelName);
	}


	private int GetLittleEmojisAmount() {
		GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("Player");
		int playersInTheZone = 0;

		foreach(GameObject player in playersInGame) {
			if(collider.OverlapPoint(player.transform.position))
				playersInTheZone++;
		}

		Debug.Log("There are " + playersInTheZone + " players in the zone.");

		return playersInTheZone - 1;	// -> removing the big player from the process
	}

	private float GetGameDuration() {
		return Time.time - startingTime;
	}
}
