using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGMisslie : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name.Equals("Character"))
			HGObjectPool.GetIns().Depool(gameObject);
	}
}
