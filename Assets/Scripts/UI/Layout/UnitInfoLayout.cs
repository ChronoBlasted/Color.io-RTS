using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoLayout : MonoBehaviour
{
    [SerializeField] TMP_Text _title;
    [SerializeField] PawnController _controller;

    [SerializeField] Image bgAttack, bgIdle, bgDefend, bgExpand;

    Image currentImg;

    public void Init(int index, PawnController controller)
    {
        _title.text = "#" + index;
        _controller = controller;

        UpdateLastClickBg(bgExpand);

        _controller.OnPawnDie += Die;
    }

    public void HandleOnIdle()
    {
        _controller.Unit.StateMachine.SetState<UnitIdleState>();

        UpdateLastClickBg(bgIdle);
    }

    public void HandleOnAttackUnit()
    {
        _controller.Unit.StateMachine.SetState<UnitAttackUnitState>();

        UpdateLastClickBg(bgAttack);
    }

    public void HandleOnDefend()
    {
        _controller.Unit.StateMachine.SetState<UnitDefendState>();

        UpdateLastClickBg(bgDefend);
    }

    public void HandleOnExpand()
    {
        _controller.Unit.StateMachine.SetState<UnitExpandState>();

        UpdateLastClickBg(bgExpand);
    }

    void Die(PawnController pawnToAdd)
    {
        _controller.OnPawnDie -= Die;

        UIManager.Instance.GameView.DestroyUnitInfo(this);
    }

    void UpdateLastClickBg(Image newImage)
    {
        if (currentImg != null) currentImg.color = ColorManager.Instance.GetColorByTeam(PlayerManager.Instance.PlayerTeamColor);

        currentImg = newImage;

        currentImg.color = ColorManager.Instance.selectedColor;
    }
}
