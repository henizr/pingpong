using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : NetworkBehaviour
{
    [SyncVar]
    int leftScore;
    [SyncVar]
    int rightScore;

    public override void OnStartServer()
    {
        Ball.OnGoal += ServerHandleGoal;
    }
    public override void OnStopServer()
    {

        Ball.OnGoal -= ServerHandleGoal;

    }

    void ServerHandleGoal(string side)
    {
        if (side == "Right") leftScore++;
        else rightScore++;

        print($"Left: {leftScore}");
        print($"Right: {rightScore}");
    }
}
