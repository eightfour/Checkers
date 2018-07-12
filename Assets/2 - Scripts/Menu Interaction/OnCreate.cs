using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCreate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Disable all menus except main menu on startup
		GameObject.Find("SettingsMenu").SetActive(false);
		GameObject.Find("NetworkMenu").SetActive(false);
		GameObject.Find("RulesMenu").SetActive(false);
		GameObject.Find("MainMenu").SetActive(true);
	}
}
