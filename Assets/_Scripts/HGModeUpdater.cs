using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGModeUpdater : MonoBehaviour {
	public HGBlockType Mode;
	private bool able = true;
	private string[] nameord = { "grass2.psd","playground2.psd","corridor1.psd"};

    void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name.Equals("Character")&& able) {
			print("mode updated\n");
			HGBackgroundLoader.ChangeTo(nameord[(int)Mode]);
			able = false;
			collider.GetComponent<HGCharacter>().UpdateMode(Mode);
			GameObject.FindWithTag("Environment_").GetComponent<HGEnvironment>().Environ_Update();
		}
		if (Mode==HGBlockType.Mode_SkyBattle&& collider.gameObject.name.Equals("Missile_(Clone)")) {
			HGObjectPool.GetIns().Depool(collider.gameObject);
		}
    }

	public void SetAval(bool cond) {
		able = cond;
	}
}
