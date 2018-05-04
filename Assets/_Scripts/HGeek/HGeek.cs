//----------------------------------------
// For Unity
// Produced by MrDas
// 2018.05.04
// dasxxx@hgeek.club
// HGeek
//----------------------------------------

using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace HGeek {
	namespace Basic {
		//文件读写封装
		public class HGIO {
			private static FileStream FS = null;

			public static string PATH_dasdata(string filename) {
				string path = Application.dataPath + "/dasdata";
				Directory.CreateDirectory(path);
				return path + "/" + filename;
			}

			public static void CLOSE() {
				FS.Flush();
				FS.Close();
			}

			public static string READ(string filepath, FileMode fmode) {
				FS = new FileStream(filepath, fmode, FileAccess.Read);
				int fsLen = (int)FS.Length;
				byte[] bytes = new byte[fsLen];
				char[] data = new char[fsLen];
				FS.Read(bytes, 0, fsLen);
				Decoder dcder = Encoding.UTF8.GetDecoder();
				dcder.GetChars(bytes, 0, fsLen, data, 0);
				return new string(data);
			}

			public static void WRITE(string srcstring, string filepath, FileMode fmode) {
				FS = new FileStream(filepath, fmode, FileAccess.Write);
				byte[] bytes = new byte[srcstring.Length];
				Encoder ecder = Encoding.UTF8.GetEncoder();
				ecder.GetBytes(srcstring.ToCharArray(), 0, srcstring.Length, bytes, 0, true);
				FS.Seek(0, SeekOrigin.Begin);
				FS.Write(bytes, 0, bytes.Length);
			}

			public static void APPEND(string srcstring, string filepath) {
				FS = new FileStream(filepath, FileMode.Append, FileAccess.Write);
				byte[] bytes = new byte[srcstring.Length];
				Encoder ecder = Encoding.UTF8.GetEncoder();
				ecder.GetBytes(srcstring.ToCharArray(), 0, srcstring.Length, bytes, 0, true);
				FS.Seek(0, SeekOrigin.End);
				FS.Write(bytes, 0, bytes.Length);
			}
		}
		//基于HGIO的json读写封装
		public class HGJsonLoader {
			public static void Unload() {
				HGIO.CLOSE();
			}

			public static T BasicRead<T>(string filename)
			where T : class, new() {
				string filepath = HGIO.PATH_dasdata(filename);
				if (!File.Exists(filepath)) {
					Debug.Log(filepath);
					T newTarget = new T();
					BasicWrite<T>(newTarget, filename);
					return newTarget;
				}
				string datastr = HGIO.READ(filepath, FileMode.Open);
				T res = JsonMapper.ToObject<T>(datastr);
				return res;
			}

			public static void BasicWrite<T>(T target, string filename)
			where T : class {
				string filepath = HGIO.PATH_dasdata(filename);
				string jsondata = JsonMapper.ToJson(target);
				Debug.Log(jsondata);
				HGIO.WRITE(jsondata, filepath, FileMode.Create);
			}

		}
		//AssetBundle读取封装
		public class HGAssetBundleLoader : MonoBehaviour {
			private HGAssetBundleLoader() { }

			private static HGAssetBundleLoader ins;
			private static Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
			private static string BundleURL =
#if UNITY_ANDROID
                    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                    "file://"+Application.streamingAssetsPath + "/IOS_AB/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
					"file://" + Application.streamingAssetsPath + "/WIN_AB/";
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
		//对象池实现
		public class HGObjectPool : MonoBehaviour {
			private List<GameObject> pool = new List<GameObject>();

			private HGObjectPool() { }
			private static HGObjectPool ins;

			public static HGObjectPool GetIns() {
				if (ins == null)
					ins = new GameObject("HGObjectPool").AddComponent<HGObjectPool>();
				return ins;
			}

			public GameObject Enpool(GameObject target) {
				GameObject res;
				res = pool.Find(tar => tar.name.Equals(target.name + "(Clone)"));
				if (res == null) {
					//print("unmatched\n");
					res = Instantiate(target);
				} else {
					//print("matched\n");
					res.transform.SetParent(null);
					pool.Remove(res);
				}
				res.SetActive(true);
				return res;
			}

			public void Depool(GameObject target) {
				target.SetActive(false);
				target.transform.SetParent(GetIns().gameObject.transform);
				pool.Add(target);
			}

			public void Clear() {
				pool.Clear();
			}
		}
	}
}
