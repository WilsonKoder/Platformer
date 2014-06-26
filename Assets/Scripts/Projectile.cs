using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * 15f * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Enemy") {
		Destroy(collision.gameObject);
		}
		Destroy(gameObject);
	}
}

