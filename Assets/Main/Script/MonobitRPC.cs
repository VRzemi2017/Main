using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class MonobitRPC : MonobitEngine.MonoBehaviour {

    public MonobitServer Server;

    public void RPC(string methodName, MonobitTargets target, params object[] parameters)
    {
        if (monobitView)
        {
            monobitView.RPC(methodName, target, parameters);
        }
    }


    [MunRPC]
    void RemoteReady()
    {
        Server.RemoteReady();
    }

    [MunRPC]
    void RecieveStart()
    {
        Server.RecieveStart();
    }

    [MunRPC]
    void RecieveEvent(GameEvent e, int ID)
    {
        Server.RecieveEvent(e, ID);
    }
}
