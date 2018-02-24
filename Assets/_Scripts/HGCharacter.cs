using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//记录人物数据
public class HGCharacter : MonoBehaviour {
	//数值配置----------
	[SerializeField] private Text ScoreUI;
	[SerializeField] private int HPInit;
	//--------------------
	//人物状态----------
	private int Score=0;
	private HGBlockType GameMode=HGBlockType.Mode_Start;
	private int HitPoint;
     //-------------------
	public void Start() {
		UITimer.InitTiming(); 
	}
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
		ScoreUI.text = string.Format("分数: {0}",Score);
	}
	public void ResetScore() {
		Score = 0;
		ScoreUI.text = string.Format("分数: {0}", Score);
	}

	public int GetScore() {
		return Score;
	}

	public void ResetHP() {
		HitPoint = HPInit;
	}

	public bool Damage(int hit) {
		HitPoint -= hit;
		return HitPoint > 0;
	}

	public int GetHP() {
		return HitPoint;
	}
}

public class CharacterStat {
	public int TimeMin { set; get; }
	public int TimeSec { set; get; }
	public int Score { set; get; }
}