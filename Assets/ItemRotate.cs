using UnityEngine;
using System.Collections;

public class ItemRotate : MonoBehaviour {

	public float rotSpeed = 60f;
	private Transform tr;

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		tr.Rotate (0, rotSpeed * Time.deltaTime, 0, Space.World);
	}
}
