using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HGBackgroundLoader : MonoBehaviour {
	private static Image bg1;
	private static Image bg2;
	private static HGBackgroundLoader ins;
	private float ChangeTime = 1f;
	// Use this for initialization
	public static void Init() {
		bg1 = GameObject.FindGameObjectWithTag("BG1").GetComponent<Image>();
		bg2 = GameObject.FindGameObjectWithTag("BG2").GetComponent<Image>();
		ins = GameObject.FindGameObjectWithTag("Environment_").GetComponent<HGBackgroundLoader>();
		Texture2D temp = HGAssetBundleLoader.GetIns().GetBundle("backgrounds").LoadAsset("grass2.psd") as Texture2D;
		bg1.sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height),Vector2.zero);
		bg2.sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);
	}

	public static void ChangeTo(string target) {
		ins.StartCoroutine("ChangeBG", target);
	}

	IEnumerator ChangeBG(string target) {
		Texture2D temp = HGAssetBundleLoader.GetIns().GetBundle("backgrounds").LoadAsset(target) as Texture2D;
		bg2.sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);
		yield return null;
		for (float timer = ChangeTime; timer > 0; timer -= Time.deltaTime) {
			bg1.color -= new Color(0,0,0,Time.deltaTime / ChangeTime);
			yield return null;
		}
		bg1.color += new Color(0, 0, 0, 1);
		bg1.sprite = bg2.sprite;
	}
}
