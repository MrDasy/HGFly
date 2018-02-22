using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HGPreloader{
	public static void Load(int sceneord) {
		SceneManager.LoadSceneAsync(3);
		SceneManager.LoadSceneAsync(sceneord);
	}
}
