using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//记录人物数据
public class HGCharacter : MonoBehaviour {
	//数值配置----------
	[SerializeField] private Text ScoreUI;
	//--------------------
	//人物状态----------
	private int Score=0;
	private HGBlockType GameMode=HGBlockType.Mode_Pause;
     //-------------------

    public void UpdateMode(HGBlockType mode) {
		if (GameMode != mode) {
			GetComponent<HGCharacterController>().UpdateModeOp(mode);
			GameMode = mode;
		}
	}
    public HGBlockType GetMode() {
        return GameMode;
    }

	public void UpdateScore() {
		Score++;
		ScoreUI.text = string.Format("分数:{0}",Score);
	}
	public void ResetScore() {
		Score = 0;
		ScoreUI.text = string.Format("分数:{0}", Score);
	}
}
