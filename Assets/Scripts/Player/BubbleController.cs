using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

	// PROPERTIES :
	[SerializeField] private GameObject emojiPrefab;

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("An emoji bubble has been popped. Freeing the emoji inside.");

		GameObject emoji = Instantiate(emojiPrefab, transform.parent);
		emoji.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

		gameObject.SetActive(false);
		Destroy(this);
	}
}
