using UnityEngine;
using System.Collections;

public class ItemRespawn : MonoBehaviour {

	private MeshRenderer mr;
	private BoxCollider bc;

	IEnumerator itemTemporallyNotActive() {
		yield return new WaitForSeconds (5);
		mr.enabled = true;
		bc.enabled = true;
	}

	// Use this for initialization
	void Start () {
		mr = gameObject.GetComponent<MeshRenderer> ();
		bc = gameObject.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void itemPickedUp() {
		mr.enabled = false;
		bc.enabled = false;
		StartCoroutine(itemTemporallyNotActive ());
	}
		
}
