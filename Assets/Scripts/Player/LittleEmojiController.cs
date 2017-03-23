using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEmojiController : MonoBehaviour {


	void Start () {
		GroundTiltController.RegisterJellySprite(GetComponent<JellySprite>());
	}
}
