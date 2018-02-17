using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

public class HGJsonLoader {
	static FileStream FS =null;
	static int len = 2048;
	public static void Unload() {
		FS.Flush();
		FS.Close();
	}
	public static T BasicRead<T>(string filename) 
	where T:class{
		FS = new FileStream(filename, FileMode.Open, FileAccess.Read);
		int fsLen = (int)FS.Length;
		byte[] bytes = new byte[fsLen];
		char[] data = new char[fsLen];
		FS.Read(bytes, 0, fsLen);
		Decoder dcder = Encoding.UTF8.GetDecoder();
		dcder.GetChars(bytes, 0, fsLen, data, 0);
		string datastr = new string(data);
		Debug.Log(datastr);
		T res = JsonMapper.ToObject<T>(datastr);
		return res;
	}

	public static void BasicWrite<T>(T target,string filename) 
	where T:class{
		FS = new FileStream(filename, FileMode.Create, FileAccess.Write);
		string jsondata = JsonMapper.ToJson(target);
		Debug.Log(jsondata);
		byte[] bytes = new byte[jsondata.Length];
		Encoder ecder = Encoding.UTF8.GetEncoder();
		ecder.GetBytes(jsondata.ToCharArray(),0,jsondata.Length,bytes,0,true);
		FS.Seek(0,SeekOrigin.Begin);
		FS.Write(bytes, 0, bytes.Length);
	}
	
}

public class HGOpinion {
	public bool GodMode { set; get; }
	public string Seed { set; get; }
}
