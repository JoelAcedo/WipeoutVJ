using UnityEngine;
using System.Collections;

public class ItemRotate : MonoBehaviour {

	public float rotSpeed = 60f;
	public bool self = true;
	private Transform tr;

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (self)
			tr.Rotate (0, rotSpeed * Time.deltaTime, 0, Space.Self);
		else
			tr.Rotate (0, rotSpeed * Time.deltaTime, 0, Space.World);
	}
}
