using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void GotoScene(string scene) {
		Debug.Log("Switching to Scene " + scene);
		SceneManager.LoadScene(scene);
	}
}
