using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGBGMLoader : MonoBehaviour {
	private int BGMID;
	System.Random ra;
	// Use this for initialization
	void Awake() {
		ra = new System.Random();
	}
	void Start () {
		
	}
	
	public void PlayBGM() {
		if (GetComponent<AudioSource>().isPlaying) return;
		BGMID = HGOpinionLoader.OPtemp.BgmID;
		if (BGMID < 1 || BGMID > 16)
			BGMID = ra.Next(1, 100) % 16;
		GetComponent<AudioSource>().clip = HGAudioLoader.Load(string.Format("bgm ({0})", BGMID));
		GetComponent<AudioSource>().Play();
	}
	public void StopBGM() {
		if (!GetComponent<AudioSource>().isPlaying) return;
		GetComponent<AudioSource>().Stop();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
