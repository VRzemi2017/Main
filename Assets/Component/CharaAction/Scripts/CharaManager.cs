using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour {

	private GameObject[] enemy;

	void Start(){
		enemy [0] = GameObject.Find ("Creature_flea 1");
		enemy [1] = GameObject.Find ("Creature_flea 2");
		enemy [2] = GameObject.Find ("Creature_flea 3");
		enemy [3] = GameObject.Find ("Creature_flea 4");
		enemy [4] = GameObject.Find ("Creature_flea 5");
		enemy [5] = GameObject.Find ("Creature_flea 6");
		enemy [6] = GameObject.Find ("Creature_flea 7");
		enemy [7] = GameObject.Find ("Creature_flea 8");
		enemy [8] = GameObject.Find ("Creature_flea 9");
		enemy [9] = GameObject.Find ("Creature_flea 10");
		enemy [10] = GameObject.Find ("Creature_flea 11");
		enemy [11] = GameObject.Find ("Creature_flea 12");
	}

	public void SetEnemyActive ( int index, bool active ) {
		enemy [index].SetActive (active);
	}

	public void SetAllEnemyActive( bool active ) {
		foreach( GameObject enemyUnit in enemy ){
			enemyUnit.SetActive (active);
		}
	}
}
