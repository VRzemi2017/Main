using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpotControl : MonoBehaviour {
    [SerializeField] int ID;

    public int SpotID { get { return ID; } }

    private bool reached;
    public bool IsReached { get { return reached; } }

    ResultManager result;

    private void Start()
    {
        result = GameObject.FindObjectOfType<ResultManager>();

#if UNITY_EDITOR
        SpotControl[] spots = GameObject.FindObjectsOfType<SpotControl>();
        if (spots.ToList().Exists(s => ID == s.SpotID && this != s))
        {
            UnityEditor.EditorUtility.DisplayDialog("警告", "SpotID衝突: " + ID + " \nGameObject Name: " + gameObject.name, "OK");
        }
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.GetComponentInChildren<PlayerNoDisplay>())
        {
            reached = true;
            GetComponent<Collider>().enabled = false;

            if (result)
            {
                result.AddSpot(this);
            }
        }
    }
}
