using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombatCursor : TileOccupant
{
    public static CombatTileEvent TileHoveredEvent = new CombatTileEvent();
    Vector3 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale;
        CombatTile.TileMouseHoveredEvent.AddListener((CombatTile t) => SetTile(t));
    }

    private void Update()
    {
        if(SelectInput())
        {
            //Debug.Log($"{currentTile.name} was clicked!");
            CombatTile.TileClickedEvent.Invoke(currentTile);
        }
    }

    public bool SelectInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            return true;
        }

        return false;
    }

    public override bool SetTile(CombatTile targetTile)
    {
        currentTile = targetTile;
        
        transform.DOMove(currentTile.transform.position, 0.1f);
        transform.DOScale(baseScale, 0.2f);

        TileHoveredEvent.Invoke(currentTile);
        return true;
    }
}
