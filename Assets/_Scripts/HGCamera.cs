using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGCamera :  MonoBehaviour {
    GameObject CharacterEntity;
	//数值配置----------
	[SerializeField] private float CameraHeight;
	[SerializeField] private float CameraDistance;
	[SerializeField] private float CameraLead;
	//--------------------

	// Use this for initialization
	void Start () {
        CharacterEntity = GameObject.FindWithTag("Character_");
		GetComponent<AudioSource>().clip = HGAudioLoader.Load(string.Format("bgm ({0})",HGOpinionLoader.OPtemp.BgmID));
		GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = new Vector3(CharacterEntity.transform.position.x+CameraLead, CameraHeight, CameraDistance);
    }
}
