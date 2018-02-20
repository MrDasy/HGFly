using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//控制人物移动
public class HGCharacterController : MonoBehaviour {
	//数值配置----------
	[SerializeField] private float JumpSpeedLimit;
	[SerializeField] private float JumpForce;
	[SerializeField] private float JumpSpeed;
	[SerializeField] private float Gravity;
	[SerializeField] private float DropForce;
	[SerializeField] private float HeightInitialize;
	[SerializeField] private float MoveSpeedAddition;
	[SerializeField] private float MoveSpeedUpInterval;
	[SerializeField] private float MoveSpeedInitialize;
	[SerializeField] private float RotateSpeed;
	[SerializeField] private HGBlockType MODEINIT = HGBlockType.Mode_Flypee;
	private HGOpinion OP;
	public GameObject GuideUI;
    //--------------------

    HGCharacter Charc;
	GameObject Environ;
	bool landing = false;
	string[] ModeOp = new string[11];
	// Use this for initialization
	 void Awake() {
		HGOpinionLoader.Init();
	}
	void Start() {
		OP = HGOpinionLoader.OPtemp;
		Test_Init();
		GuideUI.SetActive(false);
		Test_SetTexture(2);
		Charc = this.gameObject.GetComponent<HGCharacter>();
		Environ = GameObject.FindWithTag("Environment_");
		Charc.transform.position = new Vector2(0f, HeightInitialize);
		ModeOp[0] = "HGi_flypee";
		ModeOp[1] = "HGi_Sky";
		ModeOp[2] = "HGi_Runner";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")) {
			HGm_back();
			return;
		}
        switch (Charc.GetMode()) {
            case HGBlockType.Mode_Pause:
				HGc_mode_pause();
                break;
            case HGBlockType.Mode_Flypee:
				HGc_mode_flypee();
                break;
            case HGBlockType.Mode_Start:
                Charc.UpdateMode(Charc.GetMode());
                break;
            case HGBlockType.Mode_End:
				HGc_mode_end();
                break;
			case HGBlockType.Mode_SkyBattle:
				HGc_mode_sky();
				break;
			case HGBlockType.Mode_Runner:
				HGc_mode_runner();
				break;
            default:
                break;
        }
	}

    void OnCollisionEnter2D(Collision2D collision) {
		landing = true;
		if (collision.gameObject.name == "Ground")
			PlayAudio("fall");
		else PlayAudio("hit");

		if (OP.GodMode) return;
		switch (Charc.GetMode()) {
			case HGBlockType.Mode_Flypee:
			case HGBlockType.Mode_SkyBattle:
				HGcol_mode1();
				break;
			case HGBlockType.Mode_Runner:
				HGcol_mode2();
				break;
			default:
				break;
		}
	}
	void HGcol_mode1() {
		Charc.UpdateMode(HGBlockType.Mode_End);
		StopCoroutine("AutoAddSpeed");
		GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
		GetComponent<ConstantForce2D>().force = new Vector2(0f, DropForce);
		UITimer.StopTiming();
	}
	void HGcol_mode2() {
		StopCoroutine("PlayStep");
		StartCoroutine("PlayStep");
		print("landed\n");
	}

	void OnCollisionStay2D() {
		landing = true;
		switch (Charc.GetMode()) {
			case HGBlockType.Mode_Flypee:
			case HGBlockType.Mode_SkyBattle:
				break;
			case HGBlockType.Mode_Runner:

				break;
			default:
				break;
		}
	}
	IEnumerator PlayStep() {
		while (true) {
			yield return new WaitForSeconds(HGAudioLoader.Load("Footstep03_01").length);
			PlayAudio("Footstep03_01");
		}
	}
	void OnCollisionExit2D() {
		landing = false;
		switch (Charc.GetMode()) {
			case HGBlockType.Mode_Flypee:
			case HGBlockType.Mode_SkyBattle:
				break;
			case HGBlockType.Mode_Runner:
				
				break;
			default:
				break;
		}
	}
	IEnumerator AutoAddSpeed() {
		while (true) {
			for (float timer = MoveSpeedUpInterval; timer > 0; timer -= Time.deltaTime)
				yield return null;
			GetComponent<Rigidbody2D>().velocity += new Vector2(MoveSpeedAddition, 0f);
		}
	}
	void HGm_back() {
		HGBlock.Clear();
		HGObjectPool.GetIns().Clear();
		SceneManager.LoadScene(0);
	}
	void HGc_mode_end() {
		if (Input.GetButtonDown("Restart")) {
			print("Reseted\n");
			transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			transform.position = new Vector2(0f, HeightInitialize);
			GetComponent<ConstantForce2D>().force = new Vector2(0f, 0f);
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			GetComponent<Rigidbody2D>().angularVelocity = 0f;
			Environ.GetComponent<HGEnvironment>().Environ_Init();
			UITimer.ResetTiming();
			Charc.UpdateMode(HGBlockType.Mode_Pause);
		}
	}
	void HGc_mode_pause() {
		if (Input.GetButtonDown("Jump")) {
			GuideUI.SetActive(false);
			Time.timeScale = 1;
			print("Started\n");
			StartCoroutine("AutoAddSpeed");
			Charc.UpdateMode(MODEINIT);
			UITimer.StartTiming();
		}
	}
	void HGc_mode_flypee() {
		if (OP.GodMode && GetComponent<Rigidbody2D>().velocity.x < MoveSpeedInitialize)
			GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeedInitialize, GetComponent<Rigidbody2D>().velocity.y);
		if (TextureOrd == 1) jumptime -= Time.deltaTime;
		Test_SetTexture((GetComponent<Rigidbody2D>().velocity.y > 0 ? 3 : 2));
		if (Input.GetButtonDown("Jump")) {
			Test_SetTexture(1);
			PlayAudio("jump");
			//Vector2 VecTemp = new Vector2(0f, (GetComponent<Rigidbody2D>().velocity.y < JumpSpeedLimit ? JumpForce : 0f));
			//GetComponent<Rigidbody2D>().AddForce(VecTemp);
			GetComponent<Rigidbody2D>().velocity += new Vector2(0f, JumpSpeed > GetComponent<Rigidbody2D>().velocity.y ? JumpSpeed - GetComponent<Rigidbody2D>().velocity.y : JumpSpeed / 2);
		}
		if (Input.GetButtonDown("Pause")) {
			print("Paused\n");
			Time.timeScale = 0;
			GuideUI.SetActive(true);
			UITimer.StopTiming();
			Charc.UpdateMode(HGBlockType.Mode_Pause);
		}
	}
	void HGc_mode_sky() {
		HGc_mode_flypee();
	}
	void HGc_mode_runner() {
		GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeedInitialize, GetComponent<Rigidbody2D>().velocity.y);
		
		if (landing)
		if (Input.GetButtonDown("Jump")) {
			StopCoroutine("PlayStep");
			PlayAudio("jump");
			GetComponent<Rigidbody2D>().angularVelocity = 0f;
			if (transform.localRotation.x==0f)
				transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
			else transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			GetComponent<ConstantForce2D>().force = -GetComponent<ConstantForce2D>().force;
		}
	}

	public void UpdateModeOp(HGBlockType mode) {
		print((int)mode);
		if ((int)mode<8) Invoke(ModeOp[(int)mode], 0f);
	}
	void HGi_flypee() {
		if (GetComponent<BoxCollider2D>().enabled) {
			GetComponent<BoxCollider2D>().enabled = false;
		}
		GetComponent<ConstantForce2D>().force = new Vector2(0f, Gravity);
		if (GetComponent<Rigidbody2D>().velocity==Vector2.zero) GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeedInitialize, 0f);
	}
	void HGi_Sky() {
		HGi_flypee();
	}
	void HGi_Runner() {
		Test_SetTexture(4);
		if (!GetComponent<BoxCollider2D>().enabled) {
			GetComponent<BoxCollider2D>().enabled = true;
		}
		GetComponent<ConstantForce2D>().force = new Vector2(0f, Gravity);
		if (GetComponent<Rigidbody2D>().velocity == Vector2.zero) GetComponent<Rigidbody2D>().velocity = new Vector2(MoveSpeedInitialize, 0f);
	}

	void PlayAudio(string filename) {
		GetComponent<AudioSource>().clip = HGAudioLoader.Load(filename);
		GetComponent<AudioSource>().Play();
	}

	public float jumpstay;
	public int TextureOrd;
	public float jumptime;

	void Test_Init() {
		jumpstay = 0.08f;
		TextureOrd = 0;
		jumptime = -1f;
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
