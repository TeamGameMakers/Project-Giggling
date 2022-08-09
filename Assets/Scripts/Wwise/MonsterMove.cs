using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove: MonoBehaviour

{
    public AK.Wwise.Event Monstermove;
    public void MonsterBMove()
    {
        Monstermove.Post(gameObject);
    }
}
