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

	private List<JellySprite> registeredJellySprites;

	private static GroundTiltController instance;


	////////////////
	// STATIC API //
	////////////////

	public static float GetEulerAngleZ() {
		return instance.transform.rotation.eulerAngles.z;
	}

	public static void RegisterJellySprite(JellySprite jellySprite) {
		instance.registeredJellySprites.Add(jellySprite);
	}



	///////////////
	// LIFECYCLE //
	///////////////

	private void Awake() {
		instance = this;

		// Initializes the gyro on device.
		if(SystemInfo.supportsGyroscope) {
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 0.0167f;		// -> gyro updates at 60Hz
		}

		registeredJellySprites = new List<JellySprite>();
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

		if(turnRotation.z.Equals(0))
			return;

		targetRotation = Quaternion.Lerp(transform.rotation, turnRotation, 10f * Time.deltaTime);

#endif

		float angle = targetRotation.eulerAngles.z - transform.rotation.eulerAngles.z;

		// Rotates the registered jelly sprites along with the map to avoid them "falling" if they are far from the rotation center.
		foreach(JellySprite sprite in registeredJellySprites) {

			// if the sprite is close to the player, not disabling it.
			if(Vector2.Distance(sprite.transform.position, player.transform.position) < 10f)
				continue;

			sprite.SetPosition(
				RotatePointAroundPivot(sprite.transform.position, player.transform.position, angle),
				false
			);
		}


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



	/////////////
	// HELPERS //
	/////////////

	private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle) {
		return Quaternion.Euler(0f, 0f, angle) * (point - pivot) + pivot;
	}
}
