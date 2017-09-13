using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class MonobitRPC : MonobitEngine.MonoBehaviour {

    private MonobitServer Server;

    private void Start()
    {
        Server = GameObject.FindObjectOfType<MonobitServer>();
    }

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
        if (Server)
        {
            Server.RemoteReady();
        }
    }

    [MunRPC]
    void RecieveStart()
    {
        if (Server)
        {
            Server.RecieveStart();
        }
    }

    [MunRPC]
    void RecieveEvent(GameEvent e, int ID)
    {
        if (Server)
        {
            Server.RecieveEvent(e, ID);
        }
    }
}
