using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGCoin : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col) {
		GetComponent<Collider2D>().enabled = false;
		GetComponent<AudioSource>().enabled = true;
		GetComponent<AudioSource>().clip = HGAudioLoader.Load("get_coin");
		 GetComponent<AudioSource>().Play();
		//transform.parent.gameObject.SetActive(false);
		transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
		GameObject.FindWithTag("Character_").GetComponent<HGCharacter>().UpdateScore();
	}
}
