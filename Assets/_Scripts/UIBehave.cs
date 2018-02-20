using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIBehave : MonoBehaviour {
	//主菜单

	public void MainStart() {
		print("Game Start\n");
		SceneManager.LoadScene(3);
	}
	public void MainExit() {
		print("Game Quit\n");
		Application.Quit();
	}
	public void MainOpinion() {
		print("Game Opinion\n");
		SceneManager.LoadScene(2);
	}
	public void GamingBack() {
		HGBlock.Clear();
		HGObjectPool.GetIns().Clear();
		SceneManager.LoadScene(0);
	}
	public void OpinionBack() {
		SceneManager.LoadScene(0);
	}
	public void OpinionToggle(Toggle change) {
		HGOpinion op = HGOpinionLoader.OPtemp;
		op.GodMode = change.isOn;
		HGJsonLoader.BasicWrite<HGOpinion>(op,"config.das");
		HGJsonLoader.Unload();
	}

	public void OpinionSeedSave(GameObject Inf) {
		Text field = Inf.transform.Find("SeedNum").Find("Text").GetComponent<Text>();
		Text Info = Inf.transform.Find("Info").GetComponent<Text>();
		HGOpinion op = HGOpinionLoader.OPtemp;
		bool check = true;
		if (field.text.Length < 3) check = false;
		else for (int i=0;i< field.text.Length;i++)
				if (field.text[i]!='0'&& field.text[i] != '1'&& field.text[i] != '2') {
					check = false;
					break;
				}
		if (check) {
			op.Seed = field.text;
			Info.text = "<color=#00FF00>种子合法</color>";
		} else {
			op.Seed = "001100";
			Info.text = "<color=#FF0000>种子不合法,请重新输入</color>";
		}
		HGJsonLoader.BasicWrite<HGOpinion>(op, "config.das");
		HGJsonLoader.Unload();

	}
	public void OpinionBgmIDSave(GameObject inf) {
		HGOpinion op = HGOpinionLoader.OPtemp;
		print(Convert.ToInt32(inf.transform.Find("Text").GetComponent<Text>().text));
		op.BgmID = Convert.ToInt32(inf.transform.Find("Text").GetComponent<Text>().text);
		HGJsonLoader.BasicWrite<HGOpinion>(op, "config.das");
		HGJsonLoader.Unload();
	}
}
