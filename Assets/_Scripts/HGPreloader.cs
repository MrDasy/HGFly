using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HGPreloader{
	public static AsyncOperation async;
	public static void Load(int sceneord) {
		SceneManager.LoadSceneAsync(3);
		async=SceneManager.LoadSceneAsync(sceneord);
	}
}
