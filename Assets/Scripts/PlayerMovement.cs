using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed = 25f;
	public int jumpSpeed = 4;
	public Transform Light;
	public Transform Camera;
	public bool isPaused = false;
	public bool hasWon = false;
	public int score;

	// Use this for initialization
	void Start () {
		isPaused = false;
		hasWon = false;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Player movement
		transform.Translate(Vector3.right * Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime);
		//Jumping
		//rigidbody.AddForce(Vector3.up * Input.GetAxis ("Jump") * 5000f * Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.Space)) {
			rigidbody.velocity = new Vector3(0, jumpSpeed, 0);
		}

		//Move the camera and move the light with the player
		Light.position = new Vector3(transform.position.x, transform.position.y, Light.position.z);
		Camera.position = new Vector3(transform.position.x, transform.position.y, Camera.position.z);

		//Check if the players position is less than -3 on the y axis
		if(transform.position.y < -3) {
			//If so, move the player back to the spawn position. (0, 2, 0)
			transform.position = new Vector3(0, 2, 0);
			//take one away from score
			score = score - 1;
		}

		//Check to see if the user presses ESC to quit/pause the game.
		if(Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	void OnGUI() {
		GUI.Box (new Rect(10, 10, 120, 40), "Platformer");
		GUI.Label(new Rect(45, 25, 110, 20), "Score: " + score);

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
	}
}
