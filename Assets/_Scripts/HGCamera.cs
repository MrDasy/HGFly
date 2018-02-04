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
    }
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = new Vector3(CharacterEntity.transform.position.x+CameraLead, CameraHeight, CameraDistance);
    }
}
