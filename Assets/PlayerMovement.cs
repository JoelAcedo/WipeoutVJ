using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float accelerationF = 0.01f;
	public float accelerationB = 0.075f;
	public float maxSpeedF = 15f;
	public float maxSpeedB = 5f;
	public float rotation = 1f;
	private Transform forward;
	private Transform backward;
	private Transform tr;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform> ();	
		rb = gameObject.GetComponent<Rigidbody> ();	
		forward = GameObject.Find ("Forward").GetComponent<Transform> ();
		backward = GameObject.Find ("Backward").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (rb.velocity.magnitude < maxSpeedF) {
				rb.velocity += (forward.position - tr.position) * accelerationF;
			} else {
				//rb.velocity = rb.velocity.normalized * maxSpeedF;
			}
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			if (rb.velocity.magnitude < maxSpeedB) {
				rb.velocity += (backward.position - tr.position) * accelerationB;
			} else {
				
			}
		} 
		Debug.Log(rb.velocity.magnitude);
		if (Input.GetKey (KeyCode.RightArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y+rotation, tr.eulerAngles.z);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y-rotation, tr.eulerAngles.z);
		} 
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Turbo")) {
			rb.velocity = rb.velocity.normalized * maxSpeedF * 1.5f;
		} else if (other.tag.Equals ("Item")) {
			Destroy (other.gameObject);
		}
	}
}
