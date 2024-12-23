using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    [SerializeField] ScoreLayout _scoreLayout;
    [SerializeField] PlayerInfoLayout _playerInfoLayout;

    [SerializeField] UnitInfoLayout _unitInfoPrefab;
    [SerializeField] Transform _scrollContent;

    public PlayerInfoLayout PlayerInfoLayout { get => _playerInfoLayout; }

    public override void Init()
    {
        base.Init();

        _scoreLayout.Init();
    }

    public void CreateUnitInfo(int index, PawnController controller)
    {
        UnitInfoLayout currentUnitInfo = Instantiate(_unitInfoPrefab, _scrollContent);

        currentUnitInfo.Init(index, controller);

        _playerInfoLayout.UpdateTotalUnit(_scrollContent.childCount);
    }

    public void DestroyUnitInfo(UnitInfoLayout unitInfoToDestroy)
    {
        Destroy(unitInfoToDestroy.gameObject);

        _playerInfoLayout.UpdateTotalUnit(_scrollContent.childCount);
    }
}
