using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("An emoji bubble has been popped. Freeing the emoji inside.");

		JellySprite jellySprite = GetComponent<JellySprite>();
		jellySprite.transform.parent = transform.parent;
		jellySprite.m_GravityScale = 1.0f;

		Destroy(this);
	}
}
