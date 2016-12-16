using UnityEngine;
using System.Collections;

public class GUISound : MonoBehaviour {

	private AudioSource ao;

	// Use this for initialization
	void Start () {
		ao = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void click() {
		ao.Play ();
	}

	void quitClicked() {
		ao.Play ();
	}

	void enableA() {
		ao.Play ();
	}

	void enableB() {
		ao.Play ();
	}
}
