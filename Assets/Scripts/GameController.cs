using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onePlayerClicked() {
		Debug.Log ("onePlayerClicked");
	}

	void twoPlayersClicked() {
		Debug.Log ("twoPlayerClicked");
	}

	void quitClicked() {
		Debug.Log ("quitClicked");
		Application.Quit ();
	}
}
