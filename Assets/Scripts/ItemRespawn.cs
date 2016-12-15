using UnityEngine;
using System.Collections;

public class ItemRespawn : MonoBehaviour {

	public GameObject itemPickUp;

	private Transform tr;
	private MeshRenderer mr;
	private BoxCollider bc;

	IEnumerator itemTemporallyNotActive() {
		yield return new WaitForSeconds (5);
		mr.enabled = true;
		bc.enabled = true;
	}

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform> ();
		mr = gameObject.GetComponent<MeshRenderer> ();
		bc = gameObject.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void itemPickedUp() {
		mr.enabled = false;
		bc.enabled = false;
		GameObject item = Instantiate (itemPickUp, tr.position, tr.rotation) as GameObject;
		Destroy (item, 2f);
		StartCoroutine(itemTemporallyNotActive ());
	}
		
}
