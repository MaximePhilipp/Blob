using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

	// CONSTANTS :
	private Vector2 jumpForce = new Vector2(0, 300f);

	// PROPERTIES :
	private LayerMask layerMask;
	private bool jumpRegistered;
	private JellySprite jellySprite;

	private void Start() {
		jellySprite = GetComponent<JellySprite>();
		layerMask = LayerMask.GetMask("Ground");
	}

	// Update is called once per frame
	private void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && jellySprite.IsGrounded(layerMask, 1)) {
			jumpRegistered = true;
			Debug.Log("Jump registered");
		}
	}

	private void FixedUpdate() {
		if(jumpRegistered) {
			jellySprite.AddForce(jumpForce);

			jumpRegistered = false;
			Debug.Log("Performing jump.");
		}
	}
}
