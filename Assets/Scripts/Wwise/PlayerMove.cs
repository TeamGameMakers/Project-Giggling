using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour

{
    public AK.Wwise.Event Playerwalk;
    public void PlayerWalk()
    {
        Playerwalk.Post(gameObject);
    }

    public AK.Wwise.Event Playerrun;
    public void PlayerRun()
    {
        Playerrun.Post(gameObject);
    }
}

