using UnityEngine;
using UnityEngine.UI;
using Logik;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour {

	public Button createB;
	public Button joinB;
	public InputField ipF;
	public Dropdown colorD;
    Spiel spiel;

	void Start()
	{
        NetworkModel.server = false;
        NetworkModel.client = false;
        NetworkModel.ip = "";
        //wenn ich das Spiel initialisieren will wirft das nullreference exceptions??

        //       GameObject gameObject = new GameObject("Spiel");
        //      spiel = gameObject.AddComponent<Spiel>();

        createB.onClick.AddListener(delegate
        {
            NetworkModel.server = true;
            Debug.Log("Creating game.");
            SceneManager.LoadScene("Game");
        });

		joinB.onClick.AddListener(delegate
		{
            NetworkModel.ip = ipF.text;
            NetworkModel.client = true;
            Debug.Log("Joining game.");
            SceneManager.LoadScene("Game");
        });

		ipF.onValueChanged.AddListener(delegate
		{
			if (ipF.text != "")
			{
				joinB.interactable = true;
			}
			else
			{
				joinB.interactable = false;
			}
		});
	}

	public void connect() {
		string ip = ipF.text;
		Debug.Log("Trying to connect to: " + ip);

		// TODO
	}

	public void createGame() {
		// TODO
	}
}
