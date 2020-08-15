using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnStates
{
    public delegate void ptmDelegate(PlayerTurnManager ptm);
    public delegate void ptmwtDelegate(PlayerTurnManager ptm, CombatTile tile);
    public ptmwtDelegate tileClicked;
    public ptmDelegate stateStart;
    public ptmDelegate stateEnd;

    public static PTState_SelectingUnit stateSelectingUnit = new PTState_SelectingUnit();
    public static PTState_MovingUnit stateMovingUnit = new PTState_MovingUnit();
    public static PTState_SelectingTarget stateSelectingTarget = new PTState_SelectingTarget();

    public void SetState(PTState state)
    {
        tileClicked = (PlayerTurnManager ptm, CombatTile tile) => state.OnTileClicked(ptm, tile);
        stateStart =  (PlayerTurnManager ptm) => state.OnStateStart(ptm);
        stateEnd =  (PlayerTurnManager ptm) => state.OnStateEnd(ptm);
    }
}

public abstract class PTState
{
    public abstract void OnTileClicked(PlayerTurnManager ptm, CombatTile tile);
    public abstract void OnStateStart(PlayerTurnManager ptm);
    public abstract void OnStateEnd(PlayerTurnManager ptm);
}

public class PTState_SelectingUnit : PTState
{
    public override void OnStateStart(PlayerTurnManager ptm)
    {
    }

    public override void OnTileClicked(PlayerTurnManager ptm, CombatTile tile)
    {
        if (tile.occupant != null && tile.occupant is CombatUnit)
        {
            var tileUnit = tile.occupant as CombatUnit;

            if (tileUnit.playerUnit && tileUnit.active)
            {
                ptm.ActiveUnit = tileUnit;
                ptm.SetCurrentState(PlayerTurnStates.stateMovingUnit);
            }
        }
    }

    public override void OnStateEnd(PlayerTurnManager ptm)
    {
        //throw new NotImplementedException();
    }
}

public class PTState_MovingUnit : PTState
{
    public override void OnStateStart(PlayerTurnManager ptm)
    {
        ptm.lastTile = ptm.ActiveUnit.currentTile;
        ptm.interactableTiles.Clear();
        foreach (var t in ptm.ActiveUnit.moveTiles)
        {
            if (t != null)
            {
                if (t.occupant == null) t.MoveableEffect(true);
                else t.AttackableEffect(true);

                if (ptm.ActiveUnit.currentTile == t)
                    t.MoveableEffect(true);

                ptm.interactableTiles.Add(t);
            }
        }
    }

    public override void OnTileClicked(PlayerTurnManager ptm, CombatTile tile)
    {
        if (tile.occupant == null || tile == ptm.ActiveUnit.currentTile && 
            ptm.interactableTiles.Contains(tile))
        {
            ptm.ActiveUnit.SetTile(tile);
            ptm.SetCurrentState(PlayerTurnStates.stateSelectingTarget);
        }
        return;
    }

    public override void OnStateEnd(PlayerTurnManager ptm)
    {
        foreach (var t in ptm.grid.tiles)
        {
            t.MoveableEffect(false);
        }
    }
}

public class PTState_SelectingTarget : PTState
{
    public override void OnStateStart(PlayerTurnManager ptm)
    {
        ptm.interactableTiles.Clear();

        //Get skill related targets
        foreach (var t in ptm.ActiveUnit.attackTiles)
        {
            if (t != null)
            {
                t.AttackableEffect(true);
                if (ptm.ActiveUnit.currentTile == t)
                    t.MoveableEffect(true);

                ptm.interactableTiles.Add(t);
            }
        }
    }

    public override void OnTileClicked(PlayerTurnManager ptm, CombatTile tile)
    {
        if (tile.occupant != null &&
            tile.occupant is CombatUnit && 
            ptm.interactableTiles.Contains(tile))
        {
            var target = tile.occupant as CombatUnit;

            if(target != ptm.ActiveUnit)
            {
                //USE SKILL ON TARGET
                Debug.Log($"{target.unitName} targeted!");
            }
            ptm.SetCurrentState(PlayerTurnStates.stateSelectingUnit);

            ptm.ActiveUnit.SetActive(false);
            ptm.ActiveUnit = null;
        }
        return;
    }

    public override void OnStateEnd(PlayerTurnManager ptm)
    {
        foreach (var t in ptm.grid.tiles)
        {
            t.MoveableEffect(false);
        }
    }
}
