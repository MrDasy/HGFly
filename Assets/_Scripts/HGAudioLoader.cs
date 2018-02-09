using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGAudioLoader : MonoBehaviour {
	private static bool Loaded;
	private static Dictionary<string, AudioClip> audios = new Dictionary<string, AudioClip>();

	public static void Init() {
		if (!Loaded) {
			Loaded = true;
			Object[] objs = HGAssetBundleLoader.GetIns().GetBundle("audios").LoadAllAssets();
			for (int i = 0; i < objs.Length; i++) {
				print(objs[i].name);
				audios.Add(objs[i].name, objs[i] as AudioClip);
			}
		}
	}

	public static AudioClip Load(string filename) {
		AudioClip temp;
		if (audios.TryGetValue(filename, out temp)) {
			//print("got audio");
		} else print("cannot");
		return temp;
	}
}
