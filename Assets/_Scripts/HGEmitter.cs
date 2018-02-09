using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGEmitter : MonoBehaviour {
	private float deltatime=1f;

	// Use this for initialization
	void Start () {
		StartCoroutine("AddMissile");
	}

	IEnumerator AddMissile() {
		while (true) {
			for (float timer = deltatime; timer > 0; timer -= Time.deltaTime)
				yield return null;
			System.Random ra = new System.Random();
			GameObject misle = Instantiate(HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Missile_.prefab") as GameObject);
			misle.transform.position = transform.position;
		}
	}
}
