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
	public GameObject enemyObject;
	public GameObject enemyObject2;
	private bool isPaused = false;
	private bool hasWon = false;
	private bool hasLost = false;
	private bool canJump = false;
	private bool canWin = false;
	private bool isDead = false;
	private int score;
	private int enemyCount;
	private string timeString;
	//Old variables.
	//public int enemyCount;
	//public double wait;
	//public string waitString;

	// Use this for initialization
	void Start () {
		//Initialization
		isDead = false;
		isPaused = false;
		hasWon = false;
		hasLost = false;
		score = 0;
		canJump = false;
		canWin = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(enemyObject == null && enemyObject2 == null)
		{
			canWin = true;
		}
		timeString = Time.timeSinceLevelLoad.ToString();
		//InvokeRepeating("Subtract", Time.deltaTime, 0.01f);
		//waitString = wait.ToString();
		//Player movement
		//transform.Translate(Vector3.right * Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.E))
		{
			hasWon = true;
			canWin = true;
		}

		if(Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow))
		{
			rigidbody.velocity = new Vector3(-12, rigidbody.velocity.y, 0);
		}
		if(Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow))
		{
			rigidbody.velocity = new Vector3(12, rigidbody.velocity.y, 0);
		}

		//Jumping
		//rigidbody.AddForce(Vector3.up * Input.GetAxis ("Jump") * 5 000f * Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.W))
		{
			if(canJump)
			{
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0);
			}
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed * -1, 0);
		}

		//Shooting projectile
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(Projectile, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), ProjectileTransform.rotation);
		}

		//Move the camera and move the light with the player
		Light.position = new Vector3(transform.position.x, transform.position.y, Light.position.z);
		Camera.position = new Vector3(transform.position.x, transform.position.y, Camera.position.z);

		//Check if the players position is less than -3 on the y axis
		if(transform.position.y < -3)
		{
			hasLost = true;
		}

		//Check to see if the user presses ESC to quit/pause the game.
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			Pause ();
		}
	}

	void OnGUI() {
		GUI.Box (new Rect(10, 10, 120, 60), "Platformer");
		GUI.Label(new Rect(45, 25, 110, 20), "Score: " + score);
		GUI.Label(new Rect(45, 45, 110, 20), timeString);
		//GUI.Label(new Rect(45, 50, 220, 20), waitString);


		if(isPaused)
		{
			if (GUI.Button(new Rect(200, 200, 120, 40), "Quit"))
			{
				Application.Quit ();
			}
			if (GUI.Button(new Rect(200, 250, 120, 40), "Unpause"))
			{
				Pause ();
			}

		}
		if(hasWon && canWin)
		{
			if(GUI.Button(new Rect(100, 100, 300, 100), "Quit Game?"))
			{
				//Quit the game.
				Application.Quit();
			}
			if(GUI.Button(new Rect(100, 210, 300, 100), "Advance to next level?"))
			{
				//Load next level.
				//Currently don't have any more levels.
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel+1);

			}
			if(GUI.Button(new Rect(100, 320, 300, 100), "Retry?"))
			{
				Debug.Log("Won-Retry");
				isPaused = false;
				hasWon = false;
				hasLost = false;
				score = 0;
				canJump = false;
				canWin = false;
				timeString = "0";
				Time.timeScale = 1;
				rigidbody.velocity = new Vector3(0, 0, 0);
				
				transform.position = new Vector3(-14, 2, 0);
				//take one away from score
			}
		}
		if(hasLost)
		{
			if(GUI.Button(new Rect(100, 100, 300, 100), "Quit Game?"))
			{
				Application.Quit();
			}
			if(GUI.Button(new Rect(100, 210, 300, 100), "Retry?"))
			{
				Debug.Log ("Lost-Retry");
				isPaused = false;
				hasWon = false;
				hasLost = false;
				score = 0;
				canJump = false;
				canWin = false;
				timeString = "0";
				Time.timeScale = 1;
				rigidbody.velocity = new Vector3(0, 0, 0);

				transform.position = new Vector3(-14, 2, 0);
				//take one away from score
			}
		}
		if(isDead)
		{
			if(GUI.Button(new Rect(100, 100, 300, 100), "Quit Game?"))
			{
				Application.Quit();
			}
			if(GUI.Button(new Rect(100, 210, 300, 100), "Retry?"))
			{
				isDead = false;
				isPaused = false;
				hasWon = false;
				hasLost = false;
				score = 0;
				canJump = false;
				canWin = false;
				timeString = "0";
				Time.timeScale = 1;
				
				transform.position = new Vector3(-14, 2, 0);
				//take one away from score
			}
		}
	}

	//Pause function
	void Pause() {
		if(isPaused == true && !hasWon)
		{
			Time.timeScale = 1;
			Debug.Log ("Game unpaused");
			isPaused = false;
		} 
		else if(isPaused == false && !hasWon)
		{
			Time.timeScale = 0;
			Debug.Log("Game paused");
			isPaused = true;
		}
		if(hasWon)
		{
			Time.timeScale = 0;
			Debug.Log("Has won, therefore pausing");
		}
	}

	void OnTriggerEnter()
	{
		if(canWin)
		{
			Debug.Log ("You Win!");
			hasWon = true;
			Pause();
			score = score + 1;
		}
	}

	void OnCollisionEnter(Collision collisionInfo)
	{
		if(collisionInfo.gameObject.tag == "Enemy")
		{
			hasLost = true;
			Time.timeScale = 0;
		} 
		else
		{
			canJump = true;
		}
	}

	void OnCollisionExit(Collision collisionInfo)
	{
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
