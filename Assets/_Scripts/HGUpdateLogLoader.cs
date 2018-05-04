using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HGUpdateLogLoader : MonoBehaviour {
	void Start () {
		string path =Application.streamingAssetsPath+"/updatelog.das";
		string datastr = HGIO.READ(path,FileMode.OpenOrCreate);
		if (datastr.Equals("")) GetComponent<Text>().text = "未找到更新日志";
		else GetComponent<Text>().text = datastr;
		HGIO.CLOSE();
	}
}
