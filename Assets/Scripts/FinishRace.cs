using UnityEngine;
using System.Collections;

public class FinishRace : MonoBehaviour {

	public InstantGuiWindow def;
	public InstantGuiWindow win;
	public InstantGuiWindow des;
	public InstantGuiElement dw1;
	public InstantGuiElement dw2;
	public InstantGuiElement dw3;
	public InstantGuiElement ww1;
	public InstantGuiElement ww2;
	public InstantGuiTextArea txt;

	// Use this for initialization
	void Start () {
		GameObject.Find ("GameController").SendMessage ("setRaceFinishScene");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void defeatedW1() {
		def.layerOffset = 10;
		dw1.layerOffset = 1;
		txt.rawText = "Computer 1 won! \n \n You lost!";
	}

	void defeatedW2() {
		def.layerOffset = 10;
		dw2.layerOffset = 1;
		txt.rawText = "Computer 2 won! \n \n You lost!";
	}

	void defeatedW3() {
		def.layerOffset = 10;
		dw3.layerOffset = 1;
		txt.rawText = "Computer 3 won! \n \n You lost!";
	}

	void winnerW1() {
		win.layerOffset = 10;
		ww1.layerOffset = 1;
	}

	void winnerW2() {
		win.layerOffset = 10;
		ww2.layerOffset = 1;
	}

	void destroyed() {
		des.layerOffset = 10;
	}
}
