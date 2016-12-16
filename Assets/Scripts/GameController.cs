using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {
	private AudioSource ao;
	private int player1model = 1;
	private int finishCase = 0;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		GameObject[] gos = GameObject.FindGameObjectsWithTag ("GameController");
		for (int i = gos.Length - 1; i > 0; i--) Destroy (gos [i]);
	}

	// Use this for initialization
	void Start () {
		ao = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void onePlayerClicked() {
		SceneManager.LoadScene (1);
	}

	void twoPlayersClicked() {
		Debug.Log ("twoPlayerClicked");
	}

	void quitClicked() {
		Debug.Log ("quitClicked");
		Application.Quit ();
	}

	void enableA() {
		player1model = 1;
	}

	void enableB() {
		player1model = 2;
	}

	void startTutorial() {
		SceneManager.LoadScene (3);
	}

	void startMapA() {
		SceneManager.LoadScene (4);
	}

	void startMapB() {
		SceneManager.LoadScene (5);
	}

	void next() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	void back() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
	}

	void toMainMenu() {
		player1model = 1;
		finishCase = 0;
		SceneManager.LoadScene (0);
	}

	void getModel() {
		ao.Stop ();
		GameObject.Find("Player").SendMessage("setModel", player1model);
	}

	void finishRace(int finishCase) {
		this.finishCase = finishCase;
		SceneManager.LoadScene (6);
	}

	void setRaceFinishScene() {
		ao.Play ();
		switch (finishCase) {
		case 1:
			GameObject.Find ("Main Camera").SendMessage ("defeatedW1");
			break;
		case 2:
			GameObject.Find ("Main Camera").SendMessage ("defeatedW2");
			break;
		case 3:
			GameObject.Find ("Main Camera").SendMessage ("defeatedW3");
			break;
		case 4:
			if (player1model == 1)
				GameObject.Find ("Main Camera").SendMessage ("winnerW1");
			else 
				GameObject.Find ("Main Camera").SendMessage ("winnerW2");
			break;
		case 5:
			GameObject.Find ("Main Camera").SendMessage ("destroyed");
			break;
		default:
			break;
		}
	}
}
