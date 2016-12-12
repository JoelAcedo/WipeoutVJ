using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	public GameObject driveBar;
	public Image missileGUI;
	public Image bombGUI;
	public Text lapsText;
	public Text positionText;
	public int laps;
	public int ships;

	// Use this for initialization
	void Start () {
		setLap (0);
		setPosition (ships);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void enableMissile() {
		missileGUI.enabled = true;
	}

	void enableBomb() {
		bombGUI.enabled = true;
	}

	void disableMissile() {
		missileGUI.enabled = false;
	}

	void disableBomb() {
		bombGUI.enabled = false;
	}

	void setLap(int lap) {
		lapsText.text = "Lap " + lap + "/" + laps;
	}

	void setPosition(int pos) {
		positionText.text = "Pos " + pos + "/" + ships;
	}

	void setHealth(float health) {
		driveBar.SendMessage ("setHealth", health);
	}
}
