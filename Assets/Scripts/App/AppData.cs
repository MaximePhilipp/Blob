using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData : MonoBehaviour {

	public enum GroundTiltType {
		Touch,
		Gyro
	}

	// PROPERTIES :
	private static GroundTiltType tiltType;


	public static GroundTiltType GetCurrentTiltType() {
		return tiltType;
	}


	// TODO Create the switch between the controls.
	private void Start() {
		tiltType = GroundTiltType.Touch;
	}

}
