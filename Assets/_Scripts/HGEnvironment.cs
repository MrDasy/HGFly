using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGEnvironment : MonoBehaviour {
    //数值配置----------
    public static float width = 30.0f;
	public static float blank = 4.5f;
	private string seed;
	//--------------------
	int posX = 0;
	int passed = 0;
	GameObject CharacterEntity,EnvironEntity;
	System.Random ra = new System.Random();
	// Use this for initialization
	void Start () {
		seed = HGOpinionLoader.OPtemp.Seed;
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

		GameObject BlockTemplate;
		int modeid=-1;
		while (posX <= 3) {
			print((posX - 1) % (seed.Length));
			if (posX == 0)
				BlockTemplate = HGBlock.getBlock(HGBlockType.Mode_Start) as GameObject;
			else {
				modeid = (int)(seed[(posX - 1) % (seed.Length)] - '0');
				BlockTemplate = HGBlock.getBlock((HGBlockType)modeid) as GameObject;
			}
			GameObject objTemp = HGObjectPool.GetIns().Enpool(BlockTemplate);
			objTemp.GetComponent<Transform>().position = new Vector2(posX * width, 0f);
			objTemp.transform.SetParent(EnvironEntity.transform);
			HGBlock.BlockQueue.Enqueue(objTemp);
			if (posX != 0) {
				objTemp.transform.Find("ModeUpdater").GetComponent<HGModeUpdater>().SetAval(true);
				if (modeid == 0) {
					HGBlock.FlypeeSetup(ref objTemp, blank);
				}
				else if (modeid == 1) {
					objTemp.transform.Find("Emitter").GetComponent<HGEmitter>().Init();
					for (int i = -1; i <= 1; i++) {
						HGBlock.CoinSetup(objTemp.transform.position.x + 10 * i, 5, 3);
					}
				}
				else if (modeid == 2) {
					for (int i = -1; i <= 1; i++) {
						HGBlock.CoinSetup(objTemp.transform.position.x + 10 * i, 5, 3);
					}
				}
			}
			posX++;
		}
    }
    public void Environ_Update() {
		passed++;
		if (passed <= 1) return;
		HGObjectPool.GetIns().Depool(HGBlock.BlockQueue.Dequeue());
		int modeid = (int)(seed[(posX - 1) % (seed.Length)] - '0');
		GameObject BlockTemplate = HGBlock.getBlock((HGBlockType)modeid) as GameObject;
		GameObject objTemp = HGObjectPool.GetIns().Enpool(BlockTemplate);
		objTemp.GetComponent<Transform>().position = new Vector2(posX * width, 0f);
		objTemp.transform.SetParent(EnvironEntity.transform);
		HGBlock.BlockQueue.Enqueue(objTemp);
		if (posX != 0) {
			objTemp.transform.Find("ModeUpdater").GetComponent<HGModeUpdater>().SetAval(true);
			if (modeid == 0) {
				HGBlock.FlypeeSetup(ref objTemp, blank);
			} else if (modeid == 1) {
				objTemp.transform.Find("Emitter").GetComponent<HGEmitter>().Init();
				for (int i = -1; i <= 1; i++) {
					HGBlock.CoinSetup(objTemp.transform.position.x + 10 * i, 5, 3);
				}
			} else if (modeid == 2) {
				for (int i = -1; i <= 1; i++) {
					HGBlock.CoinSetup(objTemp.transform.position.x + 10 * i, 5, 3);
				}
			}
		}
		posX++;
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
        } else if (Type == HGBlockType.Mode_SkyBattle) {
			return HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("SkyBattle_Prefab.prefab");
		} else if (Type == HGBlockType.Mode_Runner) {
			return HGAssetBundleLoader.GetIns().GetBundle("prefabs").LoadAsset("Runner_prefab.prefab");
		}
		return null;
    }
	public static void FlypeeSetup(ref GameObject target,float blank){
		float t;
		for (int i = 1; i <= 3; i++) {
			t = (float)ra.Next(10, 180) / 100f * 3f;
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
			coinT.transform.GetComponent<Collider2D>().enabled = true;
			coinT.transform.position = new Vector2(posx+HGEnvironment.width/(3*num)*i,posy+(float)ra.Next(50,150)/100*HGEnvironment.blank/2);
			coinT.transform.SetParent(GameObject.FindWithTag("Environment_").transform);
			coinT.GetComponent<SpriteRenderer>().enabled = true;
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
    Mode_Flypee=0,
	Mode_SkyBattle=1,
	Mode_Runner=2,
	//状态
	Mode_Start=8,
	Mode_Pause=9,
	Mode_End=10
}
