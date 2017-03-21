using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiFaces : MonoBehaviour {

	public enum Face {
		HAPPY,
		NEUTRAL,
		PISSED,
		SMILE,
		WORRIED
	}

	[SerializeField] private List<Sprite> emojiFaces;



	private static List<Sprite> faces;


	private void Awake() {
		faces = emojiFaces;
	}

	public static Sprite GetRandomFace() {
		return faces[Random.Range(0, faces.Count)];
	}

	public static Sprite GetFace(Face face) {
		return faces[(int) face];
	}
}
