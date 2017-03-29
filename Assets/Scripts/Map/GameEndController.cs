using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndController : MonoBehaviour {

	// CONSTANTS :
	private float DELAY_TO_FINISH_GAME_SECONDS = 5f;

	// PROPERTIES :
	[SerializeField] private GameObject playerGameObject;
	[SerializeField] private GameObject App;
	[SerializeField] private Canvas preloaderCanvas;
	[SerializeField] private Image preloaderImage;

	private bool isPlayerInTheZone;
	private CircleCollider2D collider;


	private void Awake() {
		collider = GetComponent<CircleCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject == playerGameObject && !isPlayerInTheZone) {
			Debug.Log("Player entered the finish zone.");
			isPlayerInTheZone = true;
			ShowPreloader();
			StartCoroutine(CountdownToTheFinish());
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject == playerGameObject && isPlayerInTheZone) {
			Debug.Log("Player left the finish zone.");
			isPlayerInTheZone = false;
			StopAllCoroutines();
			HidePreloader();
		}
	}

	private IEnumerator CountdownToTheFinish() {

		float currentElapsedTime = 0f;

		while(currentElapsedTime <= DELAY_TO_FINISH_GAME_SECONDS) {
			currentElapsedTime += Time.deltaTime;
			preloaderImage.fillAmount = currentElapsedTime / DELAY_TO_FINISH_GAME_SECONDS;
			yield return null;
		}
		Debug.Log("Game Finished.");
		HidePreloader();

		Debug.Log("There are " + GetLittleEmojisAmount() + " little emojis with the player.");
		Debug.Log("The player took " + GetGameDuration() + " seconds to complete the level.");

		App.GetComponent<EndGamePopupController>().ShowEndGamePopup(GetGameDuration(), GetLittleEmojisAmount());
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
		return Time.time - AppData.GetStartTime();
	}





	// PRELOADER :
	private void ShowPreloader() {
		preloaderCanvas.gameObject.SetActive(true);
		preloaderImage.fillAmount = 0f;
	}

	private void HidePreloader() {
		preloaderCanvas.gameObject.SetActive(false);
	}
}
