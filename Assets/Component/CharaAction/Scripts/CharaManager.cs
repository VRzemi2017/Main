using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour {

	[SerializeField]
	private GameObject enemy01;
	[SerializeField]
	private GameObject enemy02;
	[SerializeField]
	private GameObject enemy03;
	[SerializeField]
	private GameObject enemy04;
	[SerializeField]
	private GameObject enemy05;
	[SerializeField]
	private GameObject enemy06;
	[SerializeField]
	private GameObject enemy07;
	[SerializeField]
	private GameObject enemy08;
	[SerializeField]
	private GameObject enemy09;
	[SerializeField]
	private GameObject enemy10;
	[SerializeField]
	private GameObject enemy11;
	[SerializeField]
	private GameObject enemy12;

	private void Activator() {
		//Enemy 1
		if ( !enemy01.activeInHierarchy ){
			enemy01.SetActive (true);
		}

		//Enemy 2
		if ( !enemy02.activeInHierarchy ){
			enemy02.SetActive (true);
		}

		//Enemy 3
		if ( !enemy03.activeInHierarchy ){
			enemy03.SetActive (true);
		}

		//Enemy 4
		if ( !enemy04.activeInHierarchy ){
			enemy04.SetActive (true);
		}

		//Enemy 5
		if ( !enemy05.activeInHierarchy ){
			enemy05.SetActive (true);
		}

		//Enemy 6
		if ( !enemy06.activeInHierarchy ){
			enemy06.SetActive (true);
		}

		//Enemy 7
		if ( !enemy07.activeInHierarchy ){
			enemy07.SetActive (true);
		}

		//Enemy 8
		if ( !enemy08.activeInHierarchy ){
			enemy08.SetActive (true);
		}

		//Enemy 9
		if ( !enemy09.activeInHierarchy ){
			enemy09.SetActive (true);
		}

		//Enemy 10
		if ( !enemy10.activeInHierarchy ){
			enemy10.SetActive (true);
		}

		//Enemy 11
		if ( !enemy11.activeInHierarchy ){
			enemy11.SetActive (true);
		}

		//Enemy 12
		if ( !enemy12.activeInHierarchy ){
			enemy12.SetActive (true);
		}
	}
}
