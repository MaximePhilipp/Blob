using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

	private Vector2 jumpForce = new Vector2(0, 300f);

	public float m_MinJumpForce = 10.0f;
	public float m_MaxJumpForce = 10.0f;
	public Vector2 m_MinJumpVector = new Vector2(-0.1f, 1.0f);
	public Vector2 m_MaxJumpVector = new Vector2(0.1f, 1.0f);



	// PROPERTIES :
	private bool jumpRegistered;
	private JellySprite jellySprite;

	private void Start() {
		jellySprite = GetComponent<JellySprite>();
	}

	// Update is called once per frame
	private void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
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
