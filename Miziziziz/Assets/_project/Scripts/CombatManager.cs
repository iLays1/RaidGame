using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public List<CombatUnit> playerUnits;
    public List<CombatUnit> enemyUnits;

    public TurnManager playerTurnManager;
    public TurnManager enemyTurnManager;
    public TurnManager activeTurnManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach(var u in FindObjectsOfType<CombatUnit>())
        {
            if (u.playerUnit) playerUnits.Add(u);
            else enemyUnits.Add(u);
        }
    }

    private void Start()
    {
        ActivateTurnManager(playerTurnManager);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            EndCurrentTurn();
    }

    public void ActivateTurnManager(TurnManager turnManager)
    {
        activeTurnManager = turnManager;
        turnManager.active = true;
        turnManager.StartTurn();
    }

    public void EndCurrentTurn()
    {
        activeTurnManager.active = false;

        if(activeTurnManager == playerTurnManager)
        {
            ActivateTurnManager(enemyTurnManager);
        }
        else
        {
            ActivateTurnManager(playerTurnManager);
        }
    }
}
