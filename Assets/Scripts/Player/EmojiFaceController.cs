using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiFaceController : MonoBehaviour {


	private void Awake () {
		SpriteRenderer faceRenderer = GetComponentInChildren<SpriteRenderer>();

		if(!faceRenderer) {
			Debug.Log("No face sprite renderer in children !");
			return;
		}

		faceRenderer.sprite = EmojiFaces.GetRandomFace();
	}
}
