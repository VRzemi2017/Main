using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    EVENT_HIT_GEM,
    EVENT_LEAVR_GEM,
    EVENT_GEM,
    EVENT_DAMAGE,
    EVENT_ENEMY_WAIT,
    EVENT_ENEMY_JUMP,
}

public struct EventData
{
    public GameEvent gameEvent;
    public GameObject eventObject;
}