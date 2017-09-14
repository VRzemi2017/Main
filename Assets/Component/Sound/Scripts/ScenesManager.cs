using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {

	public string _sceneName1;
	public string _sceneName2;
	public string _sceneName3;
	public string _sceneName4;
	public string _sceneName5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetKeyDown( KeyCode.Z ) ) {
			Debug.Log( _sceneName1 + "に移動します" );
			//Application.LoadLevel ( _sceneName1 );
            SceneManager.LoadScene( "_sceneName1", LoadSceneMode.Additive );
            //SceneManager.LoadScene ( "b" );
            Destroy( GameObject.Find( "DontDestroyObjects" ) );
		}
		if ( Input.GetKeyDown( KeyCode.X ) ) {
			Debug.Log( _sceneName2 + "に移動します" );
            SceneManager.LoadScene("_sceneName2", LoadSceneMode.Additive);
            //Application.LoadLevel ( _sceneName2 );
            //SceneManager.LoadScene ( "b" );
        }
		if ( Input.GetKeyDown( KeyCode.C ) ) {
			Debug.Log( _sceneName3 + "に移動します" );
            SceneManager.LoadScene("_sceneName3", LoadSceneMode.Additive);
            //Application.LoadLevel ( _sceneName3 );
			//SceneManager.LoadScene ( "b" );
		}
		if ( Input.GetKeyDown( KeyCode.V ) ) {
			Debug.Log( _sceneName4 + "に移動します" );
            SceneManager.LoadScene("_sceneName4", LoadSceneMode.Additive);
            //Application.LoadLevel ( _sceneName4 );
			//SceneManager.LoadScene ( "b" );
		}
		if ( Input.GetKeyDown( KeyCode.B ) ) {
			Debug.Log( _sceneName5 + "に移動します" );
            SceneManager.LoadScene("_sceneName5", LoadSceneMode.Additive);
            //Application.LoadLevel ( _sceneName5 );
            //SceneManager.LoadScene ( "b" );
        }
	}
}
