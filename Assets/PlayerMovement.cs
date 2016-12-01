using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float acceleration = 0.01f;
	public float rotation = 1f;
	private Transform dir;
	private Transform tr;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform> ();	
		rb = gameObject.GetComponent<Rigidbody> ();	
		dir = GameObject.Find ("Direction").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			rb.velocity += (dir.position - tr.position) * acceleration;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			rb.velocity -= (dir.position - tr.position) * acceleration;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y+rotation, tr.eulerAngles.z);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y-rotation, tr.eulerAngles.z);
		} 
	}
}
