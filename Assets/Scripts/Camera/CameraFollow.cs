using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// CONSTANTS :
	private const float CAMERA_SPEED = 30f;

	// PROPERTIES :
	[SerializeField] private GameObject targetPlayer;
	[SerializeField] private GameObject ground;

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

		Vector3 groundEulerAngles = ground.transform.rotation.eulerAngles;
		float cameraZAngle = 0;
		if(groundEulerAngles.z <= 180f)
			cameraZAngle = groundEulerAngles.z * 0.8f;
		else {
			Debug.Log("z euler angle : " + groundEulerAngles.z);
			cameraZAngle = (-360f + groundEulerAngles.z) * 0.8f;
		}

		this.transform.rotation = Quaternion.Euler(
			groundEulerAngles.x,
			groundEulerAngles.y,
			cameraZAngle
		);
	}
}
