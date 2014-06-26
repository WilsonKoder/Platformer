using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	//Define variables.
	public float playerSpeed = 50f;
	public int jumpSpeed = 4;
	public Transform Light;
	public Transform Camera;
	public Transform ProjectileTransform;
	public GameObject Projectile;
	public bool isPaused = false;
	public bool hasWon = false;
	public bool hasLost = false;
	public bool canJump = false;
	public bool canWin = false;
	public int score;
	//Old variables.
	//public int enemyCount;
	//public double wait;
	//public string waitString;

	// Use this for initialization
	void Start () {
		//Initialization
		isPaused = false;
		hasWon = false;
		hasLost = false;
		score = 0;
		canJump = false;
		canWin = false;
	}
	
	// Update is called once per frame
	void Update () {
		//InvokeRepeating("Subtract", Time.deltaTime, 0.01f);
		//waitString = wait.ToString();
		//Player movement
		//transform.Translate(Vector3.right * Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime);
		if(Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			rigidbody.velocity = new Vector3(-12, rigidbody.velocity.y, 0);
		}
		if(Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			rigidbody.velocity = new Vector3(12, rigidbody.velocity.y, 0);
		}

		//Jumping
		//rigidbody.AddForce(Vector3.up * Input.GetAxis ("Jump") * 5 000f * Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.W)) {
			if(canJump) {
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0);
			}
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed * -1, 0);
		}

		//Shooting projectile
		if(Input.GetKeyDown(KeyCode.Space)) {
			Instantiate(Projectile, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), ProjectileTransform.rotation);
		}

		//Move the camera and move the light with the player
		Light.position = new Vector3(transform.position.x, transform.position.y, Light.position.z);
		Camera.position = new Vector3(transform.position.x, transform.position.y, Camera.position.z);

		//Check if the players position is less than -3 on the y axis
		if(transform.position.y < -3) {
			hasLost = true;
		}

		//Check to see if the user presses ESC to quit/pause the game.
		if(Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	void OnGUI() {
		GUI.Box (new Rect(10, 10, 120, 40), "Platformer");
		GUI.Label(new Rect(45, 25, 110, 20), "Score: " + score);
		//GUI.Label(new Rect(45, 50, 220, 20), waitString);


		if(isPaused) {
			if (GUI.Button(new Rect(200, 200, 120, 40), "Quit")) {
				Application.Quit ();
			}
			if (GUI.Button(new Rect(200, 250, 120, 40), "Unpause")) {
				Pause ();
			}

		}
		if(hasWon) {
			if(GUI.Button(new Rect(100, 100, 300, 100), "Quit Game?")) {
				Application.Quit();
			}
			if(GUI.Button(new Rect(100, 210, 300, 100), "Advance to next level?")) {
				//Load next level.
				//Application.LoadLevel(1);
			}
		}
		if(hasLost) {
			if(GUI.Button(new Rect(100, 100, 300, 100), "Quit Game?")) {
				Application.Quit();
			}
			if(GUI.Button(new Rect(100, 210, 300, 100), "Retry?")) {
				isPaused = false;
				hasWon = false;
				hasLost = false;
				score = 0;
				canJump = false;
				canWin = false;

				transform.position = new Vector3(-14, 2, 0);
				//take one away from score
			}
		}
	}

	//Pause function
	void Pause() {
		if(isPaused == true && !hasWon) {
			Time.timeScale = 1;
			Debug.Log ("Game unpaused");
			isPaused = false;
		} else if(isPaused == false && !hasWon) {
			Time.timeScale = 0;
			Debug.Log("Game paused");
			isPaused = true;
		}
		if(hasWon) {
			Time.timeScale = 0;
			Debug.Log("Has won, therefore pausing");
		}
	}

	void OnTriggerEnter() {
		Debug.Log ("You Win!");
		hasWon = true;
		Pause();
		score = score + 1;
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if(collisionInfo.transform.tag == "Enemy") {
			Destroy(gameObject);
			hasLost = true;
		} else {
		canJump = true;
		}
	}

	void OnCollisionExit(Collision collisionInfo) {
		canJump = false;
	}

	/*
	IEnumerator Counter() {
		while(true) {
		yield return new WaitForSeconds();
		wait = wait + 0.01;
		}
	}


	void Subtract() {
		wait = wait + 0.01;
	}
	*/
	
}
