using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitInfoLayout : MonoBehaviour
{
    public void HandleOnIdle()
    {
        foreach (var unitInfo in UIManager.Instance.GameView.UnitsInfoLayouts)
        {
            unitInfo.HandleOnIdle();
        }
    }

    public void HandleOnAttackUnit()
    {
        foreach (var unitInfo in UIManager.Instance.GameView.UnitsInfoLayouts)
        {
            unitInfo.HandleOnAttackUnit();
        }
    }

    public void HandleOnAttackBase()
    {
        foreach (var unitInfo in UIManager.Instance.GameView.UnitsInfoLayouts)
        {
            unitInfo.HandleOnAttackBase();
        }
    }

    public void HandleOnDefend()
    {
        foreach (var unitInfo in UIManager.Instance.GameView.UnitsInfoLayouts)
        {
            unitInfo.HandleOnDefend();
        }
    }

    public void HandleOnExpand()
    {
        foreach (var unitInfo in UIManager.Instance.GameView.UnitsInfoLayouts)
        {
            unitInfo.HandleOnExpand();
        }
    }
}
