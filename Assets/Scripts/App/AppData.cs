using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData : MonoBehaviour {

	public enum GroundTiltType {
		None,
		Touch,
		Gyro
	}

	// PROPERTIES :
	private static GroundTiltType tiltType;
	private static float startTime;


	private void Start() {
		tiltType = GroundTiltType.None;
	}


	public static GroundTiltType GetCurrentTiltType() {
		return tiltType;
	}

	public static void StartGame(GroundTiltType type) {
		SetCurrentTiltType(type);
		startTime = Time.time;
	}
		
	private static void SetCurrentTiltType(GroundTiltType type) {
		tiltType = type;
	}

	public static float GetStartTime() {
		return startTime;
	}
}
