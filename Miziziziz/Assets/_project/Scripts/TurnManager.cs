using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnManager : MonoBehaviour
{
    public bool active;
    [SerializeField] CombatUnit _activeUnit;
    public CombatUnit ActiveUnit
    {
        set
        {
            if (_activeUnit != null)
                _activeUnit.SetSeleted(false);

            _activeUnit = value;

            if(_activeUnit != null)
            _activeUnit.SetSeleted(true);
        }
        get
        {
            return _activeUnit;
        }
    }
    
    public abstract void StartTurn();
}
