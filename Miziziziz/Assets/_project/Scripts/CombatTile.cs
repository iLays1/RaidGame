using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CombatTileEvent : UnityEvent<CombatTile> { }
public class CombatTile : MonoBehaviour
{
    public static CombatTileEvent TileMouseHoveredEvent = new CombatTileEvent();
    public static CombatTileEvent TileClickedEvent = new CombatTileEvent();

    public Vector2 gridPosition;
    public CombatGrid parentGrid;
    Vector3 baseScale;

    public TileOccupant occupant;
    SpriteRenderer rend;
    Color baseColor;
    public Color moveColor;
    public Color attackColor;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        baseColor = rend.color;
        baseScale = transform.lossyScale;
    }

    public List<CombatTile> GetSurroundingTiles(int range)
    {
        var targets = new List<CombatTile>();

        int yVal = 0;
        for (int i = -range; i < range + 1; i++)
        {
            if (i == -range)
            {
                targets.Add(parentGrid.FindTile((int)gridPosition.x + i, (int)gridPosition.y + yVal));
            }
            else if (i != range * 2)
            {
                targets.Add(parentGrid.FindTile((int)gridPosition.x + i, (int)gridPosition.y + yVal));
                targets.Add(parentGrid.FindTile((int)gridPosition.x + i, (int)gridPosition.y + -yVal));
            }
            else
            {
                targets.Add(parentGrid.FindTile((int)gridPosition.x + i, (int)gridPosition.y + yVal));
            }

            if (i < 0)
            {
                yVal++;
            }
            else
            {
                yVal--;
            }
        }

        return targets;
    }
    
    public void MoveableEffect(bool active)
    {
        if(active)
        {
            rend.color = moveColor;
            return;
        }

        rend.color = baseColor;
    }
    public void AttackableEffect(bool active)
    {
        if (active)
        {
            rend.color = attackColor;
            return;
        }

        rend.color = baseColor;
    }

    private void OnMouseEnter()
    {
        TileMouseHoveredEvent.Invoke(this);
        transform.DOScale(baseScale * 1.2f, 0.3f);
    }
    private void OnMouseExit()
    {
        transform.DOScale(baseScale, 0.3f);
    }
}
