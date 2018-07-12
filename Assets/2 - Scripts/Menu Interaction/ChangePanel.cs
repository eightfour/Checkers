using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour {

    public void GotoPanel (GameObject panel)
    {
        transform.parent.gameObject.SetActive(false);
        panel.SetActive(true);
		Debug.Log(panel.name + " is now active.");
    }
}
