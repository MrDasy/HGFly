using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UIOpinion : MonoBehaviour {
	private static string FileURL =
#if UNITY_ANDROID
                    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                    Application.dataPath + "/Raw/";  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
					"file://" + Application.dataPath + "/StreamingAssets/";
#else
                    string.Empty;  
#endif
	FileStream fs;
	// Use this for initialization
	void Awake() {
		fs = null;
	}
	void Start () {
		HGOpinion op = new HGOpinion();
		op.GodMode = true;
		WriteOp(op);
	}

	void LoadFile(string path,string filename) {
		fs = new FileStream(path + "/" + filename, FileMode.Open);
		byte[] bytes=new byte[fs.Length];
		fs.Read(bytes, 0, (int)fs.Length);
	}

	void WriteOp(HGOpinion opinion) {
		fs = new FileStream(FileURL + "config.das", FileMode.Create);
	}

	void CloseFile() {

	}
}

class HGOpinion {
	public bool GodMode { set; get; }
}
