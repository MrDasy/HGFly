using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGCoin : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name.Equals("Character")) {
			GetComponent<Collider2D>().enabled = false;
			GetComponent<AudioSource>().enabled = true;
			GetComponent<AudioSource>().clip = HGAudioLoader.Load("get_coin");
			GetComponent<AudioSource>().Play();
			GetComponent<SpriteRenderer>().enabled = false;
			GameObject.FindWithTag("Character_").GetComponent<HGCharacter>().UpdateScore();
		}
	}
}
