using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
	
	[System.Serializable]
	public struct SoundData {
		public AudioSource _sound;
		public float _sound_volume;					//_musicのVolumeを格納する変数
		public bool _sound_spatialize;				//_musicのspatializeを格納する変数
		public bool _sound_PauseFlag;				//_musicをPauseするか判定する変数
		public bool _button_controll;				//各種設定をボタンでコントロールするか判定する変数
	};

	public List<SoundDataComponent> _music; 

	public float volume_change = 0.1f; 



	// Use this for initialization
	void Start () {
		/*//SoundManagerのInspectorを初期化する
		for (int i = 0; i < _music.Count; i++) {
			_music [i].soundData = InitializeAudioSource (_music [i].soundData._sound);
		}*/
	
	}

	// Update is called once per frame
	void Update () {
		//Inspector内での調整を対応するAoudioSourceに反映する
		/*for (int i = 0; i < _music.Count; i++) {
			_music [i].soundData = AdjustmentAudioSource (_music [i].soundData);
		}


		//ボタンによる_soundObjectの調整
		for (int i = 0; i < _music.Count; i++) {
			_music [i].soundData = ButtonControll (_music [i].soundData);
		}*/

	}
		
	void LateUpdate() {
		/*//対応するAoudioSourceの値をInspector内に反映する
		for (int i = 0; i < _music.Count; i++) {
			_music [i].soundData = ReflectAudioSource (_music [i].soundData);
		}*/

	}

	//---SoundManagerのInspectorを初期化する関数
	public SoundData InitializeAudioSource( AudioSource _music ) {
		SoundData x;
		//動的初期化
		x._sound = null;
		x._sound_volume = 0f;
		x._sound_spatialize = false;
		x._sound_PauseFlag = true;
		x._button_controll = false;
		if ( _music ) {
			x._sound = _music;
			x._sound_volume = _music.volume;
			x._sound_spatialize = _music.spatialize;
		} else {
			Debug.LogWarning ("AudioSourceが対応してません。");
		}
		return x;
	}


	//---Inspector内での調整を対応するAoudioSourceに反映する関数
	public SoundData AdjustmentAudioSource( SoundData x ) {
		if (x._sound) {
			x._sound.volume = x._sound_volume;
			x._sound.spatialize = x._sound_spatialize;
			if (x._sound_PauseFlag) {
				x._sound.Pause ();
			} else {
				x._sound.UnPause ();
			}
		} else {
			Debug.LogWarning (x + "に反映させるAudioSourceがありません");
		}
		return x;
	}


	//--対応するAoudioSourceの値をInspector内に反映する関数
	public SoundData ReflectAudioSource( SoundData x ) {
		if (x._sound) {
			x._sound_volume = x._sound.volume;
			x._sound_spatialize = x._sound.spatialize;
		} else {
			Debug.LogWarning (x + "にAudioSourceがないためInspectorに表示出来ません");
		}
		return x;
	}


	//---AudioSourceのVolumeを調整する関数(ボタン操作)
	public void VolumeControll( AudioSource _music ) {
		if (Input.GetKey (KeyCode.V) && Input.GetKeyDown( KeyCode.UpArrow )) {
			_music.volume += volume_change;
			Debug.Log ( _music.gameObject.name + "のVolumeを" + _music.volume + "にしました" );
		}
		if (Input.GetKey (KeyCode.V) && Input.GetKeyDown( KeyCode.DownArrow )) {
			_music.volume -= volume_change;
			Debug.Log ( _music.gameObject.name + "のVolumeを" + _music.volume + "にしました" );
		}
	}

	//---AudioSourceのspatializeを調整する関数(ボタン操作)
	public void SpatializeControll( AudioSource _music ) {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (_music.spatialize) {
				_music.spatialize = false;
				_music.spatialBlend = 0f;
				Debug.Log (_music.gameObject.name + "の空間化を無効にし,3D→2D");
			} else {
				_music.spatialize = true;
				_music.spatialBlend = 1f;
				Debug.Log (_music.gameObject.name + "の空間化を有効にし,2D→3D");
			}
		}
	}


	//---AudioSourceの再生・一時停止を操作する関数(ボタン操作) ※Inspector内のx._sound_PauseFlagを変更するだけです
	public SoundData PauseControll( SoundData x ) {
		if ( Input.GetKeyDown( KeyCode.Return ) ) {
			if ( x._sound_PauseFlag ) {
				x._sound_PauseFlag = false;
				Debug.Log ( x._sound.gameObject.name + "を再生しました" );
			} else {
				x._sound_PauseFlag = true;
				Debug.Log ( x._sound.gameObject.name + "を一時停止しました" );
			}
		}
		return x;
	}


	//---ボタンによってAudioSourceの値を変化させる関数
	public SoundData ButtonControll( SoundData x ) {
		if (x._button_controll) {
			VolumeControll (x._sound);			//Volume調整
			SpatializeControll (x._sound);		//spatialize調整
			x　= PauseControll( x );					//再生・一時停止を操作 ※仮引数に注意
		}
		return x;
	}



	//
	//ここから外部からの使用専用の関数(アクセスできるようにstaticで宣言します)
	//

	//---AudioSourceを再生する関数
	static public SoundData Play( SoundData x ) {
		if (x._sound_PauseFlag) {
			x._sound_PauseFlag = false;
		} else {
			Debug.Log (x._sound.gameObject.name + "は既に再生されています");
		}
		return x;
	}


	//---AudioSourceを一時停止する関数
	static public SoundData Pause( SoundData x ) {
		if (x._sound_PauseFlag) {
			Debug.Log (x._sound.gameObject.name + "は既に一時停止されています");
		} else {
			x._sound_PauseFlag = true;
		}
		return x;
	}


	//---AudioSourceのVolumeを調整する関数(値を受け取ってその値に変換する)
	static public SoundData ChangeVolume( SoundData x, float num ) {
		if (x._sound) {
			if (num >= 0f && num <= 1f) {
				x._sound.volume = num;
			} else {
				Debug.Log (num + "は音量範囲外です" + "音量は0～1で入力してください");
			}
		} else {
			Debug.Log (x + "に反映させるAudioSourceがありません");
		}
		return x;
	}


	//---AudioSourceのspatializeを調整する関数(値を受け取ってその値に変換する)
	static public SoundData ChangeSpatialize( SoundData x, bool a ) {
		if (x._sound) {
			x._sound.spatialize = a;
		} else {
			Debug.Log (x + "に反映させるAudioSourceがありません");
		}
		return x;
	}



}
