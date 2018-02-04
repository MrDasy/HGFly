using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGMisslie : MonoBehaviour {
	private float speed = -3f;
	private float width = 30f;
	System.Random ra=new System.Random();
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = new Vector3(speed, 0);
		GetComponent<ConstantForce>().force = new Vector3(0,(float)ra.Next(-5,5)/30);
		Invoke("AutoDepool",width/(-speed)-0.5f);
	}
	
	void OnCollisionEnter(Collision collision) {
		transform.position = new Vector3(-100, -100);
		HGObjectPool.GetIns().Depool(gameObject);
	}

	void AutoDepool() {
		HGObjectPool.GetIns().Depool(gameObject);
	}
}
