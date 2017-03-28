using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// CONSTANTS :
	private const float CAMERA_SPEED = 30f;

	// PROPERTIES :
	[SerializeField] private GameObject targetPlayer;

	private void Awake() {
		Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
		this.transform.position = Vector3.Lerp(
			transform.position,
			new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, transform.position.z),
			CAMERA_SPEED * Time.deltaTime
		);

		float groundAngle = GroundTiltController.GetEulerAngleZ();
		float cameraZAngle = 0;
		float cameraTiltFactor = AppData.GetCurrentTiltType() == AppData.GroundTiltType.Touch ? 0.3f : 0.9f;
		if (groundAngle <= 180f)
			cameraZAngle = groundAngle * cameraTiltFactor;
		else
			cameraZAngle = (-360f + groundAngle) * cameraTiltFactor;

		this.transform.rotation = Quaternion.Euler(
			0f,
			0f,
			cameraZAngle
		);
	}
}
