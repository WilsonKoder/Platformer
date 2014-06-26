using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float enemySpeed = 5f;
	//private Vector3 startPos = new Vector3(-3, 1, 0);
	//private Vector3 endPos = new Vector3(-0.1f, 1, 0);
	private bool moveLeft = false;
	private bool moveRight = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//rigidbody.AddForce(20, rigidbody.velocity.y, rigidbody.velocity.z);
		//while(true)
		//{
			//transform.position = Vector3.Lerp (transform.position, endPos, 10f);
			//transform.position = Vector3.Lerp (transform.position, startPos, 10f);
		//}

		if(moveRight)
		{
			rigidbody.AddForce(20, rigidbody.velocity.y, rigidbody.velocity.z);
		}
		if(moveLeft)
		{
			rigidbody.AddForce(-20, rigidbody.velocity.y, rigidbody.velocity.z);
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "WallLeft")
		{
			moveLeft = false;
			moveRight = true;
		}
		if(collision.gameObject.tag == "WallRight")
		{
			moveRight = false;
			moveLeft = true;
		}
	}
}
