using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnManager : TurnManager
{
    public override void StartTurn()
    {
        Debug.Log("ENEMY TURN");
    }
}
