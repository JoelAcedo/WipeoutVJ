using UnityEngine;
using System.Collections;

public class ComputerController : MonoBehaviour {

	public int laps;
	public int pointNum;
	public GameObject missile;
	public GameObject bomb;
	public GameObject shield;
	public GameObject explosion;
	public GameObject boost;
	public GameObject healthEffect;
	public GameObject flameCenter;

	private Transform[] points;
	private Transform tr;
	private Rigidbody rb;
	private AudioSource ao;
	private ParticleSystem.EmissionModule flameC;
	private int nextPoint = 0;
	private float acceleration = 13f;
	private float maxSpeed = 14f;
	private float explosionForce = 50f;
	private float explosionRadius = 3f;
	private float missileSpeed = 1000f;
	private float health = 100f;
	private bool cantCollide = false;
	public bool haveBomb = false;
	public bool haveMissile = false;
	public bool haveShield = false;
	private bool impacted = false;

	IEnumerator placeBomb() {
		yield return new WaitForSeconds (5);
		cantCollide = true;
		GameObject leavedBomb = Instantiate (bomb, tr.position, new Quaternion ()) as GameObject;
	}

	IEnumerator playerImpacted() {
		yield return new WaitForSeconds (0.5f);
		impacted = false;
	}

	private void initPoints() {
		points = new Transform[pointNum];
		for (int i = 0; i < pointNum; i++) {
			points [i] = GameObject.Find ("MapPoint (" + i + ")").transform;
		}
	}

	private Vector3 findClosestPlayer() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		int pos = 0;
		float closest = float.PositiveInfinity;
		for (int i = 0; i < gos.Length; i++) {
			if (gos[i].Equals(gameObject)) continue;
			if (Vector3.Distance(gos[i].transform.position,tr.position) < closest) {
				pos = i;
				closest = Vector3.Distance(gos[i].transform.position,tr.position);
			}
		}
		return gos [pos].transform.position;
	}

	private Vector3 getPointOfContact() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			return hit.point;
		}
		return new Vector3 ();
	} 

	private void addRandomItem() {
		if (!(haveBomb || haveMissile || haveShield)) {
			float rand = Random.value;
			if (rand < 0.33f) haveBomb = true;
			else if (rand < 0.66f) haveMissile = true;
			else haveShield = true;
		}
	}

	// Use this for initialization
	void Start () {
		tr = gameObject.GetComponent<Transform> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		healthEffect.SetActive (false);
		ao = gameObject.GetComponent<AudioSource> ();
		flameC = flameCenter.GetComponent<ParticleSystem> ().emission;
		initPoints ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!impacted) {
			Vector3 dir = points [nextPoint].GetComponent<Transform> ().position;
			if (Vector3.Distance (dir, transform.position) < 5f) nextPoint++;
			if (nextPoint == pointNum) nextPoint = 0;
			gameObject.transform.LookAt (dir);
			dir -= transform.position;
			if (rb.velocity.magnitude < maxSpeed)
				rb.velocity += dir.normalized * Time.deltaTime * acceleration;
		}
		ao.pitch = rb.velocity.magnitude / maxSpeed + 1f;
		flameC.rate = rb.velocity.magnitude / maxSpeed * 100f;
		if (ao.pitch > 2f) ao.pitch = 2f;
		if (haveBomb) {
			haveBomb = false;
			StartCoroutine (placeBomb ());
		} else if (haveMissile) {
			Vector3 missileDir = findClosestPlayer ();
			if (Vector3.Distance (missileDir, tr.position) < 15f) {
				haveMissile = false;
				cantCollide = true;
				GameObject firedMissile = Instantiate (missile, tr.position, tr.rotation) as GameObject;
				firedMissile.transform.LookAt (missileDir);
				firedMissile.transform.localEulerAngles = new Vector3 (firedMissile.transform.localEulerAngles.x + 90f, 
					firedMissile.transform.localEulerAngles.y, firedMissile.transform.localEulerAngles.z);
				firedMissile.GetComponent<Rigidbody> ().AddRelativeForce (new Vector3 (0f, missileSpeed, 0f));
				Destroy (firedMissile, 5f);
			}
		} else if (haveShield) {
			haveShield = false;
			shield.SetActive(true);
		}
	}

	void OnTriggerEnter(Collider other) {
		Transform colider = other.GetComponent<Transform> ();
		if (other.tag.Equals ("FinishLine")) {
			laps--;
			if (laps < 0) Debug.Log(gameObject.name + " has finished!");
			other.GetComponent<AudioSource>().Play();
		} else if (other.tag.Equals ("Turbo")) {
			rb.velocity = rb.velocity.normalized * maxSpeed * 1.5f;
			GameObject booster = Instantiate (boost, tr) as GameObject;
			booster.GetComponent<Transform> ().localPosition = new Vector3 (0f, 0f, 0f);
			booster.GetComponent<Transform> ().localEulerAngles = new Vector3 (180f, 0f, 0f);
			Destroy (booster, 2.5f);
		} else if (other.tag.Equals ("Item")) {
			addRandomItem ();
			other.gameObject.SendMessage ("itemPickedUp");
		} else if (other.tag.Equals("Bomb")) {
			if (cantCollide) {
				cantCollide = false;
			} else if (shield.activeSelf) {
				shield.SetActive (false);
				GameObject explode = Instantiate (explosion, colider.position, colider.rotation) as GameObject;
				Destroy (other.gameObject);
				Destroy (explode, 1.5f);
			} else {
				rb.velocity = rb.velocity.normalized * -5f;
				rb.AddExplosionForce (explosionForce, getPointOfContact (), explosionRadius, 3f, ForceMode.Impulse);
				health -= 25F;
				GameObject explode = Instantiate (explosion, colider.position, colider.rotation) as GameObject;
				Destroy (other.gameObject);
				Destroy (explode, 1.5f);
				impacted = true;
				StartCoroutine (playerImpacted());
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag.Equals ("Health")) {
			healthEffect.SetActive (true);
			health += 25f * Time.deltaTime;
			if (health > 100f) health = 100f;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals ("Health")) {
			healthEffect.SetActive (false);
		}
	}
}
