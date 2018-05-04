using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HGMenuPreload : MonoBehaviour {
	public Text username;
	public static string user;
	public static HGMenuPreload self;
	// Use this for initialization
	public static void ChangeUsername(string name) {
		if (self.username == null) return;
		self.username.text = string.Format("当前用户：{0}",name);
		user = name;
	}
	private void UsernameInit() {
		ChangeUsername(HGIO.READ(HGIO.PATH_dasdata("userinfo.das"), FileMode.Open));
		HGIO.CLOSE();
	}
	void Start () {
		self = this;
		UsernameInit();
	}
}
