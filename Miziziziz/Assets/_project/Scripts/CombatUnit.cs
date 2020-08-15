using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombatUnit : TileOccupant
{
    [Space]
    public string unitName = "";
    public bool playerUnit = false;
    [Space]
    public int moveRange = 2;
    public int attackRange = 2;

    public bool selected { get; private set; }
    public bool active { get; private set; }

    public List<CombatTile> moveTiles = new List<CombatTile>();
    public List<CombatTile> attackTiles = new List<CombatTile>();
    Vector3 baseScale;

    SpriteRenderer rend;
    Color baseColor;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        baseColor = rend.color;
        SetActive(true);

        if (unitName == "") unitName = NameGenerator.instance.Get();
        baseScale = transform.localScale;

        name = unitName;
    }

    public void SetSeleted(bool selected)
    {
        this.selected = selected;

        if (selected)
        {
            Debug.Log($"{unitName} was selected!");
            transform.DOScale(baseScale * 1.33f, 0.5f);
        }
        else
        {
            //Debug.Log($"{unitName} was deselected!");
            transform.DOScale(baseScale, 0.5f);
        }
    }
    public void SetActive(bool active)
    {
        this.active = active;
        if (active)
        {
            //Debug.Log($"{unitName} was selected!");
            rend.color = baseColor;
        }
        else
        {
            //Debug.Log($"{unitName} was deselected!");
            rend.color = baseColor * 0.75f;
        }
    }

    public void UpdateMoveTiles()
    {
        moveTiles.Clear();
        for (int mv = 0; mv < moveRange + 1; mv++)
        {
            foreach (var t in currentTile.GetSurroundingTiles(mv))
            {
                moveTiles.Add(t);
            }
        }
    }
    public void UpdateAttackTiles()
    {
        attackTiles.Clear();
        for (int mv = 0; mv < attackRange + 1; mv++)
        {
            foreach (var t in currentTile.GetSurroundingTiles(mv))
            {
                attackTiles.Add(t);
            }
        }
    }

    public override bool SetTile(CombatTile targetTile)
    {
        bool moved = base.SetTile(targetTile);
        UpdateMoveTiles();
        UpdateAttackTiles();
        return moved;
    }
}
