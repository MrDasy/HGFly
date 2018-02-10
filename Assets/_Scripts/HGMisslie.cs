using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGMisslie : MonoBehaviour {
	void Update() {
		transform.RotateAround(transform.position, Vector3.forward, GetComponent<Rigidbody2D>().velocity.y);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name.Equals("Character"))
			HGObjectPool.GetIns().Depool(gameObject);
	}
}
