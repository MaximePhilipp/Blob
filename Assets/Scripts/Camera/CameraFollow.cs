using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// CONSTANTS :
	private const float CAMERA_SPEED = 3f;

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
	}
}
