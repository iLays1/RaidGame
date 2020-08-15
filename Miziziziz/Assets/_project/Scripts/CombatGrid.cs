using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGrid : MonoBehaviour
{
    public CombatTile combatTilePrefab;
    public Vector2 gridScale;

    [Space]
    public List<CombatTile> tiles = new List<CombatTile>();

    private void Awake()
    {
        tiles.Clear();
        foreach(var t in GetComponentsInChildren<CombatTile>())
        {
            tiles.Add(t);
        }
    }

    [ContextMenu("Refresh Grid")]
    public void GenerateGrid()
    {
        ClearGrid();

        for (int x = 0; x < gridScale.x; x++)
        {
            for (int y = 0; y < gridScale.y; y++)
            {
                var tile = Instantiate(combatTilePrefab, this.transform);
                var tilePosition = new Vector3(x,y,0);

                tile.transform.localPosition = tilePosition;
                tile.name = $"tile({x},{y})";
                tiles.Add(tile);

                tile.gridPosition = new Vector2(x,y);
                tile.parentGrid = this;
            }
        }
    }

    public void ClearGrid()
    {
        foreach (var t in GetComponentsInChildren<CombatTile>())
        {
            if(t != null)
                DestroyImmediate(t.gameObject);
        }
        tiles.Clear();
    }

    public CombatTile FindTile(int x, int y)
    {
        Vector2 targetLocation = new Vector2(x, y);

        foreach (var t in tiles)
        {
            if (t.gridPosition == targetLocation)
                return t;
        }

        return null;
    }
}
