using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ToggleTest : MonoBehaviour {
	private static string fURL;
	void Awake() {
		fURL =
#if UNITY_ANDROID
                    "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                    Application.dataPath + "/Raw/";  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
					"file://" + Application.dataPath + "/StreamingAssets";
#else
                    string.Empty;  
#endif
	}
	// Use this for initialization
	void Start () {
		writeFile("config.das");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void writeFile(string fileName) {
		FileStream fs = new FileStream(fileName, FileMode.Create);   //打开一个写入流

		string str = "写入文件";

		byte[] bytes = Encoding.UTF8.GetBytes(str);
		fs.Write(bytes, 0, bytes.Length);
		fs.Flush();     //流会缓冲，此行代码指示流不要缓冲数据，立即写入到文件。
		fs.Close();     //关闭流并释放所有资源，同时将缓冲区的没有写入的数据，写入然后再关闭
		fs.Dispose();   //释放流所占用的资源，Dispose()会调用Close(),Close()会调用Flush();    也会写入缓冲区内的数据。
	}
}
