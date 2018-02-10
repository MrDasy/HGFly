using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGEmitter : MonoBehaviour {
	private float deltatime=1f;
	private float speedx, speedy;
	System.Random ra = new System.Random();
	public void Init() {
		StartCoroutine("AddMissile");
	}

	IEnumerator AddMissile() {
		while (true) {
			for (float timer = deltatime; timer > 0; timer -= Time.deltaTime)
				yield return null;
			speedx = -13 * (float)ra.Next(90, 110) / 100;
			speedy = 2*(float)ra.Next(400, 800) / 100;
			GameObject misle = HGObjectPool.GetIns().Enpool(HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Missile_.prefab") as GameObject);
			misle.GetComponent<Rigidbody2D>().velocity = new Vector2(speedx,speedy);
			misle.transform.SetParent(GameObject.FindWithTag("Environment_").transform);
			misle.transform.position = transform.position;
		}
	}
}
