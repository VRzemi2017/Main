using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Messager : MonoBehaviour {
    private StringReactiveProperty message = new StringReactiveProperty();
    public ReadOnlyReactiveProperty<string>  Message { get { return message.ToReadOnlyReactiveProperty(); } }

    public void SetMessage(string msg)
    {
        message.Value = msg;
    }
}
