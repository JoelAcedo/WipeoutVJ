using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GameObject missile;
	public GameObject bomb;
	public GameObject explosion;
	public GameObject boost;
	public GameObject itemPickUp;

	private Transform forward;
	private Transform backward;
	private Transform tr;
	private Rigidbody rb;
	private float accelerationF = 5f;
	private float accelerationB = 3.5f;
	private float maxSpeedF = 15f;
	private float maxSpeedB = 5f;
	private float rotation = 1f;
	private float explosionForce = 50f;
	private float explosionRadius = 3f;
	private float missileSpeed = 1000f;
	private bool shield = false;
	private bool haveBomb = false;
	private bool haveMissile = false;

	private Vector3 getPointOfContact() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			Debug.Log (hit.point);
			return hit.point;
		}
		return new Vector3 ();
	} 

	private void addRandomItem() {
		if (Random.value < 0.5f) 
			haveBomb = true;
		else
			haveMissile = true;
	}

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
			if (rb.velocity.magnitude < maxSpeedF)
				rb.velocity += (forward.position - tr.position) * Time.deltaTime * accelerationF;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			if (rb.velocity.magnitude < maxSpeedB)
				rb.velocity += (backward.position - tr.position) * Time.deltaTime * accelerationB;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y+rotation, tr.eulerAngles.z);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y-rotation, tr.eulerAngles.z);
		} 
		if (Input.GetKeyDown (KeyCode.F)) {
			if (haveBomb) {
				shield = true;
				haveBomb = false;
				GameObject leavedBomb = Instantiate (bomb, tr.position, new Quaternion ()) as GameObject;
			} else if (haveMissile) {
				shield = true;
				haveMissile = false;
				GameObject firedMissile = Instantiate (missile, tr.position, tr.rotation) as GameObject;
				firedMissile.GetComponent<Rigidbody> ().AddRelativeForce (new Vector3 (0f, missileSpeed, 0f));
				Destroy (firedMissile, 5f);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Transform colider = other.GetComponent<Transform> ();
		if (other.tag.Equals ("Turbo")) {
			rb.velocity = rb.velocity.normalized * maxSpeedF * 1.5f;
			GameObject booster = Instantiate (boost, tr) as GameObject;
			booster.GetComponent<Transform> ().localPosition = new Vector3 (0f, 0f, 0f);
			booster.GetComponent<Transform> ().localEulerAngles = new Vector3 (90f, 0f, 0f);
			Destroy (booster, 2.5f);
		} else if (other.tag.Equals ("Item")) {
			addRandomItem ();
			GameObject item = Instantiate (itemPickUp, colider.position, colider.rotation) as GameObject;
			other.gameObject.SendMessage ("itemPickedUp");
			Destroy (item, 2f);
		} else if (other.tag.Equals("Bomb")) {
			if (shield) {
				shield = false;
			} else {
				rb.velocity = rb.velocity.normalized * -5f;
				rb.AddExplosionForce (explosionForce, getPointOfContact (), explosionRadius, 3f, ForceMode.Impulse);
				GameObject explode = Instantiate (explosion, colider.position, colider.rotation) as GameObject;
				Destroy (other.gameObject);
				Destroy (explode, 1.5f);
			}
		}
	}
}
