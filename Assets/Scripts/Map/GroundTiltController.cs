using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundTiltController : MonoBehaviour {

	// CONSTANTS :
	private const float TURN_RANGE = 0.8f;
	private const float DEADZONE_RANGE = 0.2f;
	private const float MAX_GROUND_TILT_DEGREE = 0.30f;
	private const float ROTATION_SPEED = 180f;

	// PROPERTIES :
	private float tiltDirection;
	private Quaternion turnRotation;
	[SerializeField] private GameObject player;


	private void Awake() {
		// Initializes the gyro on device.
		if(SystemInfo.supportsGyroscope) {
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 0.0167f;		// -> gyro updates at 60Hz
		}
	}

#if UNITY_EDITOR
	private void Update () {
		tiltDirection = -Input.GetAxis("Horizontal");
	}
#endif

	private void FixedUpdate() {
#if UNITY_EDITOR
		if(!tiltDirection.Equals(0f)) {
			turnRotation = transform.rotation * Quaternion.Euler(0f, 0f, tiltDirection * ROTATION_SPEED * Time.deltaTime);

			turnRotation.z = Mathf.Clamp(turnRotation.z, -MAX_GROUND_TILT_DEGREE, MAX_GROUND_TILT_DEGREE);
			Quaternion targetRotation = Quaternion.Lerp(transform.rotation, turnRotation, 20f * Time.deltaTime);


			float angle = targetRotation.eulerAngles.z - transform.rotation.eulerAngles.z;
			transform.RotateAround(player.transform.position, Vector3.forward, angle);

			tiltDirection = 0;


		}

#else

		float angle = GetTiltFactorForInput() * -30f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(
			0f,
			0f,
			angle
		));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
#endif
	}


	///////////////////
	// ACCELEROMETER //
	///////////////////

	public float GetTiltFactorForInput() {
		float accel = Input.acceleration.x;

		if(accel < -TURN_RANGE / 2f)
			return -1f;
		if(accel < -DEADZONE_RANGE / 2f)
			return -GetRangePercent(accel, -DEADZONE_RANGE / 2f, -TURN_RANGE / 2f);
		if(accel < DEADZONE_RANGE / 2f)
			return 0f;
		if(accel <= TURN_RANGE / 2f)
			return GetRangePercent(accel, DEADZONE_RANGE / 2f, TURN_RANGE / 2f);

		return 1f;
	}

	private float GetRangePercent(float v, float min, float max) {
		return (v - min) / (max - min);
	}
}
