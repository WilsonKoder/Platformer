using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed = 25f;
	public Transform Light;
	public Transform Camera;
	public bool isPaused = false;
	public int score;

	// Use this for initialization
	void Start () {
		isPaused = false;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Player movement
		transform.Translate(Vector3.right * Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime);
		//Jumping
		//
		//

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
	}

	//Pause function
	void Pause() {
		if(isPaused == true) {
			Time.timeScale = 1;
			Debug.Log ("Game unpaused");
			isPaused = false;
		} else if(isPaused == false) {
			Time.timeScale = 0;
			Debug.Log("Game paused");
			isPaused = true;
		}
	}
}
