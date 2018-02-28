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
	//数据
	private float jumpstay;//test
	private int TextureOrd;//test
	private float jumptime;//test

	private GameObject Character;//人物父物体
	


	//测试
	void Test_Init() {
		jumpstay = 0.08f;
		TextureOrd = 0;
		jumptime = -1f;
	}

	void Awake() {
		Test_Init();
		Character = this.gameObject;
	}
	// 初始
	void Start () {
		print("HGAnimator Loaded");
	}
	
	// Update is called once per frame
	void Update () {
		if (TextureOrd == 1) jumptime -= Time.deltaTime;
	}

	//跳跃
	public void HGanim_jump() {
		Test_SetTexture(1);
	}

	//下落
	public void HGanim_fall() {
		Test_SetTexture(2);
	}

	//上升
	public void HGanim_rise() {
		Test_SetTexture(3);
	}

	//游戏开始
	public void HGanim_start() {
		Test_SetTexture(3);
	}

	//奔跑
	public void HGanim_run() {
		Test_SetTexture(4);
	}
	//无敌生成光圈
	public void HGanim_godEnable() {
		transform.Find("God").gameObject.SetActive(true);
	}
	//无敌光圈消失（3秒后）
	public void HGanim_godDisable() {
		transform.Find("God").gameObject.SetActive(false);
	}
	void Test_SetTexture(int ord) {
		if (TextureOrd == ord) return;
		if (ord != 1 && jumptime > 0) return;
		if (ord == 1) jumptime = jumpstay;
		TextureOrd = ord;
		for (int i = 1; i <= 4; i++) {
			transform.Find(string.Format("Action{0}", i)).gameObject.SetActive(ord == i);
		}
	}
}
