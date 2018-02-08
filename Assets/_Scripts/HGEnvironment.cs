using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGEnvironment : MonoBehaviour {
    //数值配置----------
    public static float width = 30.0f;
	public static float blank = 4f;
    //--------------------
	int posX = 0;
	int passed = 0;
	GameObject CharacterEntity,EnvironEntity;
	// Use this for initialization
	void Start () {
		CharacterEntity = GameObject.Find("Character");
		EnvironEntity = GameObject.FindWithTag("Environment_");
		HGAudioLoader.Init();
		Invoke("Environ_Init", 0f);
	}

	// Update is called once per frame
	void Update () {
		
	}
    public void Environ_Init() {
		posX = 0;
		passed = 0;
		CharacterEntity.GetComponent<HGCharacter>().ResetScore();
		while (HGBlock.BlockQueue.Count >0)
			HGObjectPool.GetIns().Depool(HGBlock.BlockQueue.Dequeue());
		while (HGBlock.CoinQueue.Count > 0)
			HGObjectPool.GetIns().Depool(HGBlock.CoinQueue.Dequeue());
		GameObject objTemp = HGObjectPool.GetIns().Enpool(HGBlock.getBlock(HGBlockType.Mode_Start) as GameObject);
		objTemp.GetComponent<Transform>().position = new Vector2(posX++*width,0f);
		objTemp.transform.SetParent(EnvironEntity.transform);
		HGBlock.BlockQueue.Enqueue(objTemp);
		GameObject objTemp2 = HGBlock.getBlock(HGBlockType.Mode_Flypee) as GameObject;
		while (posX <= 2) {
			GameObject objTemp1 = HGObjectPool.GetIns().Enpool(objTemp2);
			objTemp1.transform.Find("ModeUpdater").GetComponent<Collider2D>().enabled = true;
			objTemp1.GetComponent<Transform>().position = new Vector2(posX++ * width, 0f);
			HGBlock.FlypeeSetup(ref objTemp1,blank);
			objTemp1.transform.SetParent(EnvironEntity.transform);
			HGBlock.BlockQueue.Enqueue(objTemp1);
        }
    }
    public void Environ_Update() {
		passed++;
		if (passed <= 1) return;
		HGObjectPool.GetIns().Depool(HGBlock.BlockQueue.Dequeue());
		GameObject objTemp1 = HGObjectPool.GetIns().Enpool(HGBlock.getBlock(HGBlockType.Mode_Flypee) as GameObject);
		objTemp1.GetComponent<Transform>().position = new Vector2(posX++ * width, 0f);
		HGBlock.FlypeeSetup(ref objTemp1, blank);
		objTemp1.transform.SetParent(EnvironEntity.transform);
		HGBlock.BlockQueue.Enqueue(objTemp1);
		if (passed <= 2) return;
		HGBlock.CoinSetdown(3);
	}
}

public class HGBlock {
	private static System.Random ra = new System.Random();
	public static Queue<GameObject> CoinQueue = new Queue<GameObject>();
	public static Queue<GameObject> BlockQueue = new Queue<GameObject>();
	public static Object getBlock(HGBlockType Type) {
        if (Type == HGBlockType.Mode_Flypee) {
            return HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Flypee_Prefab.prefab");
        } else if (Type == HGBlockType.Mode_Start) {
            return HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Start_Prefab.prefab");
        } else
            return null;
    }
	public static void FlypeeSetup(ref GameObject target,float blank){
		float t;
		for (int i = 1; i <= 3; i++) {
			t = (float)ra.Next(20, 200) / 100f * 3f;
			target.transform.Find(System.String.Format("Ob{0}", i)).GetComponent<Transform>().localScale = new Vector2(1f, t);
			target.transform.Find(System.String.Format("Ob{0} (1)", i)).GetComponent<Transform>().localScale = new Vector2(1f, 10f - t - blank);
			CoinSetup(target.transform.Find(System.String.Format("Ob{0}", i)).transform.position.x,t,3);
		}
	}
	public static void CoinSetup(float posx,float posy,float num) {
		GameObject coin = HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Coins_.prefab") as GameObject;
		GameObject coinT;
		for (int i = -1; i <= -2+num;i++) {
			coinT = HGObjectPool.GetIns().Enpool(coin);
			coinT.transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
			coinT.transform.position = new Vector2(posx+HGEnvironment.width/(3*num)*i,posy+(float)ra.Next(50,150)/100*HGEnvironment.blank/2);
			coinT.transform.SetParent(GameObject.FindWithTag("Environment_").transform);
			coinT.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
			CoinQueue.Enqueue(coinT);
		}
		for (int i = 1; i <= num; i++) {
			coinT = HGObjectPool.GetIns().Enpool(coin);
			HGObjectPool.GetIns().Depool(coinT);
		}
	}
	public static void CoinSetdown(float num) {
		for (int i=1;i<=(int)num*3;i++)
			HGObjectPool.GetIns().Depool(CoinQueue.Dequeue());
	}
	public static void Clear() {
		CoinQueue.Clear();
		BlockQueue.Clear();
	}
}

public enum HGBlockType {
	//模式
    Mode_Flypee,
	Mode_SkyBattle,
	//状态
	Mode_Start,
	Mode_Pause,
	Mode_End
}
