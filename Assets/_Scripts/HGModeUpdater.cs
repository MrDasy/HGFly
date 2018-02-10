using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGModeUpdater : MonoBehaviour {
	public HGBlockType Mode;
	private bool aval = true;
	// Use this for initialization

    void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name.Equals("Character")&&aval) {
			print("mode updated\n");
			aval = false;
			collider.GetComponent<HGCharacter>().UpdateMode(Mode);
			GameObject.FindWithTag("Environment_").GetComponent<HGEnvironment>().Environ_Update();
		}
		if (Mode==HGBlockType.Mode_SkyBattle&& collider.gameObject.name.Equals("Missile_(Clone)")) {
			HGObjectPool.GetIns().Depool(collider.gameObject);
		}
    }

	public void SetAval(bool cond) {
		aval = cond;
	}
}
