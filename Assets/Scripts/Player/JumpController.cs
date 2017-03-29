﻿using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class JumpController : MonoBehaviour {

	// CONSTANTS :
	private Vector2 jumpForce = new Vector2(0, 450f);
	private float DELAY_BETWEEN_JUMPS = 0.2f;

	// PROPERTIES :
	private LayerMask layerMask;
	private bool jumpRegistered;
	private JellySprite jellySprite;
	private float lastJumpTime;

	private void Start() {
		jellySprite = GetComponent<JellySprite>();
		layerMask = LayerMask.GetMask("Ground");
	}

	// Update is called once per frame
	private void Update () {

		if(AppData.GetCurrentTiltType() == AppData.GroundTiltType.None)
			return;

#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Space) && jellySprite.IsGrounded(layerMask, 1)) {
#else
		if(jellySprite.IsGrounded(layerMask, 2) && 
			((AppData.GetCurrentTiltType() == AppData.GroundTiltType.Touch && LeanTouch.Fingers.Count >= 2) || (AppData.GetCurrentTiltType() == AppData.GroundTiltType.Gyro && LeanTouch.Fingers.Count >= 1))) {
#endif

			jumpRegistered = true;
		}
	}

	private void FixedUpdate() {
		if(jumpRegistered) {

			if(Time.time - lastJumpTime > DELAY_BETWEEN_JUMPS) {
				float groundAngle = GroundTiltController.GetEulerAngleZ();
				jumpForce.x = groundAngle <= 180f ? groundAngle * -4 : (360f - groundAngle) * 4f;
				jellySprite.AddForce(jumpForce);

				lastJumpTime = Time.time;
			}

			jumpRegistered = false;
		}
	}
}
