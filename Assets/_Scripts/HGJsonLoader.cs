using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class HGJsonLoader {
	public static void Unload() {
		HGIO.CLOSE();
	}

	public static T BasicRead<T>(string filename) 
	where T:class,new(){
		string filepath = HGIO.PATH_dasdata(filename);
		if (!File.Exists(filepath)) {
			Debug.Log(filepath);
			T newTarget=new T();
			BasicWrite<T>(newTarget, filename);
			return newTarget;
		}
		string datastr = HGIO.READ(filepath,FileMode.Open);
		T res = JsonMapper.ToObject<T>(datastr);
		return res;
	}

	public static void BasicWrite<T>(T target,string filename) 
	where T:class{
		string filepath = HGIO.PATH_dasdata(filename);
		string jsondata = JsonMapper.ToJson(target);
		Debug.Log(jsondata);
		HGIO.WRITE(jsondata, filepath, FileMode.Create);
	}

}

public class HGOpinion {
	public HGOpinion() {
		GodMode=false;
		Seed = "022120121";
		BgmID = 0;
	}
	public bool GodMode { set; get; }
	public string Seed { set; get; }
	public int BgmID { set; get; }
}
