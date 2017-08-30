using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour {

	[SerializeField]
	private List<GameObject> enemy;

    public void SetEnemyActive(int num, bool active) {
        enemy[num].SetActive(active);
    }

    public void SetEnemyAllActive(bool active) {
        foreach (GameObject _ in enemy ) {
            _.SetActive(active);
        }
    }
}
