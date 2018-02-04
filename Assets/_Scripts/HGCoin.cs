using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGCoin : MonoBehaviour {
	//数值配置----------
	[SerializeField] private float RotateSpeed;
	//--------------------

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.down * RotateSpeed, Space.World);
	}

	void OnTriggerEnter(Collider col) {
		GetComponent<AudioSource>().enabled = true;
		GetComponent<AudioSource>().clip = HGAudioLoader.Load("get_coin");
		GetComponent<AudioSource>().Play();
		//transform.parent.gameObject.SetActive(false);
		transform.gameObject.GetComponent<Renderer>().enabled = false;
		GameObject.FindWithTag("Character_").GetComponent<HGCharacter>().UpdateScore();
	}
}
