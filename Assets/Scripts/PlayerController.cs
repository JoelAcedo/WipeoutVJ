using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int pointNum;
	public GameObject missile;
	public GameObject bomb;
	public GameObject shield;
	public GameObject explosion;
	public GameObject boost;
	public GameObject healthEffect;
	public GameObject flameCenter;
	public GameObject flameRight;
	public GameObject flameLeft;
	public GameObject playerCam;
	public GameObject guiController;

	private GameObject lastCP;
	private Transform forward;
	private Transform backward;
	private Transform tr;
	private Rigidbody rb;
	private AudioSource ao;
	private ParticleSystem.EmissionModule flameC;
	private ParticleSystem.EmissionModule flameR;
	private ParticleSystem.EmissionModule flameL;
	private float accelerationF = 0f;
	private float accelerationB = 0f;
	private float maxSpeedF = 0f;
	private float maxSpeedB = 0f;
	private float rotation = 0f;
	private float explosionForce = 50f;
	private float explosionRadius = 3f;
	private float missileSpeed = 1000f;
	private float playerHealth = 100f;
	private int lap = 0;
	private bool cantCollide = false;
	private bool haveBomb = false;
	private bool haveMissile = false;
	private bool haveShield = false;

	private Vector3 getPointOfContact() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			return hit.point;
		}
		return new Vector3 ();
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

	private string calculatePos(){
		string[] s = lastCP.name.Split (new char[] { '(', ')' });
		string pj = gameObject.name;
		string pos1 = (int.Parse (s [1]) * (lap * pointNum)).ToString ();
		int nextP = int.Parse (s [1]) + 5;
		if (nextP >= pointNum) nextP = 0; 
		string pos2 = Vector3.Distance (GameObject.Find ("MapPoint (" + nextP.ToString() + ")").transform.position, tr.position).ToString();
		return pj + "_" + pos1 + "_" + pos2;
	}
		
	private void addRandomItem() {
		if (!(haveBomb || haveMissile || haveShield)) {
			float rand = Random.value;
			if (rand < 0.40f) {
				haveBomb = true;
				guiController.SendMessage ("enableBomb");
			} else if (rand < 0.80f) {
				haveMissile = true;
				guiController.SendMessage ("enableMissile");
			} else {
				haveShield = true;
				guiController.SendMessage ("enableShield");
			}
		}
	}

	// Use this for initialization
	void Start () {
		GameObject.Find ("GameController").SendMessage ("getModel");
		tr = gameObject.GetComponent<Transform> ();	
		rb = gameObject.GetComponent<Rigidbody> ();	
		lastCP = GameObject.Find("MapPoint (0)");
		forward = GameObject.Find ("Forward").GetComponent<Transform> ();
		backward = GameObject.Find ("Backward").GetComponent<Transform> ();
		healthEffect.SetActive (false);
		ao = gameObject.GetComponent<AudioSource> ();
		flameC = flameCenter.GetComponent<ParticleSystem> ().emission;
		flameR = flameRight.GetComponent<ParticleSystem> ().emission;
		flameL = flameLeft.GetComponent<ParticleSystem> ().emission;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			if (rb.velocity.magnitude < maxSpeedF)
				rb.velocity += (forward.position - tr.position).normalized * Time.deltaTime * accelerationF;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			if (rb.velocity.magnitude < maxSpeedB)
				rb.velocity += (backward.position - tr.position).normalized * Time.deltaTime * accelerationB;
		} 
		ao.pitch = rb.velocity.magnitude / maxSpeedF + 1f;
		flameC.rate = rb.velocity.magnitude / maxSpeedF * 100f;
		flameR.rate = rb.velocity.magnitude / maxSpeedF * 100f;
		flameL.rate = rb.velocity.magnitude / maxSpeedF * 100f;
		if (ao.pitch > 2f) ao.pitch = 2f;
		if (Input.GetKey (KeyCode.RightArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y+rotation, tr.eulerAngles.z);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			tr.eulerAngles = new Vector3 (tr.eulerAngles.x, tr.eulerAngles.y-rotation, tr.eulerAngles.z);
		} 
		if (Input.GetKeyDown (KeyCode.F)) {
			if (haveBomb) {
				cantCollide = true;
				haveBomb = false;
				guiController.SendMessage ("disableBomb");
				GameObject leavedBomb = Instantiate (bomb, tr.position, new Quaternion ()) as GameObject;
			} else if (haveMissile) {
				cantCollide = true;
				haveMissile = false;
				guiController.SendMessage ("disableMissile");
				Vector3 missileDir = findClosestPlayer ();
				GameObject firedMissile = Instantiate (missile, tr.position, tr.rotation) as GameObject;
				firedMissile.transform.LookAt (missileDir);
				firedMissile.transform.localEulerAngles = new Vector3 (firedMissile.transform.localEulerAngles.x + 90f, 
					firedMissile.transform.localEulerAngles.y, firedMissile.transform.localEulerAngles.z);
				firedMissile.GetComponent<Rigidbody> ().AddRelativeForce (new Vector3 (0f, missileSpeed, 0f));
				Destroy (firedMissile, 5f);
			} else if (haveShield) {
				haveShield = false;
				guiController.SendMessage ("disableShield");
				shield.SetActive (true);
			}
		} else if (Input.GetKeyDown (KeyCode.R)) {
			tr.position = lastCP.transform.position;
			tr.localEulerAngles = new Vector3 (90f, 0f, 0f);
			rb.velocity = Vector3.zero;
		}
		if (Input.GetKey(KeyCode.G)) {
			playerCam.transform.localPosition = new Vector3 (0f, 0f, -5f);
			playerCam.transform.localEulerAngles = new Vector3 (90f, 0f, -180f);
		} else if (Input.GetKeyUp(KeyCode.G)) {
			playerCam.transform.localPosition = new Vector3 (0f, -30f, -15f);
			playerCam.transform.localEulerAngles = new Vector3 (-80, 0f, 0f);
		}
		if (Input.GetKeyDown(KeyCode.Escape) )
			GameObject.Find ("GameController").SendMessage ("finishRace",4);
		guiController.SendMessage ("playerPos",calculatePos());
	}

	void OnTriggerEnter(Collider other) {
		Transform colider = other.GetComponent<Transform> ();
		if (other.tag.Equals ("FinishLine")) {
			lap++;
			if (lap >= 3) GameObject.Find ("GameController").SendMessage ("finishRace",4);
			else guiController.SendMessage ("setLap", lap);
			other.GetComponent<AudioSource> ().Play ();
		} else if (other.tag.Equals ("Turbo")) {
			rb.velocity = rb.velocity.normalized * maxSpeedF * 1.5f;
			GameObject booster = Instantiate (boost, tr) as GameObject;
			booster.GetComponent<Transform> ().localPosition = new Vector3 (0f, 0f, 0f);
			booster.GetComponent<Transform> ().localEulerAngles = new Vector3 (90f, 0f, 0f);
			Destroy (booster, 2.5f);
		} else if (other.tag.Equals ("Item")) {
			addRandomItem ();
			other.gameObject.SendMessage ("itemPickedUp");
		} else if (other.tag.Equals ("Bomb")) {
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
				playerHealth -= 25F;
				guiController.SendMessage ("setHealth", playerHealth);
				GameObject explode = Instantiate (explosion, colider.position, colider.rotation) as GameObject;
				Destroy (other.gameObject);
				Destroy (explode, 1.5f);
				if (playerHealth <= 0f) {
					GameObject.Find ("GameController").SendMessage ("finishRace",5);
				}
			}
		} else if (other.tag.Equals ("CheckPoint")) {
			lastCP = other.gameObject;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag.Equals ("Health")) {
			healthEffect.SetActive (true);
			playerHealth += 25f * Time.deltaTime;
			if (playerHealth > 100f) playerHealth = 100f;
			guiController.SendMessage ("setHealth", playerHealth);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals ("Health")) {
			healthEffect.SetActive (false);
		}
	}

	void setModel(int model) {
		if (model == 1) {
			GameObject.Find ("ModelB").SetActive (false);
			accelerationF = 14f;
			accelerationB = 9f;
			maxSpeedF = 15f;
			maxSpeedB = 5f;
			rotation = 3f;
		} else {
			GameObject.Find ("ModelA").SetActive (false);
			accelerationF = 12f;
			accelerationB = 9f;
			maxSpeedF = 18f;
			maxSpeedB = 5f;
			rotation = 5f;
		}
	}
}
