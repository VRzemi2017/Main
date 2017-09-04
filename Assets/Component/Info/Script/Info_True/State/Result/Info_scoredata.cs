using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info_Scoredata : MonoBehaviour {

	public Text scoreText;
	public Text damageText;
	public Text teleportText;
	public Text eventText;
	public Text evaluationText;
	public TextMesh GM_Comment;

	public int get_score = 0;
	public int damage_hit = 0;
	public int teleport_times = 0;
	public int event_times = 0;
	public int Evaluation = 0;

	// Use this for initialization
	void Start () {
		get_score = 0;
		SetScore ();
		damage_hit = 0;
		SetDamage ();
		teleport_times = 0;
		SetTeleport ();
		event_times = 0;
		SetEvent ();
		Evaluation = 0;
		SetEvaluation ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetScore(){
		scoreText.text = string.Format( "宝石回収数：{0}",get_score );
	}
	void SetDamage(){
		damageText.text = string.Format( "被ダメージ：{0}",damage_hit );
	}
	void SetTeleport(){
		teleportText.text = string.Format( "テレポート回数：{0}", teleport_times );
	}
	void SetEvent(){
		eventText.text = string.Format( "イベント：{0}", event_times );
	}
	void SetEvaluation(){
		
		evaluationText.text = string.Format ("評価：{0}", Evaluation);
	}


	void UpdateText( ) {    //テキストの中身
		if (Evaluation == 0) {
			GM_Comment.text = "どんまい";
		}
	}
}
