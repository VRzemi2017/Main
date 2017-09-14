using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabObject : MonoBehaviour {
    [SerializeField] GameObject[] prefabs;

	// Use this for initialization
	void Start () {
        prefabs.ToList().ForEach(p =>
        {
            //GameObject obj = Instantiate(p);
            p.transform.parent = transform;
            p.transform.localPosition = Vector3.zero;
            p.transform.localRotation = Quaternion.identity;
            p.transform.localScale = Vector3.one;
        });
	}
}
