using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoLayout : MonoBehaviour
{
    [SerializeField] TMP_Text _totalUnit, _unitPerSecond, _newUnitIn;

    public void Init()
    {
    }

    public void UpdateTotalUnit(int totalUnit)
    {
        _totalUnit.text = "Total unit : " + totalUnit;
    }

    public void UpdateUnitPerSecond(float unitPerSecond)
    {
        _unitPerSecond.text = unitPerSecond.ToString("F1") + " Unit/s";
    }

    public void UpdateNewUnitIn(float timeBeforeNextUnit)
    {
        _newUnitIn.text = "New unit in : " + timeBeforeNextUnit.ToString("F1") + "s";
    }
}
