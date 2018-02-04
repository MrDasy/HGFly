using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIBehave : MonoBehaviour {
	//主菜单
	public void MainStart() {
		print("Game Start\n");
		SceneManager.LoadScene(1);
	}
	public void MainExit() {
		print("Game Quit\n");
		Application.Quit();
	}
	public void MainOpinion() {
		print("Game Opinion\n");
	}
}
