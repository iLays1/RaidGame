using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : TurnManager
{
    public CombatGrid grid;

    PlayerTurnStates playerStates = new PlayerTurnStates();

    [HideInInspector]
    public List<CombatTile> interactableTiles = new List<CombatTile>();
    [HideInInspector]
    public CombatTile lastTile;

    private void Awake()
    {
        grid = (grid == null) ? FindObjectOfType<CombatGrid>() : grid;

        CombatTile.TileClickedEvent.AddListener((CombatTile t) =>
        {
            if (!active) return;
            TileClickedEvent(t);
        });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (ActiveUnit == null) return;

            ActiveUnit.SetTile(lastTile);
            SetCurrentState(PlayerTurnStates.stateSelectingUnit);
            ActiveUnit = null;
        }
    }

    public void TileClickedEvent(CombatTile tile)
    {
        //Click event when selecting unit
        playerStates.tileClicked(this, tile);
    }

    public void SetCurrentState(PTState newState)
    {
        playerStates.stateEnd?.Invoke(this);
        playerStates.SetState(newState);
        playerStates.stateStart(this);
    }

    public override void StartTurn()
    {
        //This is for enemy turn more than player turn. But could be useful for polish effects
        SetCurrentState(PlayerTurnStates.stateSelectingUnit);
        foreach (var u in CombatManager.instance.playerUnits)
            u.SetActive(true);
    }
}
