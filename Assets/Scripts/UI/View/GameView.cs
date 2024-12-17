using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    [SerializeField] ScoreLayout _scoreLayout;

    [SerializeField] UnitInfoLayout _unitInfoPrefab;
    [SerializeField] Transform _scrollContent;

    public override void Init()
    {
        base.Init();

        _scoreLayout.Init();
    }

    public void CreateUnitInfo(int index,PawnController controller)
    {
        UnitInfoLayout currentUnitInfo = Instantiate(_unitInfoPrefab, _scrollContent);

        currentUnitInfo.Init(index, controller);
    }
}
