using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HGLoading : MonoBehaviour {
	private Scrollbar scrollbar;
	void Start () {
		scrollbar = GetComponent<Scrollbar>();
		scrollbar.size = 0;
	}

	void Update() {
		scrollbar.size = HGPreloader.async.progress;
	}
}
