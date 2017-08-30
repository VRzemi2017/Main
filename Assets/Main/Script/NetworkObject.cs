using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObject : MonoBehaviour {
    [SerializeField]
    private string objName;
    [SerializeField]
    private int group;

	// Use this for initialization
	void Start () {
        if (MonobitEngine.MonobitNetwork.inRoom)
        {
            GameObject obj = MonobitEngine.MonobitNetwork.Instantiate(objName, Vector3.zero, Quaternion.identity, group);
            if (obj)
            {
                obj.AddComponent<AttachObject>().Target = gameObject;
            }
        }
        else
        {
            GameObject obj = Resources.Load(objName) as GameObject;
            if (obj)
            {
                obj = Instantiate(obj);
                obj.transform.parent =  transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
            }
        }
	}
}
