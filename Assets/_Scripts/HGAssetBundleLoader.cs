using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGAssetBundleLoader : MonoBehaviour {
    private HGAssetBundleLoader() { }

    private static HGAssetBundleLoader ins;
	private static Dictionary<string, AssetBundle> bundles=new Dictionary<string, AssetBundle>();
	private static string BundleURL =
#if UNITY_ANDROID
                    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                    "file://"+Application.dataPath + "/Raw/IOS_AB/";  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
					"file://" + Application.dataPath + "/StreamingAssets/WIN_AB/";
#else
                    string.Empty;  
#endif

	public static HGAssetBundleLoader GetIns() {
		if (ins == null)
			ins = new GameObject("HGAssetBundleLoader").AddComponent<HGAssetBundleLoader>();
		return ins;
    }

	public AssetBundle GetBundle(string FileName) {
		AssetBundle bundle;
		if (!bundles.TryGetValue(FileName, out bundle)) {
			print(FileName);
			WWW loader = new WWW(BundleURL + FileName);
			while (!loader.isDone) { };
			print(BundleURL + FileName);
			bundle = loader.assetBundle;
			bundles.Add(FileName, bundle);
		} //else print("matched");
		return bundle;
	}

	public static void Clear() {
		bundles.Clear();
	}
}
