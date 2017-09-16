using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharaManager : MonoBehaviour {
    [SerializeField]
	private GameObject[] enemy;

    public void SetEnemyActive ( int index, bool active ) {
		enemy [index].SetActive (active);
	}

	public void SetAllEnemyActive( bool active ) {
		foreach( GameObject enemyUnit in enemy ){
			enemyUnit.SetActive (active);
		}
	}
}
