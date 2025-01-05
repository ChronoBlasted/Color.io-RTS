using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndView : View
{
    [SerializeField] TMP_Text _title;

    public override void Init()
    {
        base.Init();
    }

    public void UpdateEnd(bool isWin)
    {
        _title.text = isWin ? "VICTORY" : "DEFEAT";
    }

    public void Replay()
    {
        GameManager.Instance.ReloadScene();
    }
}
