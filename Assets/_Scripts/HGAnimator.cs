using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *动画接口，不涉及人物其他方面
 * 在每个函数中加入关于美工的代码
 * 在相应时刻会自动调用
 * 可将测试用的贴图代码删除
 */
public class HGAnimator : MonoBehaviour {
	private GameObject Character;//人物父物体

	void Awake() {
		Character = this.gameObject;
	}
	// 初始
	void Start () {
		print("HGAnimator Loaded");
	}
	
	// Update is called once per frame
	void Update () {

	}

	//跳跃
	public void HGanim_jump() {

	}

	//下落
	public void HGanim_fall() {

	}

	//上升
	public void HGanim_rise() {

	}

	//游戏开始
	public void HGanim_start() {

	}

	//奔跑
	public void HGanim_run() {

	}
	//无敌生成光圈
	public void HGanim_godEnable() {
		transform.Find("God").gameObject.SetActive(true);
	}
	//无敌光圈消失（3秒后）
	public void HGanim_godDisable() {
		transform.Find("God").gameObject.SetActive(false);
	}
}
