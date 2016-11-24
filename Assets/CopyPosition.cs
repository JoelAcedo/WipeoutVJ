using UnityEngine;
using System.Collections;

public class CopyPosition : MonoBehaviour {

	public GameObject ball;
	private Transform t;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		t = ball.GetComponent<Transform> ();
		rb = ball.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPos = new Vector3 (t.position.x, t.position.y + 15f, t.position.z - 30f);
		gameObject.GetComponent<Transform> ().position = cameraPos;
	}
}
