using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGMisslie : MonoBehaviour {
	private float speedx,speedy;
	private float width = 30f;
	System.Random ra=new System.Random();
	bool vstd = false;
	// Use this for initialization
	void Start () {
		speedx = -14*(float)ra.Next(80, 120) / 100;
		speedy = (float)ra.Next(-300, 300) / 100;
		GetComponent<Rigidbody2D>().velocity = new Vector3(speedx, speedy);
	}
	void Update() {
		transform.RotateAround(transform.position, Vector3.forward, speedy);
		if (!vstd) {
			Invoke("AutoDepool", width / -speedx);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name.Equals("Character"))
			HGObjectPool.GetIns().Depool(gameObject);
	}

	void AutoDepool() {
		vstd = false;
		HGObjectPool.GetIns().Depool(gameObject);
	}
}
