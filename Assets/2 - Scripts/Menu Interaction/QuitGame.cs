﻿using UnityEngine;

public class QuitGame : MonoBehaviour {
	public void Quit() {
		Debug.Log("Quitting Game...");
        Application.Quit();
	}
}