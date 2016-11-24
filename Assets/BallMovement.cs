using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour 
{

	public float maxSpeed;
	public float minSpeed;
	public float acceleration;
	public float brake;
	public float rotationSpeed;
	private Rigidbody rb;
	private Transform tr;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		tr = GetComponent<Transform> ();
	}

	void FixedUpdate()
	{
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (rb.velocity.z + acceleration < maxSpeed)
				rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z + acceleration);
			else
				rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			if (rb.velocity.z - brake > minSpeed)
				rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z - brake);
			else
				rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z);
		}

		if (rb.velocity.z < -2f || rb.velocity.z > 4f) {
			if (Input.GetKey (KeyCode.RightArrow))
				rb.angularVelocity = new Vector3 (rb.angularVelocity.x , 5f, rb.angularVelocity.z);
			else if (Input.GetKey (KeyCode.LeftArrow)) 
				rb.angularVelocity = new Vector3 (rb.angularVelocity.x , -5f, rb.angularVelocity.z);
		}
	}

}