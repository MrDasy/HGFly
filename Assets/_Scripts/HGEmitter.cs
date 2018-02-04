using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGEmitter : MonoBehaviour {
	private float deltatime=1f;
	private int num = 3;

	// Use this for initialization
	void Start () {
		StartCoroutine("AddMissile");
	}

	IEnumerator AddMissile() {
		while (true) {
			for (float timer = deltatime; timer > 0; timer -= Time.deltaTime)
				yield return null;
			System.Random ra = new System.Random();
			GameObject misle = HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Missile_.prefab") as GameObject;
			for (int i = 1; i <= num; i++) {
				if (ra.Next(0, 100) >= 0) {
					HGObjectPool.GetIns().Enpool(misle);
					misle.transform.SetParent(transform.parent);
					misle.transform.position = this.transform.position+ new Vector3(0,1f+2f*(float)i);
				}
			}
		}
	}
}
