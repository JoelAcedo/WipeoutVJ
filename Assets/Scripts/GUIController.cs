using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	public GameObject driveBar;
	public Image missileGUI;
	public Image bombGUI;
	public Image shieldGUI;
	public Text lapsText;
	public Text positionText;
	public int laps;
	public int ships;

	private int playePos = 0;
	private int comp1Pos = 0;
	private int comp2Pos = 0;
	private int comp3Pos = 0;
	private float playeDis = 0f;
	private float comp1Dis = 0f;
	private float comp2Dis = 0f;
	private float comp3Dis = 0f;

	// Use this for initialization
	void Start () {
		setLap (0);
		setPosition (ships);
	}
	
	// Update is called once per frame
	void Update () {
		int pos = 1;
		if (playePos < comp1Pos || (playePos == comp1Pos && playeDis > comp1Dis)) pos++;
		if (playePos < comp2Pos || (playePos == comp2Pos && playeDis > comp2Dis)) pos++;
		if (playePos < comp3Pos || (playePos == comp3Pos && playeDis > comp3Dis)) pos++;
		setPosition (pos);
	}

	void enableMissile() {
		missileGUI.enabled = true;
	}

	void enableBomb() {
		bombGUI.enabled = true;
	}

	void enableShield() {
		shieldGUI.enabled = true;
	}

	void disableMissile() {
		missileGUI.enabled = false;
	}

	void disableBomb() {
		bombGUI.enabled = false;
	}

	void disableShield() {
		shieldGUI.enabled = false;
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

	void playerPos(string pos) {
		string[] s = pos.Split (new char[] { '_' });
		if (s[0].Equals("Player")) {
			playePos = int.Parse(s[1]);
			playeDis = float.Parse(s[2]);
		} else if(s[0].Equals("Computer1")) {
			comp1Pos = int.Parse(s[1]);
			comp1Dis = float.Parse(s[2]);
		} else if(s[0].Equals("Computer2")) {
			comp2Pos = int.Parse(s[1]);
			comp2Dis = float.Parse(s[2]);
		} else if(s[0].Equals("Computer3")) {
			comp3Pos = int.Parse(s[1]);
			comp3Dis = float.Parse(s[2]);
		}
	}
}
