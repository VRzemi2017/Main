using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CharaManager : MonoBehaviour {
    [SerializeField]
	private GameObject[] enemy;

    private int damage = 0;
    public int DamageCount { get { return damage; } }

    private void Start()
    {
        MainManager.OnEventHappaned.Subscribe(e =>
        {
            if (e.gameEvent == GameEvent.EVENT_DAMAGE)
            {
                ++damage;
            }
        }).AddTo(this);
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
