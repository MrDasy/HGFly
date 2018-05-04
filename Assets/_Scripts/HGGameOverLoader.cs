using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HGGameOverLoader : MonoBehaviour {
	public Text dScore;
	public Text dTime;
	public Text Guide;
	public static float Delay = 1.5f;
	void Start () {
		Guide.gameObject.SetActive(false);
		CharacterStat statt = HGJsonLoader.BasicRead<CharacterStat>(".temp");
		dTime.text = string.Format("你的游戏时间: {0:D2} : {1:D2}", statt.TimeMin, statt.TimeSec);
		print(statt.TimeSec);
		dScore.text = string.Format("你的分数: {0}",statt.Score);
		HGJsonLoader.Unload();
		Invoke("DelayEnd", Delay);
	}

	void DelayEnd() {
		Guide.gameObject.SetActive(true);
	}
}
