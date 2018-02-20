using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HGMainPreload : MonoBehaviour {
	bool loaded=false;
	// Use this for initialization
	void Start () {
		loaded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (loaded) {
			loaded = false;
			StartCoroutine("Preload");
		}
	}

	IEnumerator Preload() {
		/*print("loading");
		HGAssetBundleLoader.GetIns().GetBundle("audios");
		HGAssetBundleLoader.GetIns().GetBundle("prefabs");
		HGAudioLoader.Init();
		print("loaded");
		*/
		SceneManager.LoadScene(1);
		yield return null;
	}
}
