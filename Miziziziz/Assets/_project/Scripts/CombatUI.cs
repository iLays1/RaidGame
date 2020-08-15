using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    private void Awake()
    {
        CombatCursor.TileHoveredEvent.AddListener
            ((CombatTile t) => TileHoveredEventReaction(t));

        HideUI();
    }

    void TileHoveredEventReaction(CombatTile t)
    {
        if (t.occupant != null && t.occupant is CombatUnit)
        {
            UpdateUIWithUnit(t.occupant as CombatUnit);
            return;
        }

        HideUI();
    }

    public void UpdateUIWithUnit(CombatUnit unit)
    {
        nameText.text = unit.unitName;
    }

    public void HideUI()
    {
        nameText.text = "";
    }
}
