using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTiltController : MonoBehaviour {

	private const float MAX_GROUND_TILT_DEGREE = 0.30f;
	private const int TILT_STEP = 15;
	private const float ROTATION_SPEED = 80f;

	// PROPERTIES :
	private float tiltDirection;
	private Quaternion turnRotation;
	
	// Update is called once per frame
	private void Update () {
		tiltDirection = -Input.GetAxis("Horizontal");
	}

	private void FixedUpdate() {
		if(!tiltDirection.Equals(0f)) {
			turnRotation = transform.rotation * Quaternion.Euler(0f, 0f, tiltDirection * ROTATION_SPEED * Time.deltaTime);

			Debug.Log("Turn rotation = " + turnRotation.z);
			turnRotation.z = Mathf.Clamp(turnRotation.z, -MAX_GROUND_TILT_DEGREE, MAX_GROUND_TILT_DEGREE);
			Debug.Log("Clamp rotation = " + turnRotation.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, turnRotation, 40f * Time.deltaTime);

			tiltDirection = 0;
		}
	}
}
