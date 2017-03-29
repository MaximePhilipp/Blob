using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSettingsPopupController : MonoBehaviour {

	// PROPERTIES :
	[SerializeField] private Canvas popupCanvas;


	public void OnTouchSettingClicked() {
		AppData.StartGame(AppData.GroundTiltType.Touch);
		DisablePopup();
	}

	public void OnGyroSettingClicked() {
		AppData.StartGame(AppData.GroundTiltType.Gyro);
		DisablePopup();
	}

	private void DisablePopup() {
		popupCanvas.gameObject.SetActive(false);
	}
}
