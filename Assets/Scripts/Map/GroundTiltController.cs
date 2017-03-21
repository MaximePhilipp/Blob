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
			transform.rotation = Quaternion.Lerp(transform.rotation, turnRotation, 40f * Time.deltaTime);

			tiltDirection = 0;
		}

#else

		float angle = GetTiltFactorForInput() * -30f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(
			0f,
			0f,
			angle
		));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 4f * Time.deltaTime);
#endif
	}



	private const float TURN_RANGE = 0.8f;
	private const float DEADZONE_RANGE = 0.2f;

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
