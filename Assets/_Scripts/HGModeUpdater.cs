using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGModeUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider) {
		print("mode updated\n");
		GameObject.FindWithTag("Environment_").GetComponent<HGEnvironment>().Environ_Update();
    }
}
