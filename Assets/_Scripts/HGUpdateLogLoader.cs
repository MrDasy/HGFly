using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class HGUpdateLogLoader : MonoBehaviour {
	void Start () {
		string path =
#if UNITY_ANDROID
                    "!\\assets\\updatelog.das";
#elif UNITY_IPHONE
					Application.dataPath+"/Raw/updatelog.das";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
					Application.dataPath + "\\StreamingAssets\\updatelog.das";
#else
                    string.Empty;  
#endif
		FileStream FS = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
		int fsLen = (int)FS.Length;
		byte[] bytes = new byte[fsLen];
		char[] data = new char[fsLen];
		FS.Read(bytes, 0, fsLen);
		Decoder dcder = Encoding.UTF8.GetDecoder();
		dcder.GetChars(bytes, 0, fsLen, data, 0);
		string datastr = new string(data);
		if (datastr.Equals("")) GetComponent<Text>().text = "未找到更新日志";
		else GetComponent<Text>().text = datastr;
		FS.Flush();
		FS.Close();
	}
	

}
