using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileOccupant : MonoBehaviour
{
    public CombatTile currentTile;

    private void Start()
    {
        var g = FindObjectOfType<CombatGrid>();

        var x = Mathf.RoundToInt(transform.localPosition.x);
        var y = Mathf.RoundToInt(transform.localPosition.y);

        SetTile(g.FindTile(x,y));
    }

    public virtual bool SetTile(CombatTile targetTile)
    {
        //returns if the move happened
        if (targetTile.occupant != null)
        {
            print("Can not move! tile is occupied");
            return false;
        }

        if (currentTile != null)
        {
            currentTile.occupant = null;
        }

        currentTile = targetTile;
        currentTile.occupant = this;
        transform.SetParent(currentTile.transform);
        transform.DOLocalMove(Vector3.zero, 0.5f);
        transform.DOScale(Vector3.one, 0.5f);
        return true;
    }
}
