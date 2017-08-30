using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentChanger : MonoBehaviour {



	[System.Serializable]
	public struct ParentData {
		public GameObject _gameObject;
		public GameObject _parent_gameObject;
	}

	public List<ParentData> _music;


	//親となるObject
	public GameObject Sound_root;
	public GameObject Enemy;
	public GameObject Gem;
	public GameObject Wand;



	// Use this for initialization
	void Start () {
		//親となるGameObjectを取得（デバック用）
		/*
		parent_a = GameObject.Find ( "Cube" );
		parent_b = GameObject.Find ( "Sphere" );
		*/

		//ParentChangerのInspectorを初期化
		for (int i = 0; i < _music.Count; i++) {
			_music [i] = UpdateParentData (_music [i]);
		}

	}
	
	// Update is called once per frame
	void Update () {
		//親子関係・positionを変えるイベント
		if (Input.GetKeyDown (KeyCode.A)) {
			ChangeParent ( Sound_root, _music[0]._gameObject );
			MovePosition ( Sound_root, _music[0]._gameObject );
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			ChangeParent ( Enemy, _music[0]._gameObject );
			MovePosition ( Enemy, _music[0]._gameObject );
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			ChangeParent ( Gem, _music[0]._gameObject );
			MovePosition ( Gem, _music[0]._gameObject );
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			ChangeParent ( Wand, _music[0]._gameObject );
			MovePosition ( Wand, _music[0]._gameObject );
		}


		//ParentChangerのInspectorを更新
		for (int i = 0; i < _music.Count; i++) {
			_music [i] = UpdateParentData (_music [i]);
		}
			
	}



	//---obj1を親、obj2を子の関係にする関数
	void ChangeParent( GameObject obj1, GameObject obj2 ) {
		if (obj1 && obj2) {
			obj2.transform.parent = obj1.transform;
		} else {
			if (!obj1 && obj2) {
				Debug.Log ("親にするオブジェクトが見つかりませんでした");
			} else if (obj1 && !obj2) {
				Debug.Log ("子にするオブジェクトが見つかりませんでした");
			} else {
				Debug.Log ("親と子のオブジェクト両方見つかりませんでした");
			}
		}
	}


	//---obj2のpositionをobj1のpositonに対応した位置に移動させる関数
	void MovePosition( GameObject obj1, GameObject obj2 ) {
		if (obj1 && obj2) {
			obj2.transform.position = obj1.transform.position;
		} else {
			if (!obj1 && obj2) {
				Debug.Log ("親にするオブジェクトが見つかりませんでした");
			} else if (obj1 && !obj2) {
				Debug.Log ("子にするオブジェクトが見つかりませんでした");
			} else {
				Debug.Log ("親と子のオブジェクト両方見つかりませんでした");
			}
		}
	}

	//---ParentChangerのInspectorを更新する関数
	ParentData UpdateParentData( ParentData x ) {
		if (x._gameObject) {
			x._parent_gameObject = x._gameObject.transform.parent.gameObject;
		} else {
			Debug.LogWarning (x + "にGameObjectがセットされていません");
		}
		return x;
	}


	//---ParentChangerのInspector内の情報を親子関係に反映させる関数 ※実行時Inspectorをいじくれないため未使用
	void ReflectParentData( ParentData x ) {
		if (x._gameObject) {
			x._gameObject.transform.parent = x._parent_gameObject.transform;
		}
	}



}
