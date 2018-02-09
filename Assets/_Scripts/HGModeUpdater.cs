using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGModeUpdater : MonoBehaviour {
	public HGBlockType Mode;
	// Use this for initialization

    void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name.Equals("Character")) {
			print("mode updated\n");
			GetComponent<Collider2D>().enabled = false;
			collider.GetComponent<HGCharacter>().UpdateMode(Mode);
			GameObject.FindWithTag("Environment_").GetComponent<HGEnvironment>().Environ_Update();
		}
    }
}
