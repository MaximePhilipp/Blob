using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTiltController : MonoBehaviour {

	// CONSTANTS :
	private const float TURN_RANGE = 0.95f;
	private const float DEADZONE_RANGE = 0.05f;
	private const float MAX_GROUND_TILT_DEGREE = 0.30f;
	private const float ROTATION_SPEED = 180f;

	// PROPERTIES :
	private float tiltDirection;
	private Quaternion turnRotation;
	[SerializeField] private GameObject player;

	private static GroundTiltController instance;

	private void Awake() {
		instance = this;

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

		Quaternion targetRotation = Quaternion.identity;

#if UNITY_EDITOR
		if(tiltDirection.Equals(0f))
			return;


		turnRotation = transform.rotation * Quaternion.Euler(0f, 0f, tiltDirection * ROTATION_SPEED * Time.deltaTime);

		turnRotation.z = Mathf.Clamp(turnRotation.z, -MAX_GROUND_TILT_DEGREE, MAX_GROUND_TILT_DEGREE);
		targetRotation = Quaternion.Lerp(transform.rotation, turnRotation, 20f * Time.deltaTime);

		tiltDirection = 0;



#else

		Quaternion turnRotation = Quaternion.Euler(new Vector3(
			0f,
			0f,
			GetTiltFactorForInput() * MAX_GROUND_TILT_DEGREE * (-100)
		));

		targetRotation = Quaternion.Lerp(transform.rotation, turnRotation, 10f * Time.deltaTime);

#endif

		float angle = targetRotation.eulerAngles.z - transform.rotation.eulerAngles.z;
		transform.RotateAround(player.transform.position, Vector3.forward, angle);
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

	public static float GetEulerAngleZ() {
		return instance.transform.rotation.eulerAngles.z;
	}
}
