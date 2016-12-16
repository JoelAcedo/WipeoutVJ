using UnityEngine;
using System.Collections;

public class StartRace : MonoBehaviour {

	public AudioClip sound;

	IEnumerator startRace() {
		yield return new WaitForSeconds (1.5f);
		gameObject.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (3f);
		gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		gameObject.GetComponent<AudioSource> ().clip = sound;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (startRace ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
